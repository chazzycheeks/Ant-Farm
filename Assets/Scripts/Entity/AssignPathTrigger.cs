using UnityEngine;

public class AssignPathTrigger : MonoBehaviour
{
    PatrolLocomotionController locoController;
    [SerializeField] PathCache surveyPath;
    private void Awake() {
        locoController = FindFirstObjectByType<PatrolLocomotionController>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        locoController.SetSurveyPath(surveyPath);
    }
}
