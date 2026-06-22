using UnityEngine;

public class AssignPathTrigger : MonoBehaviour
{
    PatrolLocomotionController locoController;
    [Tooltip("The survey path it should assign to the entity")]
    [SerializeField] PathCache surveyPath;
    private void Start() {
        locoController = FindFirstObjectByType<PatrolLocomotionController>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            locoController.SetSurveyPath(surveyPath);
        }

    }
    //private void OnTriggerStay2D(Collider2D collision) {
    //    if (collision.CompareTag("Player")) {
    //        locoController.SetSurveyPath(surveyPath);
    //    }
    //}
}
