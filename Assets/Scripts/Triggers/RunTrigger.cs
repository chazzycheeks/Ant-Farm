using MoreMountains.CorgiEngine;
using System.Collections;
using UnityEngine;

public class RunTrigger : MonoBehaviour
{
    [SerializeField] private Animator stingerArm;
    [SerializeField] private Animator cameraZoom;

    private void Awake()
    {
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            collision.GetComponent<CharacterRun>().AbilityPermitted = true;
            collision.GetComponent<CharacterJump>().JumpHeight = 3f;
            collision.GetComponent<CharacterHorizontalMovement>().WalkSpeed = 6f;
            
        }
    }
    private IEnumerator FreezeAndSting()
    {
        cameraZoom.SetTrigger("zoomingIn");
        CameraUIAnimationController.Instance.TriggerBlackBarsIn();
        stingerArm.SetTrigger("stinging");
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

    public void StingCouroutine()
    {
        StartCoroutine(FreezeAndSting());
    }

}
