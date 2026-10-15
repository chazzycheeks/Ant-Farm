using MoreMountains.CorgiEngine;
using System.Collections;
using UnityEngine;

public class DashTrigger : MonoBehaviour
{
    [SerializeField] private Animator stingerArmDash;
    [SerializeField] private Animator cameraZoom;
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
        player.GetComponentInChildren<Animator>().SetBool("Startled", true);
        yield return new WaitForSeconds(3f);
        player.GetComponentInChildren<Animator>().SetBool("Startled", false);
        cameraZoom.SetTrigger("zoomingOut");
        CameraUIAnimationController.Instance.TriggerBlackBarsOut();
        /*player.GetComponent<CharacterHorizontalMovement>().AbilityPermitted = true;*/
        player.GetComponent<Character>().UnFreeze();
        gameObject.SetActive(false);

    }

    public void StingCouroutineDash()
    {
        StartCoroutine(FreezeAndStingDash());
    }
}
