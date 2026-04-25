using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLocomotionController : MonoBehaviour
{
    [SerializeField] PathCache patrolPath;
    [SerializeField] PathCache surveyPath;
    [SerializeField] Vector3 targetCoordinates;
    [SerializeField] Transform offsetAnchor; // used to add offset to position when spotting so the eye is focused on the player, not the body
    //[SerializeField] PatrolPath patrolPath; // The patrol path
    //int currentPointIndex = 0;
    Rigidbody2D rb;
    [SerializeField] float speed = 100f; // Velocity it's moving at
    [SerializeField] float arrivalThreshold = 3f; // Distance treshhold before it slows down the speed
    [SerializeField] float spottingSpeed = 200f;
    bool isPatrolling = false;
    bool isFixating = false;
    //bool inverseLoop = false;
    PathType currentPath = PathType.Roaming;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        //StartCoroutine(MoveLoop());
    }
    private void Update() {
            

    }
    public Vector3 GetCurrentPointLocation(PathType path) {
        if(path == PathType.Roaming) {
            return patrolPath.path.waypoints[patrolPath.currentIndex].transform.position;
        } else {
            return surveyPath.path.waypoints[surveyPath.currentIndex].transform.position;
        }
    }
    public void TogglePatrol(bool startPatrol, PathType path) {
        // We can add additional varaibles here if we want, such as selecting a specific waypoint to start at or so on.
        if (startPatrol) {
            isPatrolling = true;
        } else {
            isPatrolling = false;
            rb.linearVelocity = Vector2.zero;
        }
        isFixating = false;
        if (path == PathType.Roaming) {
            currentPath = PathType.Roaming;
        } else if(path == PathType.Surveying) {
            currentPath = PathType.Surveying;
        } else if(path == PathType.FreeMoving) {
            currentPath = PathType.FreeMoving;
            isFixating = true;
        }

    }

    /// <summary>
    /// To be used during a movement cycle such as surveying to cache the initial position to 
    /// </summary>
    public void PausePatrol() {
        isPatrolling = false;
    }

    /// <summary>
    /// This function is used in the spotting state to return the Entity to the path selected in the parameter and it will set the waypoint for the path to next closest in the trajectory of the path.
    /// </summary>
    /// <param name="pathToReturnTo">Sets the path according to the parameter</param>
    public void ReturnToPath(PathType pathToReturnTo) {
        float lowestDistance = 999;
        Transform closestWaypoint = null;
        Vector3 currentPos = transform.position;
        if(pathToReturnTo == PathType.Roaming) {
            foreach (Transform t in patrolPath.path.waypoints) {
                float distance = Vector3.Distance(currentPos, t.position);
                if (distance < lowestDistance) {
                    lowestDistance = distance;
                    closestWaypoint = t;
                }
            }
            
            int currentIndex = patrolPath.path.waypoints.IndexOf(closestWaypoint);
            Vector2 dir = (closestWaypoint.position - transform.position).normalized;
            if (patrolPath.inverse == false) { // Look for the closest point to the right side
                if (dir.x < 0) {
                    patrolPath.currentIndex = currentIndex;
                    // If the path already reached it's limit, do not try to force it to go further
                    if (patrolPath.currentIndex == patrolPath.path.waypoints.Count - 1) {
                        Debug.Log("Limit reached for path, not changing result");
                        return;
                    } else {
                        patrolPath.currentIndex++;
                    }
                } else {
                    patrolPath.currentIndex = currentIndex;
                }
            } else { // Look for the closest point to the left side
                if (dir.x > 0) {
                    patrolPath.currentIndex = currentIndex;
                    // If the path already reached it's limit, do not try to force it to go further
                    if (patrolPath.currentIndex == 0) {
                        Debug.Log("Limit reached for path, not changing result");
                        return;
                    } else {
                        patrolPath.currentIndex--;
                    }
                } else {
                    patrolPath.currentIndex = currentIndex;
                }
            }
        } 
        else if (pathToReturnTo == PathType.Surveying) {
            foreach (Transform t in surveyPath.path.waypoints) {
                float distance = Vector3.Distance(currentPos, t.position);
                if (distance < lowestDistance) {
                    lowestDistance = distance;
                    closestWaypoint = t;
                }
            }
            int currentIndex = surveyPath.path.waypoints.IndexOf(closestWaypoint);
            Vector2 dir = (closestWaypoint.position - transform.position).normalized;
            if (surveyPath.inverse == false) { // Look for the closest point to the right side
                if (dir.x < 0) {
                    surveyPath.currentIndex = currentIndex;
                    // If the path already reached it's limit, do not try to force it to go further
                    if (surveyPath.currentIndex == surveyPath.path.waypoints.Count - 1) {
                        Debug.Log("Limit reached for path, not changing result");
                        return;
                    } else {
                        surveyPath.currentIndex++;
                    }
                } else {
                    surveyPath.currentIndex = currentIndex;
                }
            } else { // Look for the closest point to the left side
                if (dir.x < 0) {
                    surveyPath.currentIndex = currentIndex;
                    // If the path already reached it's limit, do not try to force it to go further
                    if (surveyPath.currentIndex == 0) {
                        Debug.Log("Limit reached for path, not changing result");
                        return;
                    } else {
                        surveyPath.currentIndex--;
                    }
                } else {
                    surveyPath.currentIndex = currentIndex;
                }
            }
        }

        currentPath = pathToReturnTo;
        isPatrolling = true;
        
    }


    private void FixedUpdate() {
        switch (currentPath) {
            case PathType.Roaming:
                if (isPatrolling) {
                    // Local refernces
                    Vector2 currentPos = transform.position;
                    Vector2 targetPos = patrolPath.path.waypoints[patrolPath.currentIndex].position;
                    Vector2 dir = (targetPos - currentPos).normalized;
                    float currentSpeed = speed;
                    float distance = Vector2.Distance(currentPos, targetPos);
                    // Slow down movement before arriving to the destination
                    if (distance < arrivalThreshold) {
                        currentSpeed = speed * (distance / arrivalThreshold);
                    }
                    rb.linearVelocity = dir * currentSpeed; // Set speed
                                                            // Stop before reaching point
                    if (distance < 0.3f) {
                        //rb.linearVelocity = Vector2.zero; This would full
                        if (patrolPath.currentIndex == 0 && patrolPath.inverse == true) {
                            patrolPath.inverse = false;
                        }
                        if (patrolPath.currentIndex == patrolPath.path.waypoints.Count - 1 && patrolPath.inverse == false) {
                            patrolPath.inverse = true;
                        }
                        if (patrolPath.inverse) {
                            patrolPath.currentIndex--;
                        } else {
                            patrolPath.currentIndex++;
                        }
                    }
                }
                break;
            case PathType.Surveying:
                if (isPatrolling) {
                    // Local refernces
                    Vector2 currentPos = transform.position;
                    Vector2 targetPos = surveyPath.path.waypoints[surveyPath.currentIndex].position;
                    Vector2 dir = (targetPos - currentPos).normalized;
                    float currentSpeed = speed;
                    float distance = Vector2.Distance(currentPos, targetPos);
                    // Slow down movement before arriving to the destination
                    if (distance < arrivalThreshold) {
                        currentSpeed = speed * (distance / arrivalThreshold);
                    }
                    rb.linearVelocity = dir * currentSpeed; // Set speed
                                                            // Stop before reaching point
                    if (distance < 0.3f) {
                        //rb.linearVelocity = Vector2.zero; This would full
                        if (surveyPath.currentIndex == 0 && surveyPath.inverse == true) {
                            surveyPath.inverse = false;
                        }
                        if (surveyPath.currentIndex == surveyPath.path.waypoints.Count - 1 && surveyPath.inverse == false) {
                            surveyPath.inverse = true;
                        }
                        if (surveyPath.inverse) {
                            surveyPath.currentIndex--;
                        } else {
                            surveyPath.currentIndex++;
                        }
                    }
                }
                break;

            case PathType.FreeMoving:
                if (isFixating) {
                    // 
                    Vector2 currentPos = offsetAnchor.transform.position;
                    Vector2 targetPos = VisibilityManager.Instance.lastPlayerPositionSeen;
                    Vector2 dir = (targetPos - currentPos).normalized;
                    float currentSpeed = spottingSpeed;
                    float distance = Vector2.Distance(currentPos, targetPos);
                    // Slow down movement before arriving to the destination

                        // t goes from 1 (at threshold) to 0 (at target)
                        float t = Mathf.Clamp01(distance / arrivalThreshold);
                        float ease = -(Mathf.Cos(Mathf.PI * t) - 1) / 2;

                        currentSpeed = spottingSpeed * ease;
                    rb.linearVelocity = dir * currentSpeed; // Set speed
                }
                break;
            
        }
    }


}

[System.Serializable]
public class PathCache {
    public PatrolPath path;
    public int currentIndex;
    public bool inverse; // whether it's going left>right(false) or left<right(true)
    public PathCache(PatrolPath _path, int index, bool _inverse) { 
        path = _path;
        currentIndex = index;
        inverse = _inverse;
    }

    
}

public enum PathType {
    Roaming,
    Surveying,
    FreeMoving
}