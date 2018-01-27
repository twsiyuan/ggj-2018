using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLink : MonoBehaviour 
{
	[SerializeField]
	MapStation startStation;

	[SerializeField]
	MapStation endStation;

	[SerializeField]
	Transform[] linkPoints = new Transform[0];

	public MapStation StartStation
	{
		get{
			return this.startStation;
		}
	}

	public MapStation EndStation
	{
		get{
			return this.endStation;
		}
	}

	public IRoad Road{
		get{
			// TODO: 不同的設計...一個介面來處理??
			var l = new List<Vector3>();
			l.AddRange (this.GetLinkPoints().Select(v => v.position));
			return new Road(l.ToArray());
		}
	}

	public bool IsValid
	{
		get{
			return this.StartStation != null && this.EndStation != null && this.StartStation != this.EndStation;
		}
	}

	void OnDrawGizmos()
	{
		if (this.startStation == null || this.endStation == null) 
		{
			return;
		}

		Gizmos.color = Color.red;

		var itr = this.GetLinkPoints ().GetEnumerator ();
		itr.MoveNext ();
		var p1 = itr.Current.position;
		while (itr.MoveNext ()) {
			var p2 = itr.Current.position;
			Gizmos.DrawLine (p1, p2);
			p1 = p2;
		}
	}

	IEnumerable<Transform> GetLinkPoints(){
		yield return this.startStation.transform;
		foreach (var t in this.linkPoints) {
			if (t != null) {
				yield return t;
			}
		}
		yield return this.endStation.transform;
	}

	void Reset(){
		this.startStation = this.GetComponent<MapStation> ();
	}
}
