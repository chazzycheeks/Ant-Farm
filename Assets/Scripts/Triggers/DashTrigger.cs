using MoreMountains.CorgiEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;

public class DashTrigger : MonoBehaviour
{
    [SerializeField] private Animator stingerArmDash;
    [SerializeField] private Animator cameraZoom;

    [SerializeField] private EventReference stingerSound;
    [SerializeField] private EventReference injectionSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CharacterDash>().enabled = true;
        }
    }
    private IEnumerator FreezeAndStingDash()
    {
        cameraZoom.SetTrigger("zoomingIn");
        CameraUIAnimationController.Instance.TriggerBlackBarsIn();
        stingerArmDash.SetTrigger("stinging");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Character>().Freeze();
        /*player.GetComponent<CharacterHorizontalMovement>().AbilityPermitted = false;*/
        /*player.GetComponent<CorgiController>().SetForce(Vector2.zero);*/
        player.GetComponentInChildren<Animator>().SetTrigger("StartCutscene");
        player.GetComponentInChildren<Animator>().SetBool("Startled", true);
        AudioManager.instance.PlayOneShotSound(stingerSound, this.transform.position);
        AudioManager.instance.PlayOneShotSound(injectionSound, this.transform.position);
        yield return new WaitForSeconds(2.5f);
        cameraZoom.SetTrigger("zoomingOut");
        CameraUIAnimationController.Instance.TriggerBlackBarsOut();
        yield return new WaitForSeconds(0.5f);
        player.GetComponentInChildren<Animator>().SetBool("Startled", false);
        CameraUIAnimationController.Instance.TriggerDashScreen();
        /*player.GetComponent<CharacterHorizontalMovement>().AbilityPermitted = true;*/
        //player.GetComponent<Character>().UnFreeze();
        gameObject.SetActive(false);

    }

    public void StingCouroutineDash()
    {
        StartCoroutine(FreezeAndStingDash());
    }
}
