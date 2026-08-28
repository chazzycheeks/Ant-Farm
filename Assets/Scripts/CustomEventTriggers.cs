using UnityEngine;
using UnityEngine.Events;

public class CustomEventTriggers : MonoBehaviour
{
    public UnityEvent enterEvent;
    public UnityEvent exitEvent;
    public UnityEvent stayEvent;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(enterEvent != null) enterEvent.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(exitEvent != null) exitEvent.Invoke();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (stayEvent != null) stayEvent.Invoke();
    }
}
