using UnityEngine;

public class ArmGrabTrapTrigger : MonoBehaviour {
    ArmGrabTrap armGrabTrap;
    

    private void Awake() {
        armGrabTrap = GetComponentInParent<ArmGrabTrap>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (armGrabTrap != null) {
                armGrabTrap.playerGO = collision.gameObject;
            }
            if (armGrabTrap != null && armGrabTrap.trappedStateHandler == null) {
                armGrabTrap.trappedStateHandler = collision.gameObject.GetComponent<TrappedStateHandler>();
                collision.gameObject.GetComponent<TrappedStateHandler>().enabled = true;
                collision.gameObject.GetComponent<TrappedStateHandler>().trap = armGrabTrap;
            }
                StartCoroutine(armGrabTrap.TrapTriggered());
        }
    }
}
