using UnityEngine;

public class TriggerNarativeState : MonoBehaviour
{
    StateManager stateManager;
    PatrolLocomotionController patrolLocomotionController;
    EntityStateTriggers entityStateTriggers;

    [SerializeField]Transform narativeStuckSpot;
    bool hasTriggered = false;
    private void Start() {
        stateManager = FindFirstObjectByType<StateManager>();
        patrolLocomotionController = FindFirstObjectByType<PatrolLocomotionController>();
        entityStateTriggers = FindFirstObjectByType<EntityStateTriggers>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        
    }

    public void EnterNarativeState() {
        patrolLocomotionController.staticPointTransform = narativeStuckSpot;
        entityStateTriggers.narativeStuckInSpot = true;
        stateManager.SwapToNarativeStuck();
        hasTriggered = true;
    }
    private void OnTriggerExit2D(Collider2D collision) {
        
    }

    public void LeaveNarativeState() {
        if (hasTriggered) {
            Debug.Log("Leaving Narative Spot");
            entityStateTriggers.narativeStuckInSpot = false;
        }
    }
}
