using UnityEngine;

public class StateManager : MonoBehaviour {
    State currentState;
    [SerializeField] State defaultState;
    private void Start() {
        SwitchToNextState(defaultState);
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
        }
        nextState.DefaultState();
        currentState = nextState;
    }

    public void ForceSwapState(State stateForced) {
        SwitchToNextState(stateForced);
    }
}
