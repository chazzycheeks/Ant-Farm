using UnityEngine;
using UnityEngine.Events;

public class VisibilityManager : MonoBehaviour
{
    public bool isVisible = false;
    public float visibilityTrigger = 2.5f;
    public float visibilityTimer;
    public UnityEvent onSeen;
    private bool seenEventPlaying = false; 

    void Update()
    {
        if (isVisible == true)
        {
            UpdateTimer();
        }

        CheckIfSpotted();
    }

    public void UpdateTimer()
    {
        if (seenEventPlaying == false)
        {
            visibilityTimer += Time.deltaTime * 1f;
        }
    }

    public void CheckIfSpotted()
    {
        if (visibilityTimer >= visibilityTrigger && seenEventPlaying == false)
        {
            visibilityTimer = 0f;
            seenEventPlaying = true;
            onSeen?.Invoke();
            
        }
    }

    public void ResetOnSeenEvent()
    {
        seenEventPlaying = false;
    }

}
