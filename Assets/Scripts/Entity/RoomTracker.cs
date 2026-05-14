using System;
using UnityEngine;

public class RoomTracker : MonoBehaviour {
    static RoomTracker instance;
    public static RoomTracker Instance {
        get {  return instance; }
    }
    public Room currentRoom_Player;
    public Room currentRoom_Entity;
    [SerializeField] int roomDistanceBeforeSurveying;
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }
    public bool canEntitySurvey {
        get {
            int roomNumber_P = currentRoom_Player.roomNumber;
            int roomNumber_E = currentRoom_Entity.roomNumber;
            //Checks if the floor is different than the one the player is currently on
            if (currentRoom_Player.floorNumber != currentRoom_Entity.floorNumber) {
                return false;
            }
            // Checks if the entity can spot from the room it's in compared to the player.
            int distance = Mathf.Abs(roomNumber_P - roomNumber_E);
            if (distance > roomDistanceBeforeSurveying) {
                return false;
            }

            return true;
        }
    }

    [SerializeField] bool _canEntitySurvey; // Shows in inspector

    public void FixedUpdate() {
        _canEntitySurvey = canEntitySurvey;
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