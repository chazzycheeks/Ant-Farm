using System.Collections;
using UnityEngine;

public class ArmGrabTrap : MonoBehaviour {
    public GameObject playerGO;
    [SerializeField]Transform anchorGrabbedPoint;
    bool disarmed = false;
    float breakProgress = 0;
    [SerializeField] float breakGoal = 2;
    public TrappedStateHandler trappedStateHandler;
    void Start() {
        //playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    public IEnumerator TrapTriggered() {
        disarmed = false;
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
            disarmed = true;
            trappedStateHandler.enabled = false;
        }
    }
}
