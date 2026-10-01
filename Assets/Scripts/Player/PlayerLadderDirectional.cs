using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine;
using Character = MoreMountains.CorgiEngine.Character;

[RequireComponent(typeof(CharacterLadder))]
[RequireComponent(typeof(Character))]
public class PlayerLadderDirectional : MonoBehaviour, MMEventListener<MMStateChangeEvent<CharacterStates.MovementStates>>
{
    private Character character;
    private CharacterLadder characterLadder;

    private void Awake()
    {
        character = GetComponent<Character>();
        characterLadder = GetComponent<CharacterLadder>();
    }

    private void OnEnable()
    {
        this.MMEventStartListening();
    }

    private void OnDisable()
    {
        this.MMEventStopListening();
    }

    public void OnMMEvent(MMStateChangeEvent<CharacterStates.MovementStates> eventType)
    {
        if (eventType.NewState != CharacterStates.MovementStates.LadderClimbing)
        {
            return;
        }

        if (characterLadder.CurrentLadder.TryGetComponent(out LadderController controller))
        {
            character.Face(controller.ForceFaceDirection);
        }
    }
}
