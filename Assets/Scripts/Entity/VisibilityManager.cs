using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VisibilityManager : MonoBehaviour
{
    public bool _isVisible {
        get {
            return coverObjects.Count == 0;
        }
    }
    public bool isVisible => _isVisible;
    public float visibilityTrigger = 2.5f;
    public float visibilityTimer;
    public float distanceUntilDiscoverable = 30f; // decides how far the entity can be until it can go into survey
    
    [SerializeField]bool debuggingDistance = false;
    public UnityEvent onSeen;
    private bool seenEventPlaying = false;
    public Vector2 lastPlayerPositionSeen;
    Transform playerT;
    GameObject entity;
    StateManager stateManager;
    EntityStateTriggers entityTriggers;
    static VisibilityManager instance;

    public List<PlayerHider> coverObjects;
    public static VisibilityManager Instance {
        get {  return instance; }
    }
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        entity = GameObject.FindGameObjectWithTag("Entity");
        stateManager = entity.GetComponent<StateManager>();
        entityTriggers = entity.GetComponent<EntityStateTriggers>();
        
    }

    public float distanceToEntity() {
        Vector2 VNoYPlayer = new Vector2(playerT.position.x, 0);
        Vector2 VNoYEntity = new Vector2(entity.transform.position.x, 0);
        float distance = Vector2.Distance(VNoYPlayer, VNoYEntity);
        return distance;
    }
    void Update() {
        if(_isVisible != true){
            visibilityTimer = 0;
        }
        if (playerT == null) {
            playerT = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (_isVisible == true && 
            entityTriggers.isSpottable &&
            RoomTracker.Instance.canEntitySurvey == true) {
            //If it's not submerged or emerged then try to feed visual input to the entity to act accordingly
            if (entityTriggers.transitioned == true) {
                if (stateManager.GetCurrentState() is Roaming) {
                    entityTriggers.noticedPlayer = true;
                }

                if (stateManager.GetCurrentState() is Surveying) {
                    entityTriggers.isSpotting = true;
                }

                if (stateManager.GetCurrentState() is Spotting) {
                    entityTriggers.isSpotting = true;
                }

                

                UpdateTimer();
            }
            lastPlayerPositionSeen = playerT.position;

        } else {
            //Toggle the visual input off so mechanics work accordingly
            if(entityTriggers.transitioned) {
                if (stateManager.GetCurrentState() is Surveying) {
                    entityTriggers.isSpotting = false;
                }

                if (stateManager.GetCurrentState() is Spotting) {
                    entityTriggers.isSpotting = false;
                }
            }
            
        }

        CheckIfSpotted();
    }

    public void UpdateTimer()
    {
        if (seenEventPlaying == false)
        {
            visibilityTimer += Time.deltaTime * 1f;
        }
    }

    public void CheckIfSpotted()
    {
        if (visibilityTimer >= visibilityTrigger && seenEventPlaying == false)
        {
            visibilityTimer = 0f;
            seenEventPlaying = true;
            //onSeen?.Invoke(); <<<< 
            Debug.Log("Seen");
        }
    }

    public void ResetOnSeenEvent()
    {
        seenEventPlaying = false;
    }

}
