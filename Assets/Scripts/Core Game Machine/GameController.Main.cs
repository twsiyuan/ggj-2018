using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class GameController : MonoBehaviour
{ 
    IEnumerator MainLoop() {
		while (true) {

			if (this.stationsBuffer.Count <= 0) {
				yield return null;
				continue;
			}

			Debug.Log("start listen next bus mission");
			var stations = this.stationsBuffer.Dequeue ();
			Debug.Log("Listener get the result : "+ string.Join(",", stations.Select(v => v.Index.ToString()).ToArray()));

			IBus bus = new Bus(5, 5);
            bus.StartBusPath(new List<IStation>(stations));
			_animateMgr.PlayBusAnimate(bus);
		}
	}

	void Update() 
	{
		if (this._passengerMgr != null) {
			_passengerMgr.UpdateTimer ();
		}

		if (this._passengerGenerator != null) {
			_passengerGenerator.UpdateTimer ();
		}
	}
}