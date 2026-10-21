using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]

public class SmallMovingWalls : MonoBehaviour
{
    private StudioEventEmitter emitter;

    private void Start()
    {
        emitter = AudioManager.instance.InitialiseEventEmitter(FMODEvents.instance.smallMovingWalls, this.gameObject);

    }

    public void StartSmallWallSFX()
    {
        emitter.Play();
    }

    public void StopSmallWallSFX()
    {
        emitter.Stop();
    }
}
