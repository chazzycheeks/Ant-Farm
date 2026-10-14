using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour {
    State currentState;
    [SerializeField] State defaultState;

    public State LastStateUsed;
    [SerializeField] StaticEntityState narativeState;
    private void Start() {
        SwitchToNextState(defaultState);
        LastStateUsed = currentState;
    }
    private void Update() {
        RunStateMachine();
    }
    void RunStateMachine() {
        State nextstate = currentState?.RunCurrentState();
        if(nextstate != null && nextstate != currentState) {
            SwitchToNextState(nextstate);
        }
    }

    protected virtual void SwitchToNextState(State nextState) {
        //Debug.Log(nextState.name);
        if (currentState != null) {
            currentState.ExitState();
            LastStateUsed = currentState;
        }
        nextState.DefaultState();
        currentState = nextState;
    }

    public void ForceSwapState(State stateForced) {
        SwitchToNextState(stateForced);
    }
    

    public void ResetToDefault() {
        SwitchToNextState(defaultState);
    }

    public State GetCurrentState() {
        return currentState;
    }

    public void SwapToNarativeStuck() {
        SwitchToNextState(narativeState);
    }
    /// <summary>
    /// If we have an animation, call this method to grab the animation event from inside the state, i.e if we parse in "Hit", and the state matches it and has a function attached to the animation event, it will trigger.
    /// </summary>
    /// <param name="eventId">Value to match for events</param>
    public void TriggerAnimationEvent(string eventId) {
        if (currentState != null) {
            currentState.HandleAnimationEvent(eventId);
        }
    }
}
