using UnityEngine;

public class DemoEnd : MonoBehaviour
{
    public GameObject endText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        endText.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        endText.SetActive(false);
    }
}
