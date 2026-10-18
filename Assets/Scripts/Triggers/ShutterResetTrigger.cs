using UnityEngine;

public class ShutterResetTrigger : MonoBehaviour
{
    [SerializeField] private ShutterTrigger shutterTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (shutterTrigger.shutterTriggerEntered == true && collision.CompareTag("Player"))
        {
            shutterTrigger.ResetShutter();
            Debug.Log("shutter reset");
        }
    }
}
