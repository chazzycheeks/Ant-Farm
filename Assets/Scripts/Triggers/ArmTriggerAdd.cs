using System.Collections;
using UnityEngine;

public class ArmTriggerAdd : MonoBehaviour
{
    [SerializeField] private Animator arm;
    private bool armTriggerEntered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (armTriggerEntered == false && collision.CompareTag("Player")) {
            StartCoroutine(AddArm());
        }
    }

    public IEnumerator AddArm()
    {
        arm.SetTrigger("trapped");
        yield return new WaitForSeconds(1f);
        arm.SetTrigger("untrapped");
        armTriggerEntered = true;

    }
}
