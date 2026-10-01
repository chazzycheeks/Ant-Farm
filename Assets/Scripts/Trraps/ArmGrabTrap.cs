using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;
using UnityEngine.UI;

public class ArmGrabTrap : MonoBehaviour {
    // References
    [HideInInspector] public GameObject playerGO;
    [HideInInspector] public TrappedStateHandler trappedStateHandler;

    [SerializeField] Slider struggleMeter;

    // Anchor Point
    [SerializeField] Transform anchorGrabbedPoint;

    // Break progress
    float breakProgress = 0;
    [SerializeField] float breakGoal = 2f;
    [SerializeField] float decayRate = 0.5f;

    // Disarm variables
    public bool disarmed = false;
    float DisarmedTrapTimer;
    [SerializeField] float DisarmTime = 2f;
    public bool UseSpline = false;
    SplineAnimate splineComponent;

    private void Awake() {
        if (UseSpline) {
            gameObject.TryGetComponent<SplineAnimate>(out splineComponent);
            if (splineComponent == null) Debug.LogError($"Spline component missing from: {this.gameObject.name}");
        }

    }

    public IEnumerator TrapTriggered() {
        trappedStateHandler.enabled = true;
        //Play grab animation
        PlayerController.instance.Animator.SetBool("Grabbed", true);
        PlayerController.instance.Animator.SetBool("Idle", false);
        breakProgress = 0;
        if(UseSpline) SplineToggle(true);
        struggleMeter = trappedStateHandler.SliderComponent;
        if (struggleMeter != null) {
            struggleMeter.value = 0f;
            struggleMeter.gameObject.SetActive(true);
        }
        StartCoroutine(BreakOutArmTrapTimer());

        while (disarmed == false) {
            playerGO.transform.position = anchorGrabbedPoint.position;
            yield return null;
        }
    }

    public void BreakFreeOutAttempt() {
        breakProgress += 1f;
        if (struggleMeter != null) struggleMeter.value = breakProgress / breakGoal;
    }

    IEnumerator BreakOutArmTrapTimer() {
        while (breakProgress < breakGoal && !disarmed) {
            if (breakProgress > 0) {
                breakProgress -= decayRate * Time.deltaTime;
                breakProgress = Mathf.Max(0, breakProgress);

                if (struggleMeter != null) struggleMeter.value = breakProgress / breakGoal;
            }
            yield return null;
        }

        if (breakProgress >= breakGoal && !disarmed) {
            if (struggleMeter != null) struggleMeter.gameObject.SetActive(false);
            struggleMeter = null;
            trappedStateHandler.enabled = false;
            trappedStateHandler.trap = null;
            PlayerController.instance.Animator.SetBool("Grabbed", false);
            PlayerController.instance.Animator.SetBool("Idle", true);
            StartCoroutine(DisarmTrap());
        }
    }

    public IEnumerator DisarmTrap() {
        DisarmedTrapTimer = 0;
        disarmed = true;
        while (DisarmedTrapTimer < DisarmTime) {
            DisarmedTrapTimer += Time.deltaTime;
            yield return null;
        }
        if(UseSpline) SplineToggle(false);
        disarmed = false;
    }

    void SplineToggle(bool pause) {
        if (pause) {
            splineComponent.Pause();
        } else {
            splineComponent.Play();
        }
    }
}