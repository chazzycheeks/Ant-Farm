using UnityEngine;

public class EndRoomSevenTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.EndRoomSevenMusic();
        }
    }
}
