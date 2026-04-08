using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;

public class VisibilityManager : MonoBehaviour
{
    public bool isVisible = false;
    public float visibilityTrigger = 2.5f;
    public float visibilityTimer;
    public UnityEvent onSeen;
    private bool seenEventPlaying = false;
    public Vector2 lastPlayerPositionSeen;
    Transform playerT;
    GameObject entity;
    StateManager stateManager;
    EntityStateTriggers entityTriggers;
    private void Awake() {
        entity = GameObject.FindGameObjectWithTag("Entity");

        stateManager = entity.GetComponent<StateManager>();
        entityTriggers = entity.GetComponent<EntityStateTriggers>();
    }
    private void Start() {
        
    }

    void Update()
    {
        if (isVisible == true)
        {
            if (playerT == null) {
                playerT = GameObject.FindGameObjectWithTag("Player").transform;
            }
            if(stateManager.GetCurrentState() is Roaming) {
                entityTriggers.noticedPlayer = true;
            }
            lastPlayerPositionSeen = playerT.position;
            
            UpdateTimer();
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
            //onSeen?.Invoke();
            Debug.Log("Seen");
        }
    }

    public void ResetOnSeenEvent()
    {
        seenEventPlaying = false;
    }

}
