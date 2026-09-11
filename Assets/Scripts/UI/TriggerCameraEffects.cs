using UnityEngine;

public class TriggerCameraEffects : MonoBehaviour {
    public void ActivateFade() {
        CameraUIAnimationController.Instance.TriggerFade();
    }
}
