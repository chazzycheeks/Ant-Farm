using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance {  get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("more than one audio manager");
        }
        instance = this;
    }
    public void PlayMusicTrack(EventReference music)
    {
        RuntimeManager.PlayOneShot(music);
    }
    public void PlaySound(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void StopMusicTrack(EventReference music)
    {
        /*RuntimeManager.paus*/
    }
}
