using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ShutterTrigger : MonoBehaviour
{
    [SerializeField] private Animator shutter;
    [SerializeField] private Animator shutterArt;
    public bool shutterTriggerEntered = false;
    [SerializeField] private GameObject resetTriggers;
    [SerializeField] private PlayableDirector cutscene6A;

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
        cutscene6A.Play();
        shutter.SetBool("Opening", true);
        shutterArt.SetBool("Opening", true);
        shutterTriggerEntered = true;
        resetTriggers.SetActive(true);

    }

    public void ResetShutter()
    {
        shutter.SetBool("Opening", false);
        shutterArt.SetBool("Opening", false);
        shutterTriggerEntered = false;
    }
}
