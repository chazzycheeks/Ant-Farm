using UnityEngine;
using FMODUnity;

public class ArmCreak : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter emitter;

    public void CreakSFX()
    {
        emitter.Play();
    }

}

