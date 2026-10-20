using UnityEngine;
using MoreMountains.CorgiEngine;

public class ElevatorFreezer : MonoBehaviour
{
    private bool isActive = false;

    public void FreezePlayer()
    {
        if (!isActive) { return; }
        PlayerController.instance.GetComponent<Character>().Freeze();
        PlayerController.instance.GetComponentInChildren<Animator>().SetTrigger("StartCutscene");
        PlayerController.instance.GetComponentInChildren<Animator>().SetBool("Cutscene", true);
    }

    public void UnfreezePLayer()
    {
        if (!isActive) { return; }
        PlayerController.instance.GetComponent<Character>().UnFreeze();
        PlayerController.instance.GetComponentInChildren<Animator>().SetBool("Cutscene", false);
    }

    public void Activate()
    {
        isActive = true;
        FreezePlayer();
    }
}
