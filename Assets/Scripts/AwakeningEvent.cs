using UnityEngine;

public class AwakeningEvent : MonoBehaviour
{
    public void AwakeningEventTrigger()
    {
        GetComponentInChildren<Animator>().SetBool("Awakening", false);
    }
}
