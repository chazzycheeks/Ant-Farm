using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;

public class TrappedStateHandler : MonoBehaviour
{
    CharacterDash charDash;
    CharacterJump charJump;
    CharacterHorizontalMovement charMov;
    InputManager _inputManager;
    public ArmGrabTrap trap;

    private void Awake() {
        charDash = GetComponent<CharacterDash>();
        charJump = GetComponent<CharacterJump>();
        charMov = GetComponent<CharacterHorizontalMovement>();
        _inputManager = InputManager.Instance;
    }

    private void OnEnable() {
        ToggleComponents(false);
    }
    private void OnDisable() {
        ToggleComponents(true);
    }

    void ToggleComponents(bool toggle) {
        charJump.enabled = toggle;
        charDash.enabled = toggle;
        charMov.enabled = toggle;
    }

    // Update is called once per frame
    void Update()
    {
        if(_inputManager.JumpButton.State.CurrentState == MMInput.ButtonStates.ButtonUp) { 
            trap.BreakFreeOutAttempt();
            
        }
    }
}
