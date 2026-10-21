using FMODUnity;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private EventReference footsteps;
    [SerializeField] private EventReference jump;
    [SerializeField] private EventReference land;
    [SerializeField] private EventReference wakeup;
    [SerializeField] private EventReference getup;
    [SerializeField] private EventReference dash;
    [SerializeField] private EventReference climb;
    public void Footsteps()
    {
        AudioManager.instance.PlayOneShotSound(footsteps, this.transform.position);
    }

    public void Jump()
    {
        AudioManager.instance.PlayOneShotSound(jump, this.transform.position);
    }

    public void Land()
    {
        AudioManager.instance.PlayOneShotSound(land, this.transform.position);
    }
    public void WakeUp()
    {
        AudioManager.instance.PlayOneShotSound(wakeup, this.transform.position);
    }

    public void GetUp()
    {
        AudioManager.instance.PlayOneShotSound(getup, this.transform.position);
    }
    public void Dash()
    {
        AudioManager.instance.PlayOneShotSound(dash, this.transform.position);
    }
    public void Climb()
    {
        AudioManager.instance.PlayOneShotSound(climb, this.transform.position);
    }
}
