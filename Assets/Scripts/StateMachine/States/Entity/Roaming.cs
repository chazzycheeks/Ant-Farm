    using UnityEngine;

public class Roaming : State {
    public State surveyState;
    protected override void Awake() {
        base.Awake();
    }
    public override void DefaultState() {
        animator.Play("Emerge");
    }

    public override void ExitState() {
        patrolController.TogglePatrol(false, PathType.Roaming);
        entityStateTriggers.noticedPlayer = false;
        entityStateTriggers.transitioned = false;
        animator.Play("Submerge");
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
        if(eventId == "Finished") {
            patrolController.TogglePatrol(true, PathType.Roaming);
            entityStateTriggers.transitioned = true;
        }
    }
}
