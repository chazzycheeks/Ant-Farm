using UnityEngine;

public class VisibilityManagerHelper : MonoBehaviour
{
    VisibilityManager visibilityManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        visibilityManager = FindAnyObjectByType<VisibilityManager>();
    }
    public void ResetVisbilityManager()
    {
        visibilityManager.ResetOnSeenEvent();
    }

}
