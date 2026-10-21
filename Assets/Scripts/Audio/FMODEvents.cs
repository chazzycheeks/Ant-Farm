using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Moving Walls SFX")]
    [field: SerializeField] public EventReference movingWalls { get; private set; }
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
