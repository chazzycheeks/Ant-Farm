using System.Collections;
using UnityEngine;

public class TriggerEffectsDoor : MonoBehaviour {
    Coroutine delayRoutine;
    [Tooltip("Delay for the entity to leave the room after the player exited the room")]
    [SerializeField] float delay = .5f;
    public void ActivateFade() {
        CameraUIAnimationController.Instance.TriggerFade();
    }

    public void LeaveRoomEntity() {
        if (EntityStateTriggers.Instance.isActivelyChasing && delayRoutine == null && EntityStateTriggers.Instance.hasLeftRoom == false) {
            delayRoutine = StartCoroutine(LeaveDelay());
        }
        EntityStateTriggers.Instance.repeatImmunity = true;
    }

    public void StopEntitySurvey() {
        EntityStateTriggers.Instance.hasLeftRoom = true;
    }

    IEnumerator LeaveDelay() {
        yield return new WaitForSeconds(delay);
        EntityStateTriggers.Instance.hasLeftRoom = true;
        delayRoutine = null;
    }

}
