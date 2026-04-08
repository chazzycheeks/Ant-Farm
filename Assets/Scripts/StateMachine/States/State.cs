using UnityEngine;

public abstract class State : MonoBehaviour {
    public EntityStateTriggers entityStateTriggers;

    protected virtual void Awake() {
        entityStateTriggers = GetComponentInParent<EntityStateTriggers>();
    }
    /// <summary>
    /// The tick of the state that will be run per frame. (Update)
    /// </summary>
    /// <returns> Returns the current or next state if it's done with it's task.</returns>
    public abstract State RunCurrentState();

    /// <summary>
    /// Default state to reset to when swapped to.
    /// </summary>
    public abstract void DefaultState();

    public abstract void ExitState();
}
