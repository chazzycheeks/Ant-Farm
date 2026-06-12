using UnityEngine;
using System.Collections.Generic;

public class PatrolPath : MonoBehaviour {
    [Range(0, 5)]
    public float waitDelay = 0;
    public bool loopPath = true;
    public bool inverseLoop = false;
    public List<Transform> waypoints = new List<Transform>();
    
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

    public void ClearPath() {
        foreach (var waypoint in waypoints) {
            if (waypoint != null) DestroyImmediate(waypoint.gameObject);
        }
        waypoints.Clear();
    }
}