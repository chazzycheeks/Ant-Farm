using System.Collections;
using UnityEngine;

public class LocomotionController : MonoBehaviour
{
    [SerializeField] PatrolPath patrolPath; // The patrol path
    int currentPointIndex = 0;
    Rigidbody2D rb;
    [SerializeField] float speed = 100f; // Velocity it's moving at
    [SerializeField] float arrivalThreshold = 3f; // Distance treshhold before it slows down the speed
    bool isPatrolling = true;
    bool inverseLoop = false;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start() {
        //StartCoroutine(MoveLoop());
    }
    private void Update() {
            

    }

    private void FixedUpdate() {
        if (isPatrolling) {
            // Local refernces
            Vector2 currentPos = transform.position;
            Vector2 targetPos = patrolPath.waypoints[currentPointIndex].position;
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
                if(currentPointIndex == 0 && inverseLoop == true) {
                    inverseLoop = false;
                }
                if (currentPointIndex == patrolPath.waypoints.Count -1 && inverseLoop == false) {
                    inverseLoop = true;
                }
                if (inverseLoop) {
                    currentPointIndex--;
                } else {
                    currentPointIndex++;
                }
                
            }
        }
    }
}
