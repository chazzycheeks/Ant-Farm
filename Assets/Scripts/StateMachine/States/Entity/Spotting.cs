using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spotting : State {
    public float spottingTimer;
    public float timeToSpot = 5;
    bool isTimeOver = false;
    [SerializeField] Surveying surveyingState;
    protected override void Awake() {
        base.Awake();
        if(surveyingState == null) {
            Debug.LogWarning("Surveying state is missing from Spotting state");
        }
    }
    public override void DefaultState() {
        patrolController.TogglePatrol(false, PathType.FreeMoving);
        spottingTimer = 0;
        isTimeOver = false;
        StartCoroutine(TimerSpoting());
    }

    public override void ExitState() {
        spottingTimer = 0;
        isTimeOver = false;
        StopCoroutine(TimerSpoting());
    }

    public override State RunCurrentState() {
        if (isTimeOver) {
            patrolController.ReturnToPath(PathType.Surveying);
            surveyingState.isReturning = true;
            return surveyingState;
        } else {
            return this;
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
