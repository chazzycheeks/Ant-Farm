using UnityEngine;
using FMODUnity;

public class SmallBarrierSound : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter emitter;

    public void BarrierSFX()
    {
        emitter.Play();
    }

}
