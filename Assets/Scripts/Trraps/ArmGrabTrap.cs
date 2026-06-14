using System.Collections;
using UnityEngine;

public class ArmGrabTrap : MonoBehaviour {
    // References
    [HideInInspector] public GameObject playerGO; // Player reference to set position when trapped
    [HideInInspector] public TrappedStateHandler trappedStateHandler; // Trapping state for player
    // Anchor Point
    [SerializeField] Transform anchorGrabbedPoint; // Point where the player is held at
     //Break progress
    float breakProgress = 0;
    [SerializeField] float breakGoal = 2; // Amount of times you have to struggle when getting caught
    //Disarm variables
    public bool disarmed = false;
    float DisarmedTrapTimer;
    [SerializeField] float DisarmTime = 2f;

    void Start() {
        //playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    public IEnumerator TrapTriggered() {
        trappedStateHandler.enabled = true;
        breakProgress = 0;
        while (disarmed == false) {
            playerGO.transform.position = anchorGrabbedPoint.position;
            yield return null;
        }
        
    }
    public void BreakFreeOutAttempt() {
        if (breakProgress < breakGoal) {
            breakProgress++;
        } else {
            trappedStateHandler.enabled = false;
            StartCoroutine(DisarmTrap());
        }
    }

    public IEnumerator DisarmTrap() {
        DisarmedTrapTimer = 0;
        disarmed = true;
        while(DisarmedTrapTimer < DisarmTime) {
            DisarmedTrapTimer += Time.deltaTime;
            yield return null;
        }
        disarmed = false;
    }
}
