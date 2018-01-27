using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLink : MonoBehaviour 
{
	[SerializeField]
	MapStation startStation;

	[SerializeField]
	MapStation endStation;

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

	public bool IsValid
	{
		get{
			return this.StartStation != null && this.EndStation != null && this.StartStation == this.EndStation;
		}
	}

	void OnDrawGizmos()
	{
		if (this.startStation == null || this.endStation == null) 
		{
			return;
		}

		var p1 = this.startStation.transform.position;
		var p2 = this.endStation.transform.position;

		Gizmos.color = Color.green;
		Gizmos.DrawLine(p1, p2);
	
	}
}
