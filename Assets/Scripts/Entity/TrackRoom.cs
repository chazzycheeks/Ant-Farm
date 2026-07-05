using UnityEngine;

public class TrackRoom : MonoBehaviour {
    RoomTracker roomTracker;
    [Tooltip("The room we assign the entity or player to be in once it walks inside the trigger")]
    [SerializeField] Room roomData;

    private void Awake() {
        roomTracker = GetComponentInParent<RoomTracker>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (roomTracker != null && collision != null) {
            if (collision.CompareTag("Player")) {
                roomTracker.SetRoom(true, roomData);
            }

            if (collision.CompareTag("EntityTrigger")) {
                roomTracker.SetRoom(false, roomData);
            }
        }
    }
}
