using UnityEngine;
using System.Collections.Generic;

public class PatrolPath : MonoBehaviour {
    [Range(0, 5)]
    [HideInInspector]public float waitDelay = 0;
    [Tooltip("Whether entity should loop or not the path")]
    public bool loopPath = true;
    [Tooltip("Whether the path should return when finishing or not")]
    public bool inverseLoop = false;
    [Tooltip("The waypoints existent in the path, null transforms get deleted on play")]
    public List<Transform> waypoints = new List<Transform>();
    private void Awake() {
        waypoints.RemoveAll(t => t == null);
    }
    public void CreateWaypoint() {
        // Cleanup nulls first
        waypoints.RemoveAll(item => item == null);
        GameObject newPoint = new GameObject("Point_" + waypoints.Count);
        newPoint.transform.SetParent(transform);
        if (waypoints.Count > 0) {
            newPoint.transform.position = waypoints[waypoints.Count - 1].position + Vector3.right;
        } else {
            newPoint.transform.position = transform.position;
        }

        waypoints.Add(newPoint.transform);
    }
    public Transform ClosestWaypointToLocation(Vector2 targetPos) {
        Transform closest = null;
        float distance = 9999;
        foreach (Transform w in waypoints) { 
            float WaypointDistance = Vector3.Distance(targetPos, w.position);
            if (WaypointDistance < distance) {
                distance = WaypointDistance;
                closest = w;
            }
        }
        if(closest == null) {
            Debug.Log("No waypoint Found in ClosestWaypointToLocation");
            return null;
        } else {
            return closest;
        }
        
    }
    public void ClearPath() {
        foreach (var waypoint in waypoints) {
            if (waypoint != null) DestroyImmediate(waypoint.gameObject);
        }
        waypoints.Clear();
    }
}