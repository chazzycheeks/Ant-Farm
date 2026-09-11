using UnityEngine;

public class CameraUIAnimationController : MonoBehaviour
{
    static CameraUIAnimationController instance;
    public static CameraUIAnimationController Instance {
        get { return instance;  }
    }
    [SerializeField] GameObject blackoutObject;
    Animator blackoutObjectAnimator;
    string fadeInAnim = "FadeIn";

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        blackoutObjectAnimator = blackoutObject.GetComponent<Animator>();
    }
    public void TriggerFade() {
        blackoutObjectAnimator.SetTrigger("FadeTrigger");
    }
}
