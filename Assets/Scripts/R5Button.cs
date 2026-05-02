using UnityEngine;

public class R5Button : MonoBehaviour
{
    [SerializeField] private GameObject panels;
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
        panels.SetActive(false);
        door.SetActive(false);
        buttonPressed = true;

    }
}
