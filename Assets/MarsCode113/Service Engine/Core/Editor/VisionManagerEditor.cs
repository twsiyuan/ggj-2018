using MarsCode113.ServiceFramework;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VisionManager)), CanEditMultipleObjects]
public class VisionManagerEditor : Editor
{

    private VisionManager script;


    private void OnEnable()
    {
        script = target as VisionManager;
    }


    public override void OnInspectorGUI()
    {
        if(Application.isPlaying) {
            GUILayout.Space(10);

            DrawSubpages();

            GUILayout.Space(10);
        }
        else
            DrawDefaultInspector();
    }


    private void DrawSubpages()
    {
        GUILayout.Label(new GUIContent("Subpages"));

        if(script.Subpages.Count > 0)
            foreach(var page in script.Subpages)
                DrawSubpageSlot(page.GetType().Name);
        else
            GUILayout.Label(new GUIContent("N/A"));
    }


    private void DrawSubpageSlot(string typeName)
    {
        EditorGUILayout.BeginHorizontal("box");
        {
            GUILayout.Label(new GUIContent("Page"), GUILayout.Width(70));

            GUILayout.Label(new GUIContent(typeName), EditorStyles.textField);
        }
        EditorGUILayout.EndHorizontal();
    }

}