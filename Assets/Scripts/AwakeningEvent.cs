using UnityEngine;

public class AwakeningEvent : MonoBehaviour
{
    public void AwakeningEventTrigger()
    {
        GetComponent<Animator>().SetBool("Awakening", false);
    }
}
