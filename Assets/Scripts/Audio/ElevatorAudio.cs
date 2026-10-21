using UnityEngine;
using FMODUnity;
using System.Collections;

public class ElevatorAudio : MonoBehaviour
{
    [SerializeField] private EventReference elevatorEndSound;
    [SerializeField] private EventReference elevatorStartSound;

    [SerializeField] private StudioEventEmitter emitter;
    public void PlayElevatorStart()
    {
        AudioManager.instance.PlayOneShotSound(elevatorStartSound, this.transform.position);
    }

    public void PlayElevatorEndRoom4()
    {
        StartCoroutine(ElevatorRoom4Couroutine());
    }

    public void PlayElevatorEndRoom5()
    {
        StartCoroutine(ElevatorRoom5Couroutine());
    }

    private IEnumerator ElevatorRoom4Couroutine()
    {
        yield return new WaitForSeconds(4.8f);
        AudioManager.instance.PlayOneShotSound(elevatorEndSound, this.transform.position);
        yield return new WaitForSeconds(0.2f);
        emitter.Stop();
    }

    private IEnumerator ElevatorRoom5Couroutine()
    {
        yield return new WaitForSeconds(2.8f);
        AudioManager.instance.PlayOneShotSound(elevatorEndSound, this.transform.position);
        yield return new WaitForSeconds(0.2f);
        emitter.Stop();
    }

}

