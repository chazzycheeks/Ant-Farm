using System;
using Unity.VisualScripting;
using UnityEngine;

public class RoomTracker : MonoBehaviour {
    static RoomTracker instance;
    public static RoomTracker Instance {
        get {  return instance; }
    }
    Room currentRoom_Player;
    Room currentRoom_Entity;
    [Tooltip("How many rooms away can the entity be before it starts spotting the player ")]
    [SerializeField] int roomDistanceBeforeSurveying = 100;
    public Room CurrentRoom_Player => currentRoom_Player;
    public Room CurrentRoom_Entity => currentRoom_Entity;
    public bool isInTheSameRoom => CurrentRoom_Entity.roomNumber == CurrentRoom_Player.roomNumber;
    
    public int minRoomNumberAway(int maxValue){
        int number = Mathf.Clamp(currentRoom_Player.roomNumber - distanceBeforeOutsideRange, 0, maxValue);
        return number;
    }

    public int maxRoomNumberAway(int maxValue) {
        int number = Mathf.Clamp(currentRoom_Player.roomNumber + distanceBeforeOutsideRange, 0, maxValue);
        return number;
    }
    public bool entityNotNear {
        get {
            if(currentRoom_Entity == null) {
                return false;
            }

            int distance = Mathf.Abs(currentRoom_Player.roomNumber - currentRoom_Entity.roomNumber);
            return distance >= distanceBeforeOutsideRange + 1;
        }
    }

    bool _isTooFar {
        get {
            if (currentRoom_Entity == null || currentRoom_Player == null) {
                return false;
            }

            int distance = Mathf.Abs(currentRoom_Player.roomNumber - currentRoom_Entity.roomNumber);
            //return distance >= distanceBeforeOutsideRange;
            return false; // returning false because we no longer need to account for distance while roaming
        }
    }
    [Tooltip("Indicate if the entity is too far away from the player")]
    public bool isTooFar => _isTooFar;

    [Tooltip("Indicate how many rooms away the entity should be before it's considered too far for patrolling")]
    public int distanceBeforeOutsideRange = 5;
    public bool _canEntitySurvey {
        get {
            //if(currentRoom_Entity == null ||currentRoom_Player == null) {
            //    return false;
            //}
            //int roomNumber_P = currentRoom_Player.roomNumber;
            //int roomNumber_E = currentRoom_Entity.roomNumber;

            ////Checks if the floor is different than the one the player is currently on
            //if (currentRoom_Player.floorNumber != currentRoom_Entity.floorNumber) {
            //    return false;
            //}

            //// Checks if the entity can spot from the room it's in compared to the player.
            //int distance = Mathf.Abs(roomNumber_P - roomNumber_E);
            //if (distance >= roomDistanceBeforeSurveying) {
            //    return false;
            //}

            return true;
        }
    }
    [SerializeField] bool CanEntitySurvey;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }
    public void SetRoom(bool _isPlayer, Room _Room) {
        if (_isPlayer) {
            currentRoom_Player = _Room;
            if(currentRoom_Entity == null) {
                currentRoom_Entity = _Room;
            }
        } else {
            currentRoom_Entity = _Room;
        }
    }

    public void FixedUpdate() {
        CanEntitySurvey = _canEntitySurvey; // visualise in inspector
    }
}


public enum FloorNumber {
    Floor1,
    Floor2,
    Floor3,
}

[Serializable]
public class Room {
    public int roomNumber;
    public FloorNumber floorNumber;
    public Room(FloorNumber _floorNumber, int _roomNumber) {
        roomNumber = _roomNumber;
        floorNumber = _floorNumber;
    }
}