using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VisibilityManager))]
public class VisibilityManagerEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        VisibilityManager script = (VisibilityManager)target;
        if (Application.isPlaying) {
            EditorGUILayout.LabelField($"Is Spottable: {script.isDiscoverable} ");
        } else {
            EditorGUILayout.LabelField("Is Spottable: false");
        }
        

    }
}