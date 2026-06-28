using UnityEngine;

public class LadderTrigger : MonoBehaviour
{
    [SerializeField] private GameObject ladder;
    private bool ladderTriggerEntered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ladderTriggerEntered == false && collision.CompareTag("Player")) {
            LowerLadder();
        }
    }

    public void LowerLadder()
    {
        ladder.SetActive(true);
        ladderTriggerEntered = true;

    }
}
