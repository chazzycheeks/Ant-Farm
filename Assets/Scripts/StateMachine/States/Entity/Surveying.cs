using UnityEngine;

public class Surveying : State {
    PatrolLocomotionController patrolController;
    public State spottingState;
    protected override void Awake() {
        base.Awake();
        patrolController = GetComponentInParent<PatrolLocomotionController>();
        if (patrolController == null) {
            Debug.Log("Patrol Locomotion Controller missing");
            return;
        }
    }
    public override void DefaultState() {
    }

    public override void ExitState() {
        
    }

    public override State RunCurrentState() {
        return this;
    }
    public override void HandleAnimationEvent(string eventId) {
        if(eventId == "Submerged") {
            animator.Play("Emerge");
        } else if (eventId == "FinishedEmerging") {
            patrolController.TogglePatrol(true, PathType.Surveying);
        }
    }
}
