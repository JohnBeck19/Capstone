using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Generation))]
public class GenerationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();  // Draw the default inspector options

        Generation generationScript = (Generation)target;

        // Add button to generate the map
        if (GUILayout.Button("Generate Map"))
        {
            generationScript.GenerateMap();
        }

        // Add button to clear the map
        if (GUILayout.Button("Clear Map"))
        {
            generationScript.ClearMap();
        }
    }
}
