using UnityEngine;

public class CameraUIAnimationController : MonoBehaviour
{
    static CameraUIAnimationController instance;
    public static CameraUIAnimationController Instance {
        get { return instance;  }
    }
    [SerializeField] GameObject blackoutObject;
    Animator blackoutObjectAnimator;

    [SerializeField] GameObject blackBarUp;
    Animator blackBarUpAnim;
    [SerializeField] GameObject blackBarDown;
    Animator blackBarDownAnim;

    string fadeInAnim = "FadeIn";

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        blackoutObjectAnimator = blackoutObject.GetComponent<Animator>();

        blackBarUpAnim = blackBarUp.GetComponent<Animator>();
        blackBarDownAnim = blackBarDown.GetComponent<Animator>();
    }
    public void TriggerFade() {
        blackoutObjectAnimator.SetTrigger("FadeTrigger");
    }

    public void TriggerBlackBarUpIn()
    {
        blackBarUpAnim.SetTrigger("In");
    }
    public void TriggerBlackBarUpOut()
    {
        blackBarUpAnim.SetTrigger("Out");
    }
    public void TriggerBlackBarDownIn()
    {
        blackBarDownAnim.SetTrigger("In");
    }
    public void TriggerBlackBarDownOut()
    {
        blackBarDownAnim.SetTrigger("Out");
    }
}
