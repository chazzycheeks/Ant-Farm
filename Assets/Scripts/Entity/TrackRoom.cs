using UnityEngine;

public class TrackRoom : MonoBehaviour {
    RoomTracker roomTracker;
    [SerializeField] Room roomData;

    private void Awake() {
        roomTracker = GetComponentInParent<RoomTracker>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (roomTracker != null && collision != null) {
            if (collision.CompareTag("Player")) {
                roomTracker.currentRoom_Player = roomData;
            }

            if (collision.CompareTag("EntityTrigger")) {
                roomTracker.currentRoom_Entity = roomData;
            }
        }
    }
}
