using System.Collections;
using System.Collections.Generic;
using ListExtension.ElementControl;
using MarsCode113.ServiceFramework;
using UnityEngine;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour {

	
	[SerializeField]
	private List<Image> levels;

	[SerializeField]
	private string[] levelName;

	private void Start()
	{
		int id = 0;

		foreach (var lv in levels)
			UIClickSensor.Init(lv, LevelClickEvent, id++);
	}

	private void LevelClickEvent(UIClickSensor ev)
	{
		ServiceEngine.Instance.SwitchScene(levelName[ev.Id]);
	}

	#region [Editor Compilation]
	#if UNITY_EDITOR

	public RectTransform scrollContent;

	public void	ResizeLevelAmount(bool add)
	{
		if(add) {
			var obj = Instantiate(levels[0].gameObject, scrollContent.transform);
			var img = obj.GetComponent<Image>();
			levels.Add(img);

			obj.name = "Scene" + levels.Count.ToString();
		}
		else {
			if(levels.Count == 1)
				return;
			
			DestroyImmediate(levels.Pop().gameObject);
		}

		var layout = scrollContent.GetComponent<GridLayoutGroup>();

		var levelAmount = levels.Count;
		var width = layout.cellSize.x * levelAmount + layout.spacing.x * (levelAmount - 1);

		scrollContent.sizeDelta = new Vector2(width, scrollContent.sizeDelta.y);

		

		layout.enabled = true;
		StartCoroutine(DisableLayout(layout));
	}

	IEnumerator DisableLayout(GridLayoutGroup layout)
	{
		yield return new WaitForEndOfFrame();
		layout.enabled = false;
	}

	#endif
	#endregion

}
