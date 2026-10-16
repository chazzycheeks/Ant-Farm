using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spotting : State {
    [HideInInspector] public float spottingTimer;
    [Tooltip("The amount of time it takes to spot the player")]
    public float timeToSpot = 5;
    bool isTimeOver = false;
    [Tooltip("The surveying state we return to if the entity fails spotting")]
    [SerializeField] Surveying surveyingState;

    [SerializeField] Roaming roamingState;
    public bool bDebugLog = false;
    protected override void Awake() {
        base.Awake();
        if(surveyingState == null) {
            Debug.LogWarning("Surveying state is missing from Spotting state");
        }
    }
    public override void DefaultState() {
        patrolController.TogglePatrol(false, PathType.Spotting);
        spottingTimer = 0;
        isTimeOver = false;
        StartCoroutine(TimerSpoting());
        entityStateTriggers.isActivelyChasing = true;
    }

    public override void ExitState() {
        spottingTimer = 0;
        isTimeOver = false;

        StopCoroutine(TimerSpoting());
    }

    public override State RunCurrentState() {
        // If the timer for spotting is over then return to surveying
        if (entityStateTriggers.hasLeftRoom) {
            roamingState.isReturning = true;
            patrolController.TogglePatrol(false, PathType.Surveying);
            entityStateTriggers.hasLeftRoom = false;
            return roamingState;
        }

        if (isTimeOver) {
            if(bDebugLog)Debug.Log("Failed to spot player, swapping to survey");
            patrolController.ReturnToPath(PathType.Surveying);
            patrolController.TogglePatrol(true, PathType.Surveying);
            surveyingState.isReturning = true;
            return surveyingState;
        } else {
            return this;
        }
        
    }

    public override void HandleAnimationEvent(string eventId) {
        if (eventId == "FinishedEmerging") {
            entityStateTriggers.transitioned = true;
        }
    }

    IEnumerator TimerSpoting() {
        while (spottingTimer < timeToSpot) {
            if (!entityStateTriggers.isSpotting) {
                spottingTimer += Time.deltaTime;
            } else {
                spottingTimer = 0;
            }
            
            yield return null;
        }
        isTimeOver = true;
    }
}
