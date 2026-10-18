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

    [SerializeField] GameObject endScreen;

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

    public void TriggerBlackBarsIn()
    {
        blackBarUpAnim.SetTrigger("In");
        blackBarDownAnim.SetTrigger("In");
    }
    public void TriggerBlackBarsOut()
    {
        blackBarUpAnim.SetTrigger("Out");
        blackBarDownAnim.SetTrigger("Out");
    }

    public void TriggerEndScreen()
    {
        endScreen.SetActive(true);
    }
}
