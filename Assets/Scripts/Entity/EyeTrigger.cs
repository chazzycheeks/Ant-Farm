using UnityEngine;

public class EyeTrigger : MonoBehaviour
{
    EntityStateTriggers stateTriggers;
    private void Awake() {
        stateTriggers = GetComponentInParent<EntityStateTriggers>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.CompareTag("Player")) {
            stateTriggers.inFrontOfEntity = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.transform.CompareTag("Player")) {
            stateTriggers.inFrontOfEntity = false;
        }
    }
}
