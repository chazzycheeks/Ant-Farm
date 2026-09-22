using MoreMountains.CorgiEngine;
using UnityEngine;

public class RunTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CharacterRun>().AbilityPermitted = true;
            collision.GetComponent<CharacterJump>().JumpHeight = 3f;
            collision.GetComponent<CharacterHorizontalMovement>().WalkSpeed = 6f;
            gameObject.SetActive(false);
        }
    }
}
