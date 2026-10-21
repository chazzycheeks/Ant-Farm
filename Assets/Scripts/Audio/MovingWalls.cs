using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]

public class MovingWalls : MonoBehaviour
{
    private StudioEventEmitter emitter;

    private void Start()
    {
        emitter = AudioManager.instance.InitialiseEventEmitter(FMODEvents.instance.movingWalls, this.gameObject);
        emitter.Play();
    }

    public void StartWallSFX()
    {
        emitter.Play();
    }

    public void StopWallSFX()
    {
        emitter.Stop();
    }
}
