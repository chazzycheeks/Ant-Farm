using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spotting : State {
    public float timer;
    public float timeToSpot = 10;
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
        timer = 0;
        isTimeOver = false;
        StartCoroutine(TimerSpoting());
    }

    public override void ExitState() {
        timer = 0;
        isTimeOver = false;
    }

    public override State RunCurrentState() {
        if (isTimeOver) {
            patrolController.ReturnToPath(PathType.Surveying);
            return surveyingState;
        } else {
            return this;
        }
        
    }

    IEnumerator TimerSpoting() {
        while (timer < timeToSpot) {
            timer += Time.deltaTime;
            yield return null;
        }
        isTimeOver = true;
    }
}
