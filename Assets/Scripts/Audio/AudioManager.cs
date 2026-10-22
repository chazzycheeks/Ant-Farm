using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;
    public static AudioManager instance {  get; private set; }

    /*private EventInstance AmbienceInstance;*/

    [SerializeField] private StudioEventEmitter ambienceEmitter;
    [SerializeField] private StudioEventEmitter safeRoomEmitter;
    [SerializeField] private StudioEventEmitter roomSevenEmitter;
    [SerializeField] private StudioEventEmitter wallEmitter;
    [SerializeField] private StudioEventEmitter endingEmitter;
    [SerializeField] private StudioEventEmitter spiralEmitter;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("more than one audio manager");
        }
        instance = this;

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();
    }
    private void Start()
    {
        /*InitialiseAmbience(FMODEvents.instance.ambience);*/
        ambienceEmitter.Play();
    }

    /*private void InitialiseAmbience(EventReference Ambience)
    {
        AmbienceInstance = CreateInstance(Ambience);
        AmbienceInstance.start();
    }*/

    /*public void SetAmbienceArea(AmbienceAreas area)
    {
        AmbienceInstance.setParameterByName("area", (float) area);
        Debug.Log("ambiencechange");
    }*/
    public void SwitchAmbienceBig()
    {
        ambienceEmitter.SetParameter("AtmosSwitch", 1);
    }
    public void SwitchAmbienceSmall()
    {
        ambienceEmitter.SetParameter("AtmosSwitch", 0);
    }

    public void StartSafeRoomMusic()
    {
        safeRoomEmitter.Play();
    }
    public void EndSafeRoomMusic()
    {
        safeRoomEmitter.SetParameter("SafeRoom", 1);
    }
    public void StartRoomSevenMusic()
    {
        roomSevenEmitter.Play();
    }
    public void EndRoomSevenMusic()
    {
        roomSevenEmitter.SetParameter("RoomFourteen", 1);
    }

    public void PlayWallMusic()
    {
        wallEmitter.Play();
    }
    public void EndWallMusic()
    {
        wallEmitter.SetParameter("Wall", 1);
    }
    public void PlayEndingMusic()
    {
        endingEmitter.Play();
    }
    public void StopEndingMusic()
    {
        endingEmitter.SetParameter("Intro", 1);
    }
    public void PlaySpiralMusic()
    {
        spiralEmitter.Play();
    }
    public void EndSpiralMusic()
    {
        spiralEmitter.SetParameter("Looming", 1);
    }

    public void PlayOneShotSound(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public StudioEventEmitter InitialiseEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
