using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PatrolPath))]
public class PatrolPathEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        GUILayout.Space(10);

        if (GUILayout.Button("Add Waypoint", GUILayout.Height(30))) {
            // Iterate over ALL selected objects
            foreach (Object obj in targets) {
                PatrolPath script = (PatrolPath)obj;

                Undo.RecordObject(script, "Add Waypoint");
                script.CreateWaypoint();

                // Register the creation of the new child object for Undo
                if (script.waypoints.Count > 0) {
                    Undo.RegisterCreatedObjectUndo(
                        script.waypoints[script.waypoints.Count - 1].gameObject,
                        "Create Waypoint Object"
                    );
                }
            }
        }

        GUILayout.Space(5);

        if (GUILayout.Button("Clear Path")) {
            if (EditorUtility.DisplayDialog("Clear Path?",
                "Are you sure you want to delete all waypoints on selected object(s)?", "Yes", "No")) {

                foreach (Object obj in targets) {
                    PatrolPath script = (PatrolPath)obj;
                    Undo.RecordObject(script, "Clear Path");
                    script.ClearPath();
                }
            }
        }
    }
}