using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class GameController : MonoBehaviour
{
	//private IAnimateManager _animateMgr;

	private IPassengerManager _passengerMgr;
	private IPassengerGenerator _passengerGenerator;
	/*
	 void Initial(ISensorListener sensor, IAnimateManager animate) {
		_sensorListener = sensor;
		_animateMgr = animate;

		_passengerMgr = new PassengerManager();
		_passengerGenerator = new PassengerGenerator();
	}*/

	 IEnumerator MainLoop() {
		while (true) {

			if (this.stationsBuffer.Count <= 0) {
				yield return null;
				continue;
			}

			Debug.Log("start listen next bus mission");
			var stations = this.stationsBuffer.Dequeue ();
			Debug.Log("Listener get the result : "+ string.Join(",", stations.Select(v => v.Index.ToString()).ToArray()));

			IBus bus = new Bus(0, 0);
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