using UnityEngine;
using MoreMountains.CorgiEngine;
using UnityEngine.Events;

public class Elevator : MovingPlatform
{
    [SerializeField] private UnityEvent onEndReached;

    protected override void EndReached()
    {
        base.EndReached();

        onEndReached?.Invoke();

        Debug.Log("Got to the end");
    }
}
