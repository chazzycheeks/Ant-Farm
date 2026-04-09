    using UnityEngine;

public class Roaming : State {
    PatrolLocomotionController patrolController;
    public State surveyState;
    protected override void Awake() {
        base.Awake();
        patrolController = GetComponentInParent<PatrolLocomotionController>();
        if(patrolController == null) {
            Debug.Log("Patrol Locomotion Controller missing");
            return;
        }
    }
    public override void DefaultState() {
        animator.Play("Emerge");
    }

    public override void ExitState() {
        patrolController.TogglePatrol(false, PathType.Roaming);
        entityStateTriggers.noticedPlayer = false;
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
        }
    }
}
