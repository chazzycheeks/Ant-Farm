using System.Collections;
using UnityEngine;

public class TriggerNarativeState : MonoBehaviour
{
    StateManager stateManager;
    PatrolLocomotionController patrolLocomotionController;
    EntityStateTriggers entityStateTriggers;

    [SerializeField]Transform narativeStuckSpot;
    bool hasTriggered = false;

    [Tooltip("Delay for how long till you trigger the leave logic")]
    [SerializeField] float delayLeave = 3f;
    Coroutine delayCoroutine;
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
        if (hasTriggered && delayCoroutine == null) {
            Debug.Log("Leaving Narative Spot");
            delayCoroutine = StartCoroutine(DelayLeaveRoutine());
        }
    }

    IEnumerator DelayLeaveRoutine() {
        yield return new WaitForSecondsRealtime(delayLeave);
        entityStateTriggers.narativeStuckInSpot = false;
        delayCoroutine = null;
    }
}
