using MoreMountains.CorgiEngine;
using UnityEngine;

public class ElevatorButtonStopper : MonoBehaviour
{
    private void Update()
    {
        //DisableButtons();
    }
    private void DisableButtons()
    {
        float firstYPosition = GetComponent<MovingPlatform>().PathElements[0].PathElementPosition.y;
        float secondYPosition = GetComponent<MovingPlatform>().PathElements[1].PathElementPosition.y;
        float topYPosition = Mathf.Max(firstYPosition, secondYPosition);
        float botYPosition = Mathf.Min(firstYPosition, secondYPosition);
        float currentPosition = transform.position.y;

        if (currentPosition > botYPosition && currentPosition < topYPosition)
        {
            Debug.Log("fuck you buttons");

        }
        else { Debug.Log("yipeeeeeeeeee"); }
    }
    //check elevator position
    //if elevator position is not equal to path positons 0 or 1, disable buttons
    //otherwise enable buttons

}
