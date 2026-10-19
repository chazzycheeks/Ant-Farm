using UnityEngine;
using FMODUnity;

public class R3DoorTrigger : MonoBehaviour
{
    [SerializeField] private Animator doorShutterAnim;
    private bool shutterIsTriggered = false;
    [SerializeField] private EventReference doorSound;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (shutterIsTriggered == false && collision.CompareTag("Player"))
        {
            RaiseShutter();
        }
    }

    public void RaiseShutter()
    {
        doorShutterAnim.SetTrigger("Open");
        shutterIsTriggered = true;
        AudioManager.instance.PlayOneShotSound(doorSound, this.transform.position);
    }
}
