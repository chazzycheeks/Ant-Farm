using UnityEngine;

public class Room6Gate : MonoBehaviour
{
    [SerializeField] private GameObject door;
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
        buttonPressed = true;

    }
}
