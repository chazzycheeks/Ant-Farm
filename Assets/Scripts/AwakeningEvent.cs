using UnityEngine;
using MoreMountains.CorgiEngine;

public class AwakeningEvent : MonoBehaviour
{
    public void AwakeningEventTrigger()
    {
        GetComponentInChildren<Animator>().SetBool("Awakening", false);
    }

    public void DisableMovement()
    {
        PlayerController.instance.GetComponent<Character>().Freeze();
    }

    public void EnableMovement()
    {
        PlayerController.instance.GetComponent<Character>().UnFreeze();
    }
}
