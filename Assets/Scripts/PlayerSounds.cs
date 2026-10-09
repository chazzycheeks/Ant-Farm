using UnityEngine;
using FMODUnity;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private EventReference music;
    public void Footsteps()
    {
        Debug.Log("step");
        AudioManager.instance.PlayMusicTrack(music);
    }

    public void Test()
    {
        Debug.Log("Test");
    }
}
