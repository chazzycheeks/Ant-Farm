using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GamepadUISelector : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private PlayerInput playerInput;

    private void Awake()
    {
        playerInput.onControlsChanged += TrySelecting;
    }

    private void OnEnable()
    {
        TrySelecting(playerInput);
    }

    private void OnDestroy()
    {
       playerInput.onControlsChanged -= TrySelecting;
    }

    private void TrySelecting(PlayerInput input)
    {
        if (input.currentControlScheme != "Gamepad") { return; }
        EventSystem.current.SetSelectedGameObject(playButton.gameObject);
    }
}
