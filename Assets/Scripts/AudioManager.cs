using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance {  get; private set; }

    private EventInstance musicEventInstance;

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

    /*public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
    }
    private void InitialiseMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateIn
    }

    public void SetMusic(MusicTransitions transition)
    {
        musicEventInstance
    }*/
}
