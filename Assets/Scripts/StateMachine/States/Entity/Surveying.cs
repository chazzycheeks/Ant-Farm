using UnityEngine;

public class Surveying : State {
    public State spottingState;
    protected override void Awake() {
        base.Awake();
    }
    public override void DefaultState() {
    }

    public override void ExitState() {
    }

    public override State RunCurrentState() {
        if (entityStateTriggers.isSpotting) {
            return spottingState;
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
        }
    }
}
