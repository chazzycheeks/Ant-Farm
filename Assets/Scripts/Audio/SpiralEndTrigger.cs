using UnityEngine;

public class SpiralEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.EndSpiralMusic();
        }
    }
}
