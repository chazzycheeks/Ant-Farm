using System.Collections;
using UnityEngine;

public class PatrolLocomotionController : MonoBehaviour
{
    [SerializeField] PathCache patrolPath;
    [SerializeField] PathCache surveyPath;
    
    //[SerializeField] PatrolPath patrolPath; // The patrol path
    //int currentPointIndex = 0;
    Rigidbody2D rb;
    [SerializeField] float speed = 100f; // Velocity it's moving at
    [SerializeField] float arrivalThreshold = 3f; // Distance treshhold before it slows down the speed
    bool isPatrolling = false;
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

    public void TogglePatrol(bool startPatrol, PathType path) {
        // We can add additional varaibles here if we want, such as selecting a specific waypoint to start at or so on.
        if (startPatrol) {
            isPatrolling = true;
        } else {
            isPatrolling = false;
            rb.linearVelocity = Vector2.zero;
        }

        if (path == PathType.Roaming) {
            currentPath = PathType.Roaming;
        } else {
            currentPath = PathType.Surveying;
        }
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
        }
        
    }
}

[System.Serializable]
public class PathCache {
    public PatrolPath path;
    public int currentIndex;
    public bool inverse;
    public PathCache(PatrolPath _path, int index, bool _inverse) { 
        path = _path;
        currentIndex = index;
        inverse = _inverse;
    }

    
}

public enum PathType {
    Roaming,
    Surveying
}