using UnityEngine;

public class Room6Gate : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject room4Door;
    private bool buttonPressed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (buttonPressed == false && collision.CompareTag("Player"))
        {
            PressButton();
        }
    }

    public void PressButton()
    {
        door.SetActive(false);
        room4Door.SetActive(false);
        buttonPressed = true;

    }
}
