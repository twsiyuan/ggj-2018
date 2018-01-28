using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(MenuScene)), CanEditMultipleObjects]
public class MenuSceneEditor : Editor
{

	private MenuScene script;


	private void OnEnable()
	{
		script = target as MenuScene;
	}

	public override void OnInspectorGUI()
	{    	
		DrawDefaultInspector();

		GUILayout.Space(5);

		EditorGUILayout.BeginHorizontal();
		{
			if(GUILayout.Button(new GUIContent("Add Level"), EditorStyles.miniButtonLeft))
				ResizeNumberOfGameScenes(true);

			if(GUILayout.Button(new GUIContent("Remove Level"),  EditorStyles.miniButtonRight))
				ResizeNumberOfGameScenes(false);
		}
		EditorGUILayout.EndHorizontal();
	}

	private void ResizeNumberOfGameScenes(bool add)
	{
		script.ResizeLevelAmount(add);
	}
	
}