using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StaticEntityState : State {
    StateManager stateManager;

    bool isSectionOver = false;

    [Tooltip("The surveying state we return to if the entity fails spotting")]
    [SerializeField] Surveying surveyingState;
    [SerializeField] Roaming roamingState;

    public bool bDebugLog = false;
    protected override void Awake() {
        base.Awake();
        if(surveyingState == null) {
            Debug.LogWarning("Surveying state is missing from Spotting state");
        }
        stateManager = GetComponentInParent<StateManager>();
    }
    public override void DefaultState() {
        patrolController.TogglePatrol(false, PathType.Static);
        
    }

    public override void ExitState() {
        
    }

    public override State RunCurrentState() {
        if(entityStateTriggers.narativeStuckInSpot == false) {
            if(stateManager.LastStateUsed is Surveying) {
                patrolController.ReturnToPath(PathType.Surveying);
                patrolController.TogglePatrol(true, PathType.Surveying);
                return surveyingState;
            } else {
                patrolController.ReturnToPath(PathType.Roaming);
                patrolController.TogglePatrol(true, PathType.Roaming);
                return roamingState;
            }
        }
            return this;
    }

    public override void HandleAnimationEvent(string eventId) {
        if (eventId == "FinishedEmerging") {
            entityStateTriggers.transitioned = true;
        }
    }

}
