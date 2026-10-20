using MoreMountains.CorgiEngine;
using UnityEngine;

public class ElevatorHelper : MonoBehaviour
{
    [SerializeField] private ButtonActivated buttonActivated;
    [SerializeField] private MovingPlatform movingPlatform;

    private void Awake()
    {
        buttonActivated.OnActivation.AddListener(HandleFirstActivation);
    }

    private void OnDestroy()
    {
        buttonActivated.OnActivation.RemoveAllListeners();
    }

    private void HandleFirstActivation()
    {
        movingPlatform.MoveTowardsEnd();
        buttonActivated.OnActivation.RemoveListener(HandleFirstActivation);
        buttonActivated.OnActivation.AddListener(HandleNextActivation);
    }

    private void HandleNextActivation()
    {
        movingPlatform.ResetEndReached();
    }

}
