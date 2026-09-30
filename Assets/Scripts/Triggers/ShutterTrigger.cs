using UnityEngine;

public class ShutterTrigger : MonoBehaviour
{
    [SerializeField] private GameObject shutter;
    [SerializeField] private GameObject shutterArt;
    private bool shutterTriggerEntered = false;

    private void Start()
    {
        shutter.GetComponent<Animator>().enabled = false;
        shutterArt.GetComponent<Animator>().enabled = false;


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
        shutterArt.GetComponent<Animator>().enabled = true;
        shutterTriggerEntered = true;

    }
}
