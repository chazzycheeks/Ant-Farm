using MoreMountains.CorgiEngine;
using UnityEngine;

public class RunTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CharacterRun>().enabled = true;
            collision.GetComponent<CharacterJump>().enabled = true;
            gameObject.SetActive(false);
        }
    }
}
