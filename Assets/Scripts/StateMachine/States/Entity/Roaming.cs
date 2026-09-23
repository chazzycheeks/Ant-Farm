    using UnityEngine;

public class Roaming : State {
    [Tooltip("The survey state we use once we spot a player briefly")]
    public State surveyState;
    [Tooltip("Whether it returns from surveying or not, if it does then player the submerge animation")]
    public bool isReturning = false;
    public bool bDebugLog = false;
    protected override void Awake() {
        base.Awake();
    }
    public override void DefaultState() {
        entityStateTriggers.transitioned = false;
        if (isReturning) {
            animator.Play("Submerge");
            isReturning = false;
        } else {
            animator.Play("Emerge");
        }
        entityStateTriggers.isActivelyChasing = false;
        patrolController.OutOfChaseUpdate();
    }

    public override void ExitState() {
        patrolController.TogglePatrol(false, PathType.Roaming);
    }

    public override State RunCurrentState() {
        if(entityStateTriggers.noticedPlayer) {
            if(bDebugLog)Debug.Log("Noticed player, swaping to survey");
            return surveyState;
        } else {
            return this;
        }
            
    }
    public override void HandleAnimationEvent(string eventId) {
        if (eventId == "Submerged") {
            transform.root.position = patrolController.GetCurrentPointLocation(PathType.Roaming);
            animator.Play("Emerge");

        } else if (eventId == "FinishedEmerging") {
            patrolController.TogglePatrol(true, PathType.Roaming);
            entityStateTriggers.transitioned = true;
        }
    }
}
