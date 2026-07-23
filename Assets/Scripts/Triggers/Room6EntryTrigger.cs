using UnityEngine;

public class Room6EntryTrigger : MonoBehaviour
{
    [SerializeField] private GameObject wallOne;
    [SerializeField] private GameObject wallTwo;
    [SerializeField] private GameObject wallThree;
    [SerializeField] private GameObject wallFour;
    [SerializeField] private GameObject wallFive;
    [SerializeField] private GameObject wallSix;
    [SerializeField] private GameObject wallSeven;
    private bool entryTriggerEntered = false;

    private void Start()
    {
        wallOne.GetComponent<Animator>().enabled = false;
        wallTwo.GetComponent<Animator>().enabled = false;
        wallThree.GetComponent<Animator>().enabled = false;
        wallFour.GetComponent<Animator>().enabled = false;
        wallFive.GetComponent<Animator>().enabled = false;
        wallSix.GetComponent<Animator>().enabled = false;
        wallSeven.GetComponent<Animator>().enabled = false;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (entryTriggerEntered == false && collision.CompareTag("Player"))
        {
            StartMovingWalls();
        }
    }

    public void StartMovingWalls()
    {
        wallOne.GetComponent<Animator>().enabled = true;
        wallTwo.GetComponent<Animator>().enabled= true;
        wallThree.GetComponent<Animator>().enabled = true;
        wallFour.GetComponent<Animator>().enabled = true;
        wallFive.GetComponent<Animator>().enabled = true;
        wallSix.GetComponent<Animator>().enabled = true;
        wallSeven.GetComponent<Animator>().enabled = true;
        entryTriggerEntered = true;

    }
}
