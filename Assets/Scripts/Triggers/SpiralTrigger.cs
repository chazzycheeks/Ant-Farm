using UnityEngine;

public class SpiralTrigger : MonoBehaviour
{
    [SerializeField] private GameObject wallsIn;
    [SerializeField] private GameObject wallsOut;
    [SerializeField] private GameObject spiralArm;
    private bool spiralTriggerEntered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (spiralTriggerEntered == false)
        {
            RemoveWalls();
        }
        else
        {
            return;
        }
    }

    public void RemoveWalls()
    {
        wallsIn.SetActive(false);
        wallsOut.SetActive(true);
        spiralArm.SetActive(true);
        spiralTriggerEntered = true;

    }
}
