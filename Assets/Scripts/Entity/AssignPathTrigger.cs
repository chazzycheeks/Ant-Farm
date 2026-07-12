using Unity.VisualScripting;
using UnityEngine;

public class AssignPathTrigger : MonoBehaviour
{
    PatrolLocomotionController locoController;
    [Tooltip("The survey path it should assign to the entity")]
    [SerializeField] PathCache surveyPath;
    [Tooltip("Should the spotting path be changed upon entering this trigger?")]
    public bool AssignSpottingPath = true;
    [Tooltip("The spotting path it sohuld assign to the entity")]
    [SerializeField] PathCache spottingPath;
    [Tooltip("Should this be assigned as the default path?")]
    public bool AssignDefaultPath;

    private void Awake() {
        //Error cases
        if (surveyPath == null) Debug.LogWarning($"Object does not have survey path attached - {gameObject.name}");
        if (AssignSpottingPath && spottingPath == null) Debug.LogWarning($"Object does not have a spotting path attached -{gameObject.name}");
    }
    private void Start() {
        locoController = FindFirstObjectByType<PatrolLocomotionController>();
        if (AssignDefaultPath && locoController.SurveyPath != null) Debug.LogWarning($"Object: {gameObject.name} is overlapping an existing surveyPath, there's two objects with assign default path");
        if (AssignDefaultPath) {
            locoController.SetSurveyPath(surveyPath);
            if (AssignSpottingPath) locoController.SetSpottingPath(spottingPath);
            Debug.Log($"Assigning paths from object: {gameObject.name}");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            locoController.SetSurveyPath(surveyPath);
            if (AssignSpottingPath) locoController.SetSpottingPath(spottingPath);
        }

    }
    //private void OnTriggerStay2D(Collider2D collision) {
    //    if (collision.CompareTag("Player")) {
    //        locoController.SetSurveyPath(surveyPath);
    //    }
    //}
}
