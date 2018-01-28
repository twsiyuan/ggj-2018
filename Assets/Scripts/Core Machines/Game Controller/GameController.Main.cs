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

			var stations = this.stationsBuffer.Dequeue ();
             
            IBus bus = busCenter.LaunchBus(new List<IStation> (stations));
            
            _animateMgr.PlayBusAnimate(busCenter, bus); 
        }
	}

	void Update() 
	{
		if (this._passengerMgr != null) {
			_passengerMgr.UpdateTimer ();
		}

		if (this.passengerGenerator != null) {
			passengerGenerator.UpdateTimer ();
		}
	}
}