using UnityEngine;
using FMODUnity;

public class Track1Stopper : MonoBehaviour
{
    [SerializeField] private EventReference music;

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            StopMusic();
        }
    }

    public void StopMusic()
    {
        
    }
}
