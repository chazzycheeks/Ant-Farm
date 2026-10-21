using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Moving Walls SFX")]
    [field: Header("Small Moving Walls SFX")]
/*    [field: Header("Giant Shutter Moving SFX")]
    [field: Header("Giant Shutter End SFX")]*/
    [field: SerializeField] public EventReference movingWalls { get; private set; }
    [field: SerializeField] public EventReference smallMovingWalls { get; private set; }
/*    [field: SerializeField] public EventReference giantShutterMoving { get; private set; }
    [field: SerializeField] public EventReference giantShutterEnd { get; private set; }*/
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD instance in the scene");
        }
        instance = this;
    }
}
