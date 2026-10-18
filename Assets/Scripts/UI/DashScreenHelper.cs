using MoreMountains.CorgiEngine;
using UnityEngine;

public class DashScreenHelper : MonoBehaviour
{
    public void CloseDashScreen()
    {
        PlayerController.instance.GetComponent<Character>().UnFreeze();
        gameObject.SetActive(false);
    }
}
