using System.Collections;
using UnityEngine;

public class PlayerHider : MonoBehaviour
{
    private VisibilityManager visibilityManager;
    
    // Update is called once per frame
    private void Start()
    {
        visibilityManager = FindAnyObjectByType<VisibilityManager>();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        visibilityManager.isVisible = false;
    //        visibilityManager.visibilityTimer = 0;
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            visibilityManager.coverObjects.Remove(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) { // test fix to look for movement while in stay rather than on enter
        if (collision.CompareTag("Player")) {
            visibilityManager.coverObjects.Add(this);
        }
    }

}

