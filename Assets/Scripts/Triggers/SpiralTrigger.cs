using System.Linq;
using UnityEngine;
using FMODUnity;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class SpiralTrigger : MonoBehaviour
{
    [SerializeField] private GameObject wallsIn;
    [SerializeField] private GameObject wallsOut;
    [SerializeField] private GameObject spiralArm;
    private bool spiralTriggerEntered = false;
    public Animator shutterAnims;

    [SerializeField] private PlayableDirector cutsceneR4B;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spiralTriggerEntered == false && collision.CompareTag("Player"))
        {
            /*RemoveWalls();
            ActivateShutters();*/
            cutsceneR4B.Play();
            AudioManager.instance.PlaySpiralMusic();
        }
    }

    public void RemoveWalls()
    {
        wallsIn.SetActive(false);
        wallsOut.SetActive(true);
        spiralArm.SetActive(true);
        spiralTriggerEntered = true;

    }

    public void ActivateShutters()
    {

        foreach (var foundObject in FindObjectsByType<Animator>(sortMode: FindObjectsSortMode.InstanceID))
        {
            Debug.Log(foundObject.name);
            if (foundObject.CompareTag("Shutter"))
            {
                Debug.Log("TAG");
                foundObject.SetTrigger("close");
            }
        }
    }
}
