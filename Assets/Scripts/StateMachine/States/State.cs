using UnityEngine;

public abstract class State : MonoBehaviour {
    [HideInInspector] public EntityStateTriggers entityStateTriggers;
    protected Animator animator;
    protected PatrolLocomotionController patrolController;
    protected virtual void Awake() {
        entityStateTriggers = GetComponentInParent<EntityStateTriggers>();
        if (entityStateTriggers == null) {
            Debug.LogWarning($"Entity State Triggers missing from state: {gameObject.name}");
        }
        animator = GetComponentInParent<Animator>();
        if (animator == null) {
            Debug.LogWarning($"Animator missing from state: {gameObject.name}");
        }
        patrolController = GetComponentInParent<PatrolLocomotionController>();
        if (patrolController == null) {
            Debug.LogWarning($"Patrol Locomotion Controller missing from state: {gameObject.name}");
        }
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
