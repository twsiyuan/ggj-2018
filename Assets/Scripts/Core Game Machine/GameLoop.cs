using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class GameLoop : IGameLoop {

    private ISensorListener _sensorListener;

    private IAnimateManager _animateMgr;

    public void Initial(ISensorListener sensor, IAnimateManager animate) {
        _sensorListener = sensor;
        _animateMgr = animate;
    }

    public IEnumerator MainLoop() {
        while (true) {

            Debug.Log("start listen next bus mission");

            yield return _sensorListener.ListenNextBusPath();

            string log = "";
            _sensorListener.Result.ForEach((i)=> { log += i+ ", "; });
            Debug.Log("Listener get the result : "+ log);



            _animateMgr.PlayBusAnimate();
        }
    }
}