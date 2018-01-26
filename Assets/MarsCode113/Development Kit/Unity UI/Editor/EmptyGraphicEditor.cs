using UnityEditor;

[CustomEditor(typeof(EmptyGraphic)), CanEditMultipleObjects]
public class EmptyGraphicEditor : Editor
{

    EmptyGraphic script;


    void OnEnable()
    {
        script = target as EmptyGraphic;
    }


    public override void OnInspectorGUI()
    {
        var toggle = EditorGUILayout.Toggle("Raycast Target", script.raycastTarget);
        if(toggle != script.raycastTarget) {
            Undo.RecordObject(script, "Toggle raycast target");
            script.raycastTarget = toggle;
        }
    }

}