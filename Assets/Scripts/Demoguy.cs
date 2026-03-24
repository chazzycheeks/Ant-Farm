using UnityEngine;

public class Demoguy : MonoBehaviour
{
    public GameObject guyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        guyText.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        guyText.SetActive(false);
    }
}
