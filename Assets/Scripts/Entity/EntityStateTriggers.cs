using UnityEngine;

public class EntityStateTriggers : MonoBehaviour
{
    public static EntityStateTriggers Instance {
        get {
            return instance;
        }
    }

    static EntityStateTriggers instance;
    /// <summary>
    /// Used in Roaming state to detect whether a player is in sight of the entity or not, if it is then swap to survey.
    /// </summary>
    public bool noticedPlayer;
    /// <summary>
    /// Used in survey and spotting timer logic, if it is on then do not increase the idle timer and reset it to 0, else increase it
    /// </summary>
    public bool isSpotting;
    /// <summary>
    /// Used to check whether it finished emerging or not
    /// </summary>
    public bool transitioned;
    /// <summary>
    /// Used to check whether the player is in front of the entity or not.
    /// </summary>
    public bool inFrontOfEntity;
    public bool narativeStuckInSpot;
    /// <summary>
    /// Toggle whether it's spotted a player and is trying to chase them or not
    /// </summary>
    public bool isActivelyChasing = false;

    public bool hasLeftRoom = false; // Did player leave the room? Then leave survey state
    bool _isSameRoomAndChasing => isActivelyChasing && RoomTracker.Instance.isInTheSameRoom;

    public bool sameRoomAndChasing;

    public bool repeatImmunity;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }
    private void Update() {
        sameRoomAndChasing = _isSameRoomAndChasing;
    }

}
