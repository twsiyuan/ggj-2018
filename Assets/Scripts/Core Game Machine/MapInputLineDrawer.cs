using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MapInput))]
public class MapInputLineDrawer : MonoBehaviour
{	
	MapInput input;

	[SerializeField]
	LineRenderer linePrefab = null;

	LineRenderer lineInstance = null;

	void Awake()
	{
		this.input = this.GetComponent<MapInput> ();
		this.input.SelectStarted += this.OnStarted;
		this.input.SelectEnded += this.OnEnded;
		this.input.SelectProcessing += this.OnProcessing;

		var go = GameObject.Instantiate (this.linePrefab.gameObject, this.transform);
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.transform.localScale = Vector3.one;

		this.lineInstance = go.GetComponent<LineRenderer>();
	}

	void OnDestroy(){
		if (this.input != null) {
			this.input.SelectStarted -= this.OnStarted;
			this.input.SelectEnded -= this.OnEnded;
			this.input.SelectProcessing -= this.OnProcessing;
		}
	}

	void OnStarted (object sender, MapInput.SelectStartEventArgs e)
	{

	}

	List<Vector3> positions = new List<Vector3> ();

	void OnProcessing (object sender, MapInput.SelectingEventArgs e)
	{
		var pos = this.positions;
		pos.Clear ();

		var s1 = (IStation)null;
		foreach (var s in e.Stations) {
			if (s1 == null) {
				s1 = s;
				pos.Add (s1.Transform.position);
				continue;
			}

			var s2 = s;
			var road = s1.GetRoad (s2);

			for (var i = 0; i <= 100; i++) {
				var p = road.GetPosition (i/ 100f);
				pos.Add (p);
			}

			s1 = s2;
		}

		pos.Add (e.CurrentPosition);


		this.lineInstance.positionCount = pos.Count;
		for (var i = 0; i < pos.Count; i++) {
			this.lineInstance.SetPosition (i, pos[i]);
		}
	}

	void OnEnded (object sender, MapInput.SelectEndEventArgs e)
	{
		this.lineInstance.positionCount = 0;
	}


}