using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("Music")]

    [field: SerializeField] public EventReference introMusic { get; private set; }
    [field: SerializeField] public EventReference safeRoomMusic { get; private set; }
    [field: SerializeField] public EventReference roomSevenMusic { get; private set; }

    [field: Header("Moving Walls SFX")]
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
