using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour 
{
	[SerializeField]
	Transform rootTransform = null;
	
	void Awake()
	{
		if (this.rootTransform != null) {
			var stations = this.transform.GetComponentsInChildren<MapStation> ();
			var links = this.transform.GetComponentsInChildren<MapLink> ();

			foreach (var link in links) {
				if (!link.IsValid) {
					Debug.LogErrorFormat (link, "Link({0}) is invalid", link.name);
				}
			}


		}
	}


}
