using MarsCode113.ServiceFramework;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CustomEditor(typeof(AssetManager)), CanEditMultipleObjects]
public class AssetManagerEditor : Editor
{

    private AssetManager script;


    private void OnEnable()
    {
        script = target as AssetManager;
    }


    public override void OnInspectorGUI()
    {
        if(Application.isPlaying) {
            GUILayout.Space(10);

            DrawAssetPool();

            GUILayout.Space(10);

            DrawAsyncLoadRequests();

            GUILayout.Space(10);
        }
        else
            DrawDefaultInspector();
    }


    private void DrawAssetPool()
    {
        GUILayout.Label(new GUIContent("Asset Pool"));

        if(script.AssetPool.Count > 0)
            foreach(var unit in script.AssetPool)
                DrawAssetUnitSlot(unit.Key, unit.Value);
        else
            GUILayout.Label(new GUIContent("N/A"));
    }


    private void DrawAssetUnitSlot(string key, PoolUnit<Object> unit)
    {
        EditorGUILayout.BeginVertical("box");
        {
            GUILayout.Label(new GUIContent(key));

            DrawAssetUnitContent(unit);
        }
        EditorGUILayout.EndVertical();
    }


    private void DrawAssetUnitContent(PoolUnit<Object> unit)
    {
        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label(new GUIContent(unit.Storage.name), EditorStyles.textField);

            GUILayout.Label(new GUIContent(unit.CacheTag.ToString()), EditorStyles.toolbarPopup, GUILayout.Width(105));
        }
        EditorGUILayout.EndHorizontal();
    }


    private void DrawAsyncLoadRequests()
    {
        GUILayout.Label(new GUIContent("Async Load Requests"));

        if(script.AsyncLoadRequests.Count > 0)
            foreach(var unit in script.AsyncLoadRequests)
                DrawLoadRequestSlot(unit.Key, unit.Value);
        else
            GUILayout.Label(new GUIContent("N/A"));
    }


    private void DrawLoadRequestSlot(string index, AsnycLoadRequestBase request)
    {
        var rect = GUILayoutUtility.GetRect(EditorGUIUtility.currentViewWidth - 230, 17);
        var progress = request.GetRequestProgress();
        EditorGUI.ProgressBar(rect, progress, index);
    }

}