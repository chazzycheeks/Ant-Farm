using UnityEngine;

public class ArmTriggerAdd : MonoBehaviour
{
    [SerializeField] private GameObject arm;
    private bool armTriggerEntered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (armTriggerEntered == false)
        {
            AddArm();
        }
        else
        {
            return;
        }
    }

    public void AddArm()
    {
        arm.SetActive(true);
        armTriggerEntered = true;

    }
}
