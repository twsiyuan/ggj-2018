using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class AnimateManager : MonoBehaviour, IAnimateManager
{
    private IBusView _busView;
    private IBusViewFactory _busViewFactory;

    public AnimateManager() {
        _busViewFactory = new BusViewFactory();
    }

    public void PlayBusAnimate(IBus bus) {

        Debug.Log("Play animate");

        StartCoroutine(_playBusAnimate(bus));
    }

    private IEnumerator _playBusAnimate(IBus bus) {

        List<IStation> stations = bus.BusPath;

        _busView = _busViewFactory.MakeBusView();

        yield return _busView.InitAnimate(stations[0].Transform); 

        for (int i = 1; i < stations.Count; i++) {
            IStation current = stations[i];
            Transform stationTransform = current.Transform;
            Debug.Log("go to " + stationTransform.position);
            yield return _busView.MoveToStationAnimate(stationTransform);
        }

        yield return null;
    }
}