using MoreMountains.CorgiEngine;
using UnityEngine;

public class CheckpointUI : MonoBehaviour
{
    private bool checkpointReached = false;
    [SerializeField] private GameObject exitTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (checkpointReached == false && collision.CompareTag("Player"))
        {
            CameraUIAnimationController.Instance.TriggerCheckpointUI();
            CheckpointReached();
        }
    }

    private void CheckpointReached()
    {
        checkpointReached = true;
        exitTrigger.gameObject.SetActive(true);
    }

    public  void CheckpointLeft()
    {
        checkpointReached = false;
        exitTrigger.gameObject.SetActive(false);
        Debug.Log("checkpoint left");
    }
}
