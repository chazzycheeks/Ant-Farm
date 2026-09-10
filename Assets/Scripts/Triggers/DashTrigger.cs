using MoreMountains.CorgiEngine;
using UnityEngine;

public class DashTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CharacterDash>().enabled = true;
            gameObject.SetActive(false);
        }
    }
}
