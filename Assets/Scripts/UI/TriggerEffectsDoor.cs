using UnityEngine;

public class TriggerEffectsDoor : MonoBehaviour {
    public void ActivateFade() {
        CameraUIAnimationController.Instance.TriggerFade();
    }

    public void LeaveRoomEntity() {
        if (EntityStateTriggers.Instance.isActivelyChasing) {
            EntityStateTriggers.Instance.hasLeftRoom = true;
        }
        EntityStateTriggers.Instance.repeatImmunity = true;
    }

}
