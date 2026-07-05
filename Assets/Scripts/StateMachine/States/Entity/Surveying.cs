using System.Collections;
using UnityEngine;

public class Surveying : State {
    public float surveyingTimer;
    [Tooltip("The timer it takes to finish surveying a room")]
    public float timeToSurvey = 20;
    bool isTimeOver = false;
    [Tooltip("The spotting state we move to if the player gets spotted while surveying")]
    public State spottingState;
    [Tooltip("The roaming state we go back to if the player is not spotted")]
    public Roaming roamingState;
    [Tooltip("Used to check whether the entity is returning from Spotting or not to not play the submerge animation")]
    public bool isReturning = false;

    [SerializeField] float _gracePeriodTimerSpotting = 2f; // Timer the meter needs to hit before 
    [SerializeField] float _gracePeriodMeterAmplifier = 1f; // Affects how fast the meter fills up
    [SerializeField] float _gracePeriodMeter; // The meter of how much until the grace period is over
    [SerializeField] bool _isGracePeriodOver; // Shows whether the grace period is over or not

    private Coroutine currentTimerRoutine;
    Coroutine _gracePeriodCoroutine;
    public bool bDebugLog = false;
    protected override void Awake() {
        base.Awake();
    }
    public override void DefaultState() {
        if (!isReturning) {
            StartTimer();
            entityStateTriggers.transitioned = false;
            animator.Play("Submerge");
        } else {
            isReturning = false;
        }
    }

    public override void ExitState() {
        StopCoroutine(currentTimerRoutine);
        StopCoroutine(_gracePeriodCoroutine);
        isTimeOver = false;
        _gracePeriodCoroutine = null;
        _isGracePeriodOver = false;
        _gracePeriodMeter = 0;
        surveyingTimer = 0;
    }

    public override State RunCurrentState() {
        // If it spots the player then go to spot
        if (entityStateTriggers.isSpotting && _gracePeriodCoroutine == null) {
            _gracePeriodCoroutine = StartCoroutine(GraceTimerSpotting());
        }

        if (_isGracePeriodOver) {
            if (bDebugLog) Debug.Log("Spotted player, swapping to spotting");
            return spottingState;
        }
        // If the timer for surveying over then return to roaming
        if (isTimeOver) {
            if(bDebugLog)Debug.Log("Failed to spot player, swap to roaming");
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

        } else if (eventId == "FinishedEmerging")
        {
            patrolController.TogglePatrol(true, PathType.Surveying);
            entityStateTriggers.transitioned = true;
        }
    }

    private void StartTimer()
    {
        if (currentTimerRoutine != null)
        {
            StopCoroutine(currentTimerRoutine);
        }

        currentTimerRoutine = StartCoroutine(TimerSpoting());
    }
    IEnumerator GraceTimerSpotting() {
        while (_gracePeriodMeter < _gracePeriodTimerSpotting) {
            if (entityStateTriggers.isSpotting) {
                _gracePeriodMeter += Time.deltaTime * _gracePeriodMeterAmplifier;
            } else {
                _gracePeriodMeter = Mathf.Clamp(_gracePeriodMeter - Time.deltaTime, 0, _gracePeriodTimerSpotting);
            }

            yield return null;
        }
        if (bDebugLog) Debug.Log("Grace period over");
        _isGracePeriodOver = true;
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
