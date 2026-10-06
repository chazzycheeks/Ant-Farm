using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Animator Animator => GetComponentInChildren<Animator>();

    private void Awake()
    {
        instance = this;
        /*StartCoroutine(AwakeningBool());*/
    }
    private IEnumerator AwakeningBool()
    {
        yield return new WaitForSeconds(1.283f);
        Animator.SetBool("Awakening", false);
    }
}
