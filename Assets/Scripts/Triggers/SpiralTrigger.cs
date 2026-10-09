using System.Linq;
using UnityEngine;

public class SpiralTrigger : MonoBehaviour
{
    [SerializeField] private GameObject wallsIn;
    [SerializeField] private GameObject wallsOut;
    [SerializeField] private GameObject spiralArm;
    [SerializeField] private GameObject dropLadder;
    private bool spiralTriggerEntered = false;
    public Animator shutterAnims;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spiralTriggerEntered == false && collision.CompareTag("Player"))
        {
            RemoveWalls();
            ActivateShutters();
            
        }
    }

    public void RemoveWalls()
    {
        wallsIn.SetActive(false);
        wallsOut.SetActive(true);
        spiralArm.SetActive(true);
        dropLadder.SetActive(true);
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
