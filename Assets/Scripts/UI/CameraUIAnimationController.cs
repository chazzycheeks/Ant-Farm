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
    [SerializeField] GameObject dashScreen;

    [SerializeField] GameObject checkpointUI;
    Animator checkpointUIAnim;

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

        checkpointUIAnim = checkpointUI.GetComponent<Animator>();
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

    public void TriggerDashScreen()
    {
        dashScreen.SetActive(true);
    }

    public void TriggerCheckpointUI()
    {
        checkpointUIAnim.SetTrigger("Activated");
    }
}
