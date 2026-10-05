using System.Collections;
using UnityEngine;

public class Surveying : State {
    
    [Tooltip("The spotting state we move to if the player gets spotted while surveying")]
    public State spottingState;
    [Tooltip("The roaming state we go back to if the player is not spotted")]
    public Roaming roamingState;
    [Tooltip("Used to check whether to return to survey path or ")]
    public bool isReturning = false;
    [SerializeField] float _gracePeriodTimerSpotting = 2f; // Timer the meter needs to hit before 
    [SerializeField] float _gracePeriodMeterAmplifier = 1f; // Affects how fast the meter fills up
    [SerializeField] float _gracePeriodMeter; // The meter of how much until the grace period is over
    [SerializeField] bool _isGracePeriodOver; // Shows whether the grace period is over or not
    float GracePeriodAmplifier {
        get {
            return entityStateTriggers.inFrontOfEntity ? 999f : _gracePeriodMeterAmplifier;
        }
    }
    Coroutine _gracePeriodCoroutine;
    public bool bDebugLog = false;
    protected override void Awake() {
        base.Awake();
    }
    public override void DefaultState() {
        if (!isReturning) {
            transform.root.position = patrolController.GetStartPointLocation(PathType.Surveying);
        }
        isReturning = false;
        patrolController.TogglePatrol(true, PathType.Surveying);

        entityStateTriggers.isActivelyChasing = true;
    }

    public override void ExitState() {
        if (_gracePeriodCoroutine != null) StopCoroutine(_gracePeriodCoroutine);
        _gracePeriodCoroutine = null;
        _isGracePeriodOver = false;
        _gracePeriodMeter = 0;
        patrolController.TogglePatrol(false, PathType.Surveying);
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
        // If the player has left room then go to roaming state which is static
        if (entityStateTriggers.hasLeftRoom) {
            if (bDebugLog) Debug.Log("Failed to spot player, swap to roaming");
            roamingState.isReturning = true;
            patrolController.TogglePatrol(false, PathType.Surveying);
            entityStateTriggers.hasLeftRoom = false;
            return roamingState;
        }

        return this;
    }
    public override void HandleAnimationEvent(string eventId) {
        if (eventId == "Submerged") {
            patrolController.TogglePatrol(false, PathType.Surveying);
            transform.root.position = patrolController.GetCurrentPointLocation(PathType.Surveying);
            animator.Play("Emerge");

        } else if (eventId == "FinishedEmerging") {
            patrolController.TogglePatrol(true, PathType.Surveying);
            entityStateTriggers.transitioned = true;
        }
    }

    IEnumerator GraceTimerSpotting() {
        while (_gracePeriodMeter < _gracePeriodTimerSpotting) {
            if (entityStateTriggers.isSpotting) {
                _gracePeriodMeter += Time.deltaTime * GracePeriodAmplifier;
            } else {
                _gracePeriodMeter = Mathf.Clamp(_gracePeriodMeter - Time.deltaTime, 0, _gracePeriodTimerSpotting);
            }

            yield return null;
        }
        if (bDebugLog) Debug.Log("Grace period over");
        _isGracePeriodOver = true;
    }

}
