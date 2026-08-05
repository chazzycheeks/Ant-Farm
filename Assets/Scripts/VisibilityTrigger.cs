using MoreMountains.CorgiEngine;
using UnityEngine;

public class VisibilityTrigger : MonoBehaviour
{
    Teleporter teleporter;
    private void Start() {
        teleporter = GetComponent<Teleporter>();
        if(VisibilityManager.Instance != null) {
            teleporter.OnActivation.AddListener(VisibilityManager.Instance.OnNewRoomEnter);
        } else {
            Debug.LogError($"Visbility Manager missing");
        }
    }
}
