    using UnityEngine;

public class Roaming : State {
    PatrolLocomotionController patrolController;
    private void Awake() {
        patrolController = GetComponentInParent<PatrolLocomotionController>();
        if(patrolController == null) {
            Debug.Log("Patrol Locomotion Controller missing");
            return;
        }
    }
    public override void DefaultState() {
        patrolController.TogglePatrol(true);
    }

    public override void ExitState() {
        patrolController.TogglePatrol(false);
    }

    public override State RunCurrentState() {
        return this;
    }
}
