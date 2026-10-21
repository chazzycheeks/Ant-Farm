using UnityEngine;
using FMODUnity;


public class GinatShutterAudio : MonoBehaviour
{
    /*private StudioEventEmitter emitter;*/
    [SerializeField] private StudioEventEmitter StartMovingEmitter;
    [SerializeField] private StudioEventEmitter EndMovingEmitter;

    /* private void Start()
     {
         emitter = AudioManager.instance.InitialiseEventEmitter(FMODEvents.instance.giantShutterMoving, this.gameObject);

     }*/

    public void StartShutterFX()
    {
        StartMovingEmitter.Play();
    }

    public void StopShutterSFX()
    {
        StartMovingEmitter.Stop();
    }

    public void EndShutterSFX()
    {
        EndMovingEmitter.Play();
    }
}

