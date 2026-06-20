    using UnityEngine;

public class Roaming : State {
    [Tooltip("The survey state we use once we spot a player briefly")]
    public State surveyState;
    [Tooltip("Used to check whether the entity should roam in a reverse path or not")]
    public bool isReturning = false;
    protected override void Awake() {
        base.Awake();
    }
    public override void DefaultState() {
        if (isReturning) {
            animator.Play("Submerge");
            isReturning = false;
        } else {
            animator.Play("Emerge");
        }
        
    }

    public override void ExitState() {
        patrolController.TogglePatrol(false, PathType.Roaming);
    }

    public override State RunCurrentState() {
        if(entityStateTriggers.noticedPlayer) {
            Debug.Log("Noticed player, swaping to survey");
            return surveyState;
        } else {
            return this;
        }
            
    }
    public override void HandleAnimationEvent(string eventId) {
        //if(eventId == "Finished") {
        //    patrolController.TogglePatrol(true, PathType.Roaming);
        //    entityStateTriggers.transitioned = true;
        //    entityStateTriggers.isSpottable = true;
        //}

        if (eventId == "Submerged") {
            transform.root.position = patrolController.GetCurrentPointLocation(PathType.Roaming);
            animator.Play("Emerge");

        } else if (eventId == "FinishedEmerging") {
            patrolController.TogglePatrol(true, PathType.Roaming);
            entityStateTriggers.transitioned = true;
            entityStateTriggers.isSpottable = true;
        }
    }
}
