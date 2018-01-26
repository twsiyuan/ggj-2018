using UnityEngine;
using UnityEditor;
using MarsCode113.ServiceFramework;

[CustomEditor(typeof(ServiceEngine)), CanEditMultipleObjects]
public class ServiceEngineEditor : Editor
{

    private ServiceEngine script;


    private void OnEnable()
    {
        script = target as ServiceEngine;
    }


    public override void OnInspectorGUI()
    {
        if(Application.isPlaying) {
            Repaint();

            GUILayout.Space(10);

            DrawManagers();

            GUILayout.Space(10);

            DrawSceneManager();

            GUILayout.Space(10);
        }
    }


    private void DrawManagers()
    {
        GUILayout.Label(new GUIContent("Service Managers"));

        foreach(var manager in script.Managers)
            DrawManagerSlot(manager.Key, manager.Value.GetType().Name);
    }


    private void DrawManagerSlot(string systemTag, string typeName)
    {
        EditorGUILayout.BeginHorizontal("box");
        {
            GUILayout.Label(new GUIContent(systemTag), GUILayout.Width(70));

            GUILayout.Label(new GUIContent(typeName), EditorStyles.textField);
        }
        EditorGUILayout.EndHorizontal();
    }


    private void DrawSceneManager()
    {
        GUILayout.Label(new GUIContent("Scene Manager"));

        EditorGUILayout.BeginHorizontal("box");
        {
            GUILayout.Label(new GUIContent("Scene"), GUILayout.Width(70));

            GUILayout.Label(new GUIContent(GetSceneName()), EditorStyles.textField);
        }
        EditorGUILayout.EndHorizontal();
    }


    private string GetSceneName()
    {
        return script.Scene != null ? script.Scene.GetType().Name : "N/A"; ;
    }

}