using UnityEngine;

public class Room4Button : MonoBehaviour
{
    [SerializeField] private GameObject door;
    private bool buttonPressed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (buttonPressed == false)
        {
            PressButton();
        }
        else
        {
            return;
        }
    }

    public void PressButton()
    {
        door.SetActive(false);
        buttonPressed = true;

    }
}
