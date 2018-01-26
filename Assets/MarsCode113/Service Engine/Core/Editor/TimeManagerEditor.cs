using UnityEngine;
using UnityEditor;
using MarsCode113.ServiceFramework;

[CustomEditor(typeof(TimeManager)), CanEditMultipleObjects]
public class TimeManagerEditor : Editor
{

    private TimeManager script;


    private void OnEnable()
    {
        script = target as TimeManager;
    }


    public override void OnInspectorGUI()
    {
        if(Application.isPlaying)
            DrawTimeState();
        else
            DrawDefaultInspector();
    }


    private void DrawTimeState()
    {
        GUILayout.Space(10);

        DrawTimeScaleUI();

        DrawTimeScaleFlagUI();

        DrawPauseStateUI();

        GUILayout.Space(10);
    }


    private void DrawTimeScaleUI()
    {
        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label(new GUIContent("Time Scale"), GUILayout.Width(100));

            GUILayout.Label(new GUIContent(Time.timeScale.ToString()), EditorStyles.textField);
        }
        EditorGUILayout.EndHorizontal();
    }


    private void DrawTimeScaleFlagUI()
    {
        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label(new GUIContent("Time Scale Flag"), GUILayout.Width(100));

            GUILayout.Label(new GUIContent(script.TimeScaleFlag.ToString()), EditorStyles.textField);
        }
        EditorGUILayout.EndHorizontal();
    }


    private void DrawPauseStateUI()
    {
        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label(new GUIContent("On Paused"), GUILayout.Width(100));

            GUILayout.Label(new GUIContent(script.OnPause.ToString()), EditorStyles.toolbarDropDown);
        }
        EditorGUILayout.EndHorizontal();
    }

}