using UnityEngine;

public class ShutterTrigger : MonoBehaviour
{
    [SerializeField] private GameObject shutter;
    private bool shutterTriggerEntered = false;

    private void Start()
    {
        shutter.GetComponent<Animator>().enabled = false;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (shutterTriggerEntered == false && collision.CompareTag("Player"))
        {
            StartShutter();
        }
    }

    public void StartShutter()
    {
        shutter.GetComponent<Animator>().enabled = true;
        shutterTriggerEntered = true;

    }
}
