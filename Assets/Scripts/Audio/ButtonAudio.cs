using UnityEngine;
using FMODUnity;

public class ButtonAudio : MonoBehaviour
{
    [SerializeField] private EventReference buttonPressSound;
  public void PlayButtonPress()
    {
        AudioManager.instance.PlayOneShotSound(buttonPressSound, this.transform.position);
    }
}
