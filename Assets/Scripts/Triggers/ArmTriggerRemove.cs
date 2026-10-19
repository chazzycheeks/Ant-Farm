using UnityEngine;

public class ArmTriggerRemove : MonoBehaviour
{
    [SerializeField] private GameObject arm;
    [SerializeField] private GameObject armblock;
    [SerializeField] private GameObject armFallen;
    private bool armTriggerEntered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (armTriggerEntered == false && collision.CompareTag("Player")) {
            RemoveArm();
        }
    }

    public void RemoveArm()
    {
        arm.SetActive(false);
        armblock.SetActive(false);
        armTriggerEntered = true;
        armFallen.SetActive(true);

    }
}
