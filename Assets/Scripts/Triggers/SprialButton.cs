using UnityEngine;

public class SpiralButton : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject sprialTrigger;
    private bool buttonPressed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (buttonPressed == false && collision.CompareTag("Player")) {
            PressButton();
        }
    }

    public void PressButton()
    {
        door.SetActive(false);
        sprialTrigger.SetActive(true);
        buttonPressed = true;

    }
}
