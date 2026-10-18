using MoreMountains.CorgiEngine;
using System.Collections;
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
/*        yield return new WaitForSeconds(1.25f);*/
       /* PlayerController.instance.GetComponent<Animator>().SetBool("Walking", false);*/
        PlayerController.instance.GetComponentInChildren<Animator>().SetBool("Cutscene", true);
    }

    /*public void IdleAnimCouroutine()
    {
        StartCoroutine(PlayIdleAnim());
    }*/
    public void StopIdleAnim()
    {
        PlayerController.instance.GetComponentInChildren<Animator>().SetBool("Cutscene", false);
    }

/*    public void TurnOnDashScreen()
    {
        CameraUIAnimationController.Instance.TriggerDashScreen();
    }*/
}
