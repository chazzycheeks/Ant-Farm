using System;
using UnityEngine;

public class RoomTracker : MonoBehaviour {
    static RoomTracker instance;
    public static RoomTracker Instance {
        get {  return instance; }
    }
    Room currentRoom_Player;
    Room currentRoom_Entity;
    [Tooltip("How many rooms away can the entity be before it starts spotting the player ")]
    [SerializeField] int roomDistanceBeforeSurveying;
    public bool _canEntitySurvey {
        get {
            if(currentRoom_Entity == null ||currentRoom_Player == null) {
                return false;
            }
            int roomNumber_P = currentRoom_Player.roomNumber;
            int roomNumber_E = currentRoom_Entity.roomNumber;
            //Checks if the floor is different than the one the player is currently on
            if (currentRoom_Player.floorNumber != currentRoom_Entity.floorNumber) {
                return false;
            }
            // Checks if the entity can spot from the room it's in compared to the player.
            int distance = Mathf.Abs(roomNumber_P - roomNumber_E);
            if (distance >= roomDistanceBeforeSurveying) {
                return false;
            }

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