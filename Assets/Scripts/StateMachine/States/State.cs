using UnityEngine;

public abstract class State : MonoBehaviour {
    public EntityStateTriggers entityStateTriggers;
    protected Animator animator;
    protected virtual void Awake() {
        entityStateTriggers = GetComponentInParent<EntityStateTriggers>();
        animator = GetComponentInParent<Animator>();
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
    /// <summary>
    /// Override this function to trigger functions inside states whenever animations happen, such as "Hit" in attack.
    /// </summary>
    /// <param name="eventId">The event ID name to look for to match.</param>
    public virtual void HandleAnimationEvent(string eventId) { }
}
