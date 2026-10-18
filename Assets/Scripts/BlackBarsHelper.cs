using MoreMountains.CorgiEngine;
using UnityEngine;

public class BlackBarsHelper : MonoBehaviour
{
    public void ShowBlackBars()
    {
        CameraUIAnimationController.Instance.TriggerBlackBarsIn();
        PlayerController.instance.GetComponent<Character>().Freeze();
    }

    public void HideBlackBars()
    {
        CameraUIAnimationController.Instance.TriggerBlackBarsOut();
        PlayerController.instance.GetComponent<Character>().UnFreeze();
    }

    public void JustFreeze()
    {
        PlayerController.instance.GetComponent<Character>().Freeze();
    }

    public void JustUnFreeze()
    {
        PlayerController.instance.GetComponent<Character>().UnFreeze();
    }

    public void TurnOnEndScreen()
    {
        PlayerController.instance.GetComponent<Character>().Freeze();
        CameraUIAnimationController.Instance.TriggerEndScreen();
    }

    public void PlayIdleAnim()
    {
        PlayerController.instance.GetComponent<Animator>().SetBool("Idle", true);
    }
}
