using System.Collections;
using System.Threading;
using UnityEngine;

/// <summary>
/// Modified version of roaming to act as a static point
/// </summary>
public class RoamingStatic : Roaming {
    public float immunityToSpotting = 2.5f;
    float timeRunning = 0;
    Coroutine immunityCoroutine;
    public override void DefaultState() {
        transform.root.position = patrolController.GetCurrentPointLocation(PathType.Roaming);
        entityStateTriggers.isActivelyChasing = false;
        patrolController.OutOfChaseUpdate();
        entityStateTriggers.transitioned = true;
        entityStateTriggers.repeatImmunity = false;
        if(immunityCoroutine == null) {
            immunityCoroutine = StartCoroutine(ImmunityToSpottingTimer());
        } else {
            timeRunning = 0;
        }
    }

    public override void ExitState() {
    }

    public override State RunCurrentState() {
        if(entityStateTriggers.repeatImmunity) {
            timeRunning = 0;
            entityStateTriggers.repeatImmunity = false;
        }
        if (entityStateTriggers.noticedPlayer) {
            if (bDebugLog) Debug.Log("Noticed player, swaping to survey");
            return surveyState;
        } else {
            return this;
        }

    }

    IEnumerator ImmunityToSpottingTimer() {
        timeRunning = 0;
        entityStateTriggers.transitioned = false;
        Debug.Log("Can't spot");
        while(timeRunning < immunityToSpotting) {
            timeRunning += Time.deltaTime;

            yield return null;
        }
        Debug.Log("Can spot");
        entityStateTriggers.transitioned = true;
        immunityCoroutine = null;
    }

   
}
