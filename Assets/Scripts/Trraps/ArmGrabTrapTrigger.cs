using MoreMountains.CorgiEngine;
using UnityEngine;

public class ArmGrabTrapTrigger : MonoBehaviour {
    ArmGrabTrap armGrabTrap;
    

    private void Awake() {
        armGrabTrap = GetComponentInParent<ArmGrabTrap>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && armGrabTrap.disarmed == false) {
            if(collision.GetComponent<Health>().TemporarilyInvulnerable == false && collision.gameObject.GetComponent<TrappedStateHandler>().enabled == false) {
                if (armGrabTrap != null) {
                    armGrabTrap.playerGO = collision.gameObject;
                }
                if (armGrabTrap.trappedStateHandler == null) {
                    armGrabTrap.trappedStateHandler = collision.gameObject.GetComponent<TrappedStateHandler>();
                }
                collision.gameObject.GetComponent<TrappedStateHandler>().trap = armGrabTrap;
                StartCoroutine(armGrabTrap.TrapTriggered());
            }
        }
    }
}
