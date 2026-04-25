using System.Collections;
using UnityEngine;

public class Surveying : State {
    public float surveyingTimer;
    public float timeToSurvey = 20;
    bool isTimeOver = false;
    public State spottingState;
    public State roamingState;
    protected override void Awake() {
        base.Awake();
    }
    public override void DefaultState() {
        entityStateTriggers.noticedPlayer = false;
        entityStateTriggers.transitioned = false;
        entityStateTriggers.isSpotting = false;
        animator.Play("Submerge");
        
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
            return roamingState;
        }
            return this;
    }
    public override void HandleAnimationEvent(string eventId) {
        if(eventId == "Submerged") {
            transform.root.position = patrolController.GetCurrentPointLocation(PathType.Surveying);
            animator.Play("Emerge");

        } else if (eventId == "FinishedEmerging") {
            patrolController.TogglePatrol(true, PathType.Surveying);
            entityStateTriggers.transitioned = true;
            entityStateTriggers.isSpottable = true;
            StartCoroutine(TimerSpoting());
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
