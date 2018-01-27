using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class GameLoop : IGameLoop {

    private ISensorListener _sensorListener;

    private IAnimateManager _animateMgr;

    private IPassengerManager _passengerMgr;
    private IPassengerGenerator _passengerGenerator;

    public void Initial(ISensorListener sensor, IAnimateManager animate) {
        _sensorListener = sensor;
        _animateMgr = animate;

        _passengerMgr = new PassengerManager();
        _passengerGenerator = new PassengerGenerator();
    }

    public IEnumerator MainLoop() {
        while (true) {

            Debug.Log("start listen next bus mission");

            yield return _sensorListener.ListenNextBusPath();

            string log = "";
            _sensorListener.Result.ForEach((i)=> { log += i+ ", "; });
            Debug.Log("Listener get the result : "+ log);

            IBus bus = new Bus(0, 0);
            _animateMgr.PlayBusAnimate(bus);
        }
    }

    public void Update() {
        _passengerMgr.UpdateTimer();
        _passengerGenerator.UpdateTimer();
    }
}