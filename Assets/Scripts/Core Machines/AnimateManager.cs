using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class AnimateManager : MonoBehaviour, IAnimateManager
{ 
    [SerializeField]
    private BusViewFactory _busViewFactory;

    public void PlayBusAnimate(BusCenter busCenter, IBus bus) { 
        StartCoroutine(_playBusAnimate(busCenter, bus));
    }

    private IEnumerator _playBusAnimate(BusCenter busCenter, IBus bus) {

        List<IStation> stations = bus.BusPath;

        IBusView _busView = _busViewFactory.MakeBusView();

        yield return _busView.InitAnimate(stations[0].Transform.position);
        yield return _waitPassThourghStation(bus, _busView, stations[0]);

        for (int i = 1; i < stations.Count; i++) {
			var road = stations [i - 1].GetRoad (stations[i]);
			yield return _busView.MoveToStationAnimate(road);
            yield return _waitPassThourghStation(bus, _busView, stations[i]);
        }

        yield return null;
         
        _busView.RemoveBusView();
        busCenter.RecycleBus(bus);
    }

    private IEnumerator _waitPassThourghStation(IBus bus, IBusView busView, IStation station) {
        int waitingNumberBase = station.WaitingNumber + 1;
        List<IPassenger> arriveds = new List<IPassenger>();
        List<IPassenger> getOffs = new List<IPassenger>();
        List<IPassenger> aboards = new List<IPassenger>();
        List<IPassenger> changedTypePassengers = bus.PassThroughNextStation();

        changedTypePassengers.ForEach((p) => {
            if (p.IsArrived) { arriveds.Add(p); }
            else if (p.IsWaiting) { getOffs.Add(p); }
            else if (p.IsMoving) { aboards.Add(p); }
        });

        yield return _arrivedAnimate(arriveds, busView);

        yield return _getOffAnimate(getOffs, station, waitingNumberBase);

        yield return _aboardAnimate(aboards, station);

         _rearrangeLineAnimate(station);
    }

    private IEnumerator _arrivedAnimate(List<IPassenger> arriveds, IBusView busView) {
        for (int i = 0; i < arriveds.Count; i++) {
            arriveds[i].View.ArrivedStationAnimate(busView.Transform, i, arriveds.Count);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator _getOffAnimate(List<IPassenger> getOffs, IStation station, int orderBase) { 
        for (int i = 0; i < getOffs.Count; i++) {
            yield return getOffs[i].View.GetOffBusToStationAnimation(station.Transform, i + orderBase);
        }
    }

    private IEnumerator _aboardAnimate(List<IPassenger> aboards, IStation station) {
        for (int i = 0; i < aboards.Count; i++) {
            yield return aboards[i].View.AboardBusAnimate(station.Transform);
        }
    }

    private void _rearrangeLineAnimate(IStation station) {
        station.RearrangePassengersView();
    }
}