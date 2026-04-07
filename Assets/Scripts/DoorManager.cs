using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public List<GameObject> roomsList = new List<GameObject>();

    public void Door001OnActivation()
    {
        roomsList[1].SetActive(true);
    }
    public void Door001OnExit()
    {
        roomsList[0].SetActive(false);
    }
}
