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
        patrolController.TogglePatrol(true);
    }

    public override void ExitState() {
        patrolController.TogglePatrol(false);
        entityStateTriggers.noticedPlayer = false;
    }

    public override State RunCurrentState() {
        if(entityStateTriggers.noticedPlayer) {
            Debug.Log("Noticed player swaping to survey");
            return surveyState;
        } else {
            return this;
        }
            
    }
}
