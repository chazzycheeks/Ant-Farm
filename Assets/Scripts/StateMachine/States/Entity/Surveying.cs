using System.Collections;
using UnityEngine;

public class Surveying : State {
    [HideInInspector]public float surveyingTimer;
    [Tooltip("The timer it takes to finish surveying a room")]
    public float timeToSurvey = 20;
    bool isTimeOver = false;
    [Tooltip("The spotting state we move to if the player gets spotted while surveying")]
    public State spottingState;
    [Tooltip("The roaming state we go back to if the player is not spotted")]
    public Roaming roamingState;
    [Tooltip("Used to check whether the entity should survey in a reverse path or not")]
    public bool isReturning = false;
    protected override void Awake() {
        base.Awake();
    }
    public override void DefaultState() {
        entityStateTriggers.noticedPlayer = false;
        entityStateTriggers.transitioned = false;
        entityStateTriggers.isSpotting = false;
        if (!isReturning) {
            animator.Play("Submerge");
        } else {
            isReturning = false;
        }
        
        
    }

    public override void ExitState() {
        StopCoroutine(TimerSpoting());
        isTimeOver = false;
        surveyingTimer = 0;
    }

    public override State RunCurrentState() {
        if (entityStateTriggers.isSpotting) {
            return spottingState;
        }

        if (isTimeOver) {
            roamingState.isReturning = true;
            patrolController.TogglePatrol(false, PathType.Surveying);
            return roamingState;
        }
            return this;
    }
    public override void HandleAnimationEvent(string eventId) {
        if(eventId == "Submerged") {
            patrolController.TogglePatrol(false, PathType.Surveying);
            transform.root.position = patrolController.GetCurrentPointLocation(PathType.Surveying);
            animator.Play("Emerge");

        } else if (eventId == "FinishedEmerging") {
            patrolController.TogglePatrol(true, PathType.Surveying);
            StartCoroutine(TimerSpoting());
            entityStateTriggers.transitioned = true;
            entityStateTriggers.isSpottable = true;
        }
    }

    IEnumerator TimerSpoting() {
        while (surveyingTimer < timeToSurvey) {
            if (!entityStateTriggers.isSpotting) {
                surveyingTimer += Time.deltaTime;
            } else {
                surveyingTimer = 0;
            }

            yield return null;
        }
        isTimeOver = true;
    }
}
