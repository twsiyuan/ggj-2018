using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSetup : MonoBehaviour {

	[SerializeField]
	Texture2D normalCursor;

	[SerializeField]
	Vector2 normalCursorHotspot;

	[SerializeField]
	Texture2D clickCursor;

	[SerializeField]
	Vector2 clickCursorHotspot;

	void Awake()
	{
		Cursor.SetCursor (this.normalCursor, this.normalCursorHotspot, CursorMode.ForceSoftware);
		GameObject.DontDestroyOnLoad (this.gameObject);
	}

	void Update()
	{
		if (Input.GetMouseButton (0)) {
			Cursor.SetCursor (this.clickCursor, this.clickCursorHotspot, CursorMode.ForceSoftware);
		} else {
			Cursor.SetCursor (this.normalCursor, this.normalCursorHotspot, CursorMode.ForceSoftware);
		}
	}
}
