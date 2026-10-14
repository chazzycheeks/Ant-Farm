using MoreMountains.CorgiEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VisibilityManager : MonoBehaviour
{
    public bool _isVisible {
        get {
            return coverObjects.Count == 0;
        }
    }
    public bool isVisible => _isVisible;
    public float visibilityTrigger = 2.5f;
    public float caughtTrigger = 5f;
    public float visibilityTimer;
    
    [SerializeField]bool debuggingDistance = false;
    public UnityEvent onCaught;
    private bool seenEventPlaying = false;
    public Vector2 lastPlayerPositionSeen;
    Transform playerT;
    GameObject entity;
    StateManager stateManager;
    EntityStateTriggers entityTriggers;
    static VisibilityManager instance;

    private float visiblityPercentage;
    [SerializeField] private Image vignette;
    [SerializeField] private GameObject blackCut;

    [Tooltip("Grace period variable to be used while transitioning between rooms.")]
    public bool CanBeSpotted = true;

    [Tooltip("How long is the player immune to spotting(used when transitioning between rooms).")]
    [SerializeField] float spottingPeriodTimeImmunity = 10f;

    /// <summary>
    /// Timer for grace period where the player cannot be seen
    /// </summary>
    float gracePeriodTimer = 0;
    Coroutine timerCoroutineGraceSpotting;

    public List<PlayerHider> coverObjects;
    public static VisibilityManager Instance {
        get {  return instance; }
    }

    public void OnNewRoomEnter() {
        if(timerCoroutineGraceSpotting == null) {
            gracePeriodTimer = 0f;
            timerCoroutineGraceSpotting = StartCoroutine(gracePeriodSpottingImmunity());
        }
    }

    IEnumerator gracePeriodSpottingImmunity() {
        CanBeSpotted = false;
        while (gracePeriodTimer < spottingPeriodTimeImmunity) {
            gracePeriodTimer += Time.deltaTime;
            yield return null;
        }
        CanBeSpotted = true;
        timerCoroutineGraceSpotting = null;
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
        vignette.color = new Color(vignette.color.r, vignette.color.g, vignette.color.b, 0f);

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
        if (_isVisible == true && RoomTracker.Instance._canEntitySurvey == true) {

            //If it's not submerged or emerged then try to feed visual input to the entity to act accordingly
            if (entityTriggers.transitioned == true && CanBeSpotted)
            {
                if (stateManager.GetCurrentState() is RoamingStatic)
                {
                    entityTriggers.noticedPlayer = true;
                }

                if (stateManager.GetCurrentState() is Surveying && entityTriggers.sameRoomAndChasing)
                {
                    entityTriggers.isSpotting = true;
                }

                if (stateManager.GetCurrentState() is Spotting && entityTriggers.sameRoomAndChasing)
                {
                    entityTriggers.isSpotting = true;

                    if(visibilityTimer < visibilityTrigger) visibilityTimer = visibilityTrigger;
                }


                if (entityTriggers.sameRoomAndChasing || stateManager.GetCurrentState() is StaticEntityState) UpdateTimer();

            }

            lastPlayerPositionSeen = playerT.position;

        } else {
            //Toggle the visual input off so mechanics work accordingly
            if(entityTriggers.transitioned) {
                entityTriggers.isSpotting = false;
                entityTriggers.noticedPlayer = false;
            }
            
        }
        /*if (seenEventPlaying == false) { }*/
        visiblityPercentage = visibilityTimer / visibilityTrigger;
        float currentAlpha = vignette.color.a;
        float newAlpha = Mathf.Lerp(currentAlpha,visiblityPercentage, Time.deltaTime*8f);
        vignette.color = new Color(vignette.color.r,vignette.color.g,vignette.color.b,newAlpha);
        CheckIfCaught();
    }

    public void UpdateTimer()
    {
        visibilityTimer += Time.deltaTime * 1f;

        //if (seenEventPlaying == false)
        //{
        //    visibilityTimer += Time.deltaTime * 1f;
        //}
        //if (visibilityTimer >= visibilityTrigger)
        //{
        //    visibilityTimer = 0f;
        //    caughtTimer += Time.deltaTime * 1f;
        //}
    }
   

    public void CheckIfCaught()
    {
        //if (caughtTimer >= caughtTrigger && seenEventPlaying == false)
        if (visibilityTimer >= caughtTrigger && seenEventPlaying == false)
        {
            visibilityTimer = 0f;
            seenEventPlaying = true;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Health>().Damage(300f, player, 0f, 0f, Vector3.up);
            player.GetComponent<CorgiController>().GravityActive(false);
            player.GetComponentInChildren<Animator>().SetBool("Awakening", true);
            Debug.Log("Dead");
            StartCoroutine(CutToBlack());
        }
    }

    public void ResetOnSeenEvent()
    {
        seenEventPlaying = false;
    }

    private IEnumerator CutToBlack()
    {
        yield return new WaitForSeconds(4f);
        blackCut.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        blackCut.SetActive(false);
        yield return new WaitForSeconds(1f);
        stateManager.ResetToDefault();
    }








}
