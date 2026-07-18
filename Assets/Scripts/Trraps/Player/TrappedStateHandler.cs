using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class TrappedStateHandler : MonoBehaviour
{
    MoreMountains.CorgiEngine.Character character;
    CharacterHorizontalMovement charMov;
    CorgiController controller;
    CharacterDash charDash;
    CharacterJump charJump;
    InputManager _inputManager;
    public ArmGrabTrap trap;
    [SerializeField] Slider _sliderComponent;
    public Slider SliderComponent => _sliderComponent;
    private void Awake() {
        charDash = GetComponent<CharacterDash>();
        charJump = GetComponent<CharacterJump>();
        character = GetComponent<MoreMountains.CorgiEngine.Character>();
        controller = GetComponent<CorgiController>();
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
        charMov.ResetHorizontalSpeed();
        charMov.ResetInput();

        controller.SetForce(Vector2.zero);
        character.ConditionState.ChangeState(CharacterStates.CharacterConditions.Normal);
        character.MovementState.ChangeState(CharacterStates.MovementStates.Falling);

        charMov.AbilityPermitted = toggle;
        charDash.AbilityPermitted = toggle;
        charJump.AbilityPermitted = toggle;
    }

    // Update is called once per frame
    void Update()
    {
        if(_inputManager.JumpButton.State.CurrentState == MMInput.ButtonStates.ButtonUp) { 
            trap.BreakFreeOutAttempt();
            
        }
    }
}
