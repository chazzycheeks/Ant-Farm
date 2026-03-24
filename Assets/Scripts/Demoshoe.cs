using UnityEngine;

public class Demoshoe : MonoBehaviour
{
    public GameObject shoeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        shoeText.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shoeText.SetActive(false);
    }
}
