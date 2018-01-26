using UnityEngine;
using UnityEditor;
using MarsCode113.ServiceFramework;

[CustomEditor(typeof(DataManager)), CanEditMultipleObjects]
public class DataManagerEditor : Editor
{

    private DataManager script;

    private string previewHeader;

    private string previewContent;

    private bool onUnitPreview;


    private void OnEnable()
    {
        script = target as DataManager;
    }


    public override void OnInspectorGUI()
    {
        if(Application.isPlaying) {
            GUILayout.Space(10);

            if(onUnitPreview)
                DrawPreviewUI();
            else
                DrawDataPool();

            GUILayout.Space(10);
        }
        else
            DrawDefaultInspector();
    }


    private void DrawDataPool()
    {
        GUILayout.Label(new GUIContent("Data Pool"));

        if(script.DataPool.Count > 0)
            foreach(var data in script.DataPool)
                DrawDataUnitSlot(data.Key, data.Value);
        else
            GUILayout.Label(new GUIContent("N/A"));
    }


    private void DrawDataUnitSlot(string index, PoolUnit<object> unit)
    {
        EditorGUILayout.BeginVertical("box");
        {
            GUILayout.Label(new GUIContent(index));

            DrawDataUnitContent(unit);

            if(GUILayout.Button("Preview", EditorStyles.miniButton)) {
                previewHeader = index;
                SetPreviewData(unit.Storage);
            }
        }
        EditorGUILayout.EndVertical();
    }


    private void DrawDataUnitContent(PoolUnit<object> unit)
    {
        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label(new GUIContent(unit.Storage.GetType().Name), EditorStyles.textField);

            GUILayout.Label(new GUIContent(unit.CacheTag.ToString()), EditorStyles.toolbarPopup, GUILayout.Width(110));
        }
        EditorGUILayout.EndHorizontal();
    }


    private void SetPreviewData(object data)
    {
        previewContent = JsonUtility.ToJson(data, true);

        onUnitPreview = true;
    }


    private void DrawPreviewUI()
    {
        EditorGUILayout.BeginHorizontal("box");
        {
            GUILayout.Label(new GUIContent("Preview"), GUILayout.Width(70));

            GUILayout.Label(new GUIContent(previewHeader), EditorStyles.textField);
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Label(new GUIContent(previewContent));

        if(GUILayout.Button("Back", EditorStyles.miniButton)) {
            onUnitPreview = false;
            previewContent = "";
        }
    }

}