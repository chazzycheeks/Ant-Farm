using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ShutterTrigger : MonoBehaviour
{
    [SerializeField] private Animator shutterArt;
    public bool shutterTriggerEntered = false;
    [SerializeField] private GameObject resetTriggers;

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (shutterTriggerEntered == false && collision.CompareTag("Player"))
        {
            StartShutter();
            AudioManager.instance.PlayWallMusic();
        }
    }

    public void StartShutter()
    {
        shutterArt.SetBool("Opening", true);
        shutterTriggerEntered = true;
        resetTriggers.SetActive(true);

    }

    public void ResetShutter()
    {
        shutterArt.SetBool("Opening", false);
        shutterTriggerEntered = false;
    }
}
