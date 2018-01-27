using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour 
{
	[SerializeField]
	bool generateOnAwake = true;

	[SerializeField]
	Map generateTarget = null;

	void Awake()
	{
		if (this.generateOnAwake) {
			this.Generate (this.generateTarget);
		}
	}

	public void Generate(IMap target){
		if (target == null)
			throw new System.ArgumentNullException ();

		var stations = this.transform.GetComponentsInChildren<MapStation> ();
		var links = this.transform.GetComponentsInChildren<MapLink> ();

		foreach (var link in links) {
			if (!link.IsValid) {
				Debug.LogErrorFormat (link, "Link({0}) is invalid", link.name);
			}
		}

		var map = new Dictionary<MapStation, IStation> ();
		foreach (var s in stations) {
			var ss = new Station () {
				IsMainStation = s.IsMainStation,
				Transform = s.transform,
			};

			target.AddStation (ss);
			map.Add (s, ss);
		}

		foreach (var l in links) {
			if (l.IsValid) {
				// TODO: Check map
				var ss = map[l.StartStation];
				var es = map[l.EndStation];

				target.AddLink (ss, es);
			}
		}
	}

}
