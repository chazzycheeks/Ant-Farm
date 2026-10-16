using UnityEngine;

public class SingleArmTrigger : MonoBehaviour
{

    [SerializeField] private GameObject arm;
    private bool oneArmTriggerEntered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (oneArmTriggerEntered == false && collision.CompareTag("Player"))
        {
            TurnOnArm();
        }
    }

    public void TurnOnArm()
    {
        arm.SetActive(true);
        gameObject.SetActive(false);
        oneArmTriggerEntered=true;
    }
}
