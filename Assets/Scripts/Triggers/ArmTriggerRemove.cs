using UnityEngine;

public class ArmTriggerRemove : MonoBehaviour
{
    [SerializeField] private GameObject arm;
    private bool armTriggerEntered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (armTriggerEntered == false)
        {
            RemoveArm();
        }
        else
        {
            return;
        }
    }

    public void RemoveArm()
    {
        arm.SetActive(false);
        armTriggerEntered = true;

    }
}
