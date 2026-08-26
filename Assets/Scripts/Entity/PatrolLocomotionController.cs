using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLocomotionController : MonoBehaviour
{
    [Header("CoordinationPoints")]
    [Tooltip("The path used during roaming, needs to have an inittialy set point to start the roaming process")]
    [SerializeField] PathCache _patrolPath; // need to have a starting patrol path and hide this
    public PathCache PatrolPath => _patrolPath;
    /// <summary> The path used during surveying, it should retroactively get set by <see cref="AssignPathTrigger"/>. </summary>
    PathCache _surveyPath;
    public PathCache SurveyPath => _surveyPath;
    [Tooltip("The point where the entity is stuck still while it's in a naratting spot")]
    public Transform staticPointTransform;
    PathCache lastSurveyPathUsed;
    //Vector3 targetCoordinates;
    PathCache _spottingPath;
    public PathCache SpottingPath => _spottingPath;
    [Tooltip("The anchor point used to add offset to position when spotting so the eye is focused on the player, not the body")]

    [SerializeField] Transform offsetAnchorLeft;
    [SerializeField] Transform offsetAnchorRight;

    [Header("Speeds")]
    [Tooltip("The velocity speed the entity roams at")]
    [SerializeField] float roamingSpeed = 7f;
    [Tooltip("The velocity speed the entity surveys at")]
    [SerializeField] float surveySpeed = 14f;
    [Tooltip("The distance treshold before it slows down to reach the waypoint")]
    [SerializeField] float arrivalThreshold = 3f;
    [Tooltip("The velocity speed it spots at")]
    [SerializeField] float spottingSpeed = 20f;
    [Tooltip("The velocity speed it goes to the static spot speed with.")]
    [SerializeField] float staticSpeed = 100f;
    //Velocity the entity is currently going at
    [Header("Survey")]
    [Tooltip("The amount of time you stop at a point(in seconds")]
    [SerializeField] float arrivalStopTime = 5f;
    private bool _isWaiting = false;

    float _speed {
        get {
            return currentPath == PathType.Roaming ? roamingSpeed : surveySpeed;
        }
    }

    [Header("Properties")]
    bool isPatrolling = false;
    bool isFixating = false;
    PathType currentPath = PathType.Roaming;
    //References
    Rigidbody2D rb;
    
    


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        //StartCoroutine(MoveLoop());
    }
    private void Update() {
            

    }

    public void SetSurveyPath(PathCache newPath) {
        if(_surveyPath != null) {
            lastSurveyPathUsed = _surveyPath;
            _surveyPath = newPath;
        } else {
            _surveyPath = newPath;
        }
    }
    public void SetSpottingPath(PathCache newPath) {
        if (_patrolPath != null) {
            _spottingPath = newPath;
        } else {
            _spottingPath = newPath;
        }
    }
    public Vector3 GetCurrentPointLocation(PathType path) {
        if(path == PathType.Roaming) {
            return _patrolPath.path.waypoints[_patrolPath.currentIndex].transform.position;
        } else {
            return _surveyPath.path.waypoints[_surveyPath.currentIndex].transform.position;
        }
    }
    /// <summary>
    /// Toggles whether the unit should be in patrol mode or not and which path to swap to
    /// </summary>
    /// <param name="startPatrol">Decides whether the entity patrols or not</param>
    /// <param name="path">The path to set to</param>
    public void TogglePatrol(bool startPatrol, PathType path) {
        if (startPatrol) {
            isPatrolling = true;
        } else {
            isPatrolling = false;
            rb.linearVelocity = Vector2.zero;
        }
        rb.linearVelocity = Vector2.zero;
        isFixating = false;
        //Setting path according to the parameter
        currentPath = path;
        if(path == PathType.FreeMoving) {
            isFixating = true;
        } else if(path == PathType.Spotting) {
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
            foreach (Transform t in _patrolPath.path.waypoints) {
                float distance = Vector3.Distance(currentPos, t.position);
                if (distance < lowestDistance) {
                    lowestDistance = distance;
                    closestWaypoint = t;
                }
            }
            
            int currentIndex = _patrolPath.path.waypoints.IndexOf(closestWaypoint);
            Vector2 dir = (closestWaypoint.position - transform.position).normalized;
            if (_patrolPath.inverse == false) { // Look for the closest point to the right side
                if (dir.x < 0) {
                    _patrolPath.currentIndex = currentIndex;
                    // If the path already reached it's limit, do not try to force it to go further
                    if (_patrolPath.currentIndex == _patrolPath.path.waypoints.Count - 1) {
                        Debug.Log("Limit reached for path, not changing result");
                        return;
                    } else {
                        _patrolPath.currentIndex++;
                    }
                } else {
                    _patrolPath.currentIndex = currentIndex;
                }
            } else { // Look for the closest point to the left side
                if (dir.x > 0) {
                    _patrolPath.currentIndex = currentIndex;
                    // If the path already reached it's limit, do not try to force it to go further
                    if (_patrolPath.currentIndex == 0) {
                        Debug.Log("Limit reached for path, not changing result");
                        return;
                    } else {
                        _patrolPath.currentIndex--;
                    }
                } else {
                    _patrolPath.currentIndex = currentIndex;
                }
            }
        } 
        else if (pathToReturnTo == PathType.Surveying) {
            foreach (Transform t in _surveyPath.path.waypoints) {
                float distance = Vector3.Distance(currentPos, t.position);
                if (distance < lowestDistance) {
                    lowestDistance = distance;
                    closestWaypoint = t;
                }
            }
            int currentIndex = _surveyPath.path.waypoints.IndexOf(closestWaypoint);
            Vector2 dir = (closestWaypoint.position - transform.position).normalized;
            if (_surveyPath.inverse == false) { // Look for the closest point to the right side
                if (dir.x < 0) {
                    _surveyPath.currentIndex = currentIndex;
                    // If the path already reached it's limit, do not try to force it to go further
                    if (_surveyPath.currentIndex == _surveyPath.path.waypoints.Count - 1) {
                        Debug.Log("Limit reached for path, not changing result");
                        return;
                    } else {
                        _surveyPath.currentIndex++;
                    }
                } else {
                    _surveyPath.currentIndex = currentIndex;
                }
            } else { // Look for the closest point to the left side
                if (dir.x < 0) {
                    _surveyPath.currentIndex = currentIndex;
                    // If the path already reached it's limit, do not try to force it to go further
                    if (_surveyPath.currentIndex == 0) {
                        Debug.Log("Limit reached for path, not changing result");
                        return;
                    } else {
                        _surveyPath.currentIndex--;
                    }
                } else {
                    _surveyPath.currentIndex = currentIndex;
                }
            }
        }

        currentPath = pathToReturnTo;
        isPatrolling = true;
        
    }

    private void FixedUpdate() {
        switch (currentPath) {
            case PathType.Roaming:
                MoveAlongPath(_patrolPath);
                break;
            case PathType.Surveying:
                MoveAlongPath(_surveyPath);
                break;
            case PathType.FreeMoving:
                //if (isFixating) MoveToTargetPoint();
                break;
            case PathType.Spotting:
                if(isFixating && VisibilityManager.Instance.CanBeSpotted) MoveToWindow();
                break;
            case PathType.Static:
                if (VisibilityManager.Instance == null) return;

                //Vector2 currentPos = offsetAnchor.transform.position;
                Vector2 currentPos = transform.position;
                Vector2 targetPos = staticPointTransform.position;
                Vector2 dir = (targetPos - currentPos).normalized;
                float distance = Vector2.Distance(currentPos, targetPos);

                float t = Mathf.Clamp01(distance / arrivalThreshold);
                float ease = -(Mathf.Cos(Mathf.PI * t) - 1) / 2;

                rb.linearVelocity = dir * (staticSpeed * ease);
                break;
        }
    }
    private void MoveToWindow() {
        if (VisibilityManager.Instance == null) return;
        Vector2 targetPos = SpottingPath.path.ClosestWaypointToLocation(VisibilityManager.Instance.lastPlayerPositionSeen).position;
        // Decide position of eye offset depending on whether target is more right or left
        Vector2 decidingOffsetDistance = targetPos - (Vector2)transform.position;
        Vector2 currentPos = decidingOffsetDistance.x > 0 ? offsetAnchorRight.transform.position : offsetAnchorLeft.transform.position;
        Vector2 dir = (targetPos - currentPos).normalized;
        // Calculate easing to destination and add velocity
        float distance = Vector2.Distance(currentPos, targetPos);
        float t = Mathf.Clamp01(distance / arrivalThreshold);
        float ease = -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
        rb.linearVelocity = dir * (spottingSpeed * ease);
    }
    private void MoveAlongPath(PathCache pathCache) {
        if (!isPatrolling || pathCache?.path?.waypoints == null || pathCache.path.waypoints.Count == 0) return;

        if (_isWaiting) {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 currentPos = transform.position;
        Vector2 targetPos = pathCache.path.waypoints[pathCache.targetIndex].position;
        Vector2 dir = (targetPos - currentPos).normalized;
        float distance = Vector2.Distance(currentPos, targetPos);

        float currentSpeed = _speed;
        if (distance < arrivalThreshold) {
            currentSpeed = _speed * (distance / arrivalThreshold);
        }

        rb.linearVelocity = dir * currentSpeed;

        if (distance < 0.3f) {
            StartCoroutine(WaitAtWaypoint(pathCache));
        }
    }
    private IEnumerator WaitAtWaypoint(PathCache pathCache) {
        if (_isWaiting) yield break;

        _isWaiting = true;
        rb.linearVelocity = Vector2.zero;

        if(currentPath == PathType.Roaming) {
            yield return new WaitForSeconds(0);
        } else {
            yield return new WaitForSeconds(arrivalStopTime);
        }

        pathCache.currentIndex = pathCache.targetIndex;

        bool hitStart = pathCache.targetIndex == 0 && pathCache.inverse;
        bool hitEnd = pathCache.targetIndex == pathCache.path.waypoints.Count - 1 && !pathCache.inverse;
        bool hitDistanceLimit = currentPath == PathType.Roaming && RoomTracker.Instance != null && RoomTracker.Instance.isTooFar;

        if (hitStart) {
            pathCache.inverse = false;
        } else if (hitEnd) {
            pathCache.inverse = true;
        } else if (hitDistanceLimit) {
            pathCache.inverse = !pathCache.inverse;
        }

        pathCache.targetIndex += pathCache.inverse ? -1 : 1;
        pathCache.targetIndex = Mathf.Clamp(pathCache.targetIndex, 0, pathCache.path.waypoints.Count - 1);

        _isWaiting = false;
    }
    //private void MoveToTargetPoint() {
    //    if (VisibilityManager.Instance == null) return;

    //    Vector2 currentPos = offsetAnchor.transform.position;
    //    Vector2 targetPos = VisibilityManager.Instance.lastPlayerPositionSeen;
    //    Vector2 dir = (targetPos - currentPos).normalized;
    //    float distance = Vector2.Distance(currentPos, targetPos);

    //    float t = Mathf.Clamp01(distance / arrivalThreshold);
    //    float ease = -(Mathf.Cos(Mathf.PI * t) - 1) / 2;

    //    rb.linearVelocity = dir * (spottingSpeed * ease);
    //}
}

[System.Serializable]
public class PathCache {
    public PatrolPath path;
    public int currentIndex;
    public int targetIndex;
    public bool inverse; // whether it's going left>right(false) or left<right(true)
    public PathCache(PatrolPath _path, int _currentIndex, int _tarrgetIndex, bool _inverse) { 
        path = _path;
        currentIndex = _currentIndex;
        targetIndex = _tarrgetIndex;
        inverse = _inverse;
    }

    
}

public enum PathType {
    Roaming,
    Surveying,
    FreeMoving,
    Spotting,
    Static
}