using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class AnimateManager : MonoBehaviour, IAnimateManager
{ 
    [SerializeField]
    private BusViewFactory _busViewFactory;

	public class BusEventArgs : EventArgs
	{
		public IBus Bus {
			get;
			set;
		}

		public IBusView BusView {
			get;
			set;
		}
	}

    public event Action StartBusEvent;
    public event Action BusDoorOpenEvent;
	public event EventHandler<BusEventArgs> BusArrived;
	public event EventHandler<BusEventArgs> BusLeaved;

    void Awake() {
        StartBusEvent = null;
        BusDoorOpenEvent = null;
    }

    public void PlayBusAnimate(BusCenter busCenter, IBus bus) { 
        StartCoroutine(_playBusAnimate(busCenter, bus));
    }

    private IEnumerator _playBusAnimate(BusCenter busCenter, IBus bus) {

        List<IStation> stations = bus.BusPath;

        IBusView _busView = _busViewFactory.MakeBusView(bus);

        if (StartBusEvent != null)
            StartBusEvent.Invoke();

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

		if (BusArrived != null) {
			BusArrived (this, new BusEventArgs(){
				Bus = bus,
				BusView = busView,
			});
		}

        yield return _arrivedAnimate(arriveds, bus, busView);

		yield return _getOffAnimate(getOffs, bus, busView, station, waitingNumberBase);

		yield return _aboardAnimate(aboards, bus, busView, station);

		if (BusLeaved != null) {
			BusLeaved (this, new BusEventArgs(){
				Bus = bus,
				BusView = busView,
			});
		}

         _rearrangeLineAnimate(station);
    }

	private IEnumerator _arrivedAnimate(List<IPassenger> arriveds, IBus bus, IBusView busView) {
        for (int i = 0; i < arriveds.Count; i++) {
            arriveds[i].View.ArrivedStationAnimate(busView.Transform, i, arriveds.Count);
             
            if (BusDoorOpenEvent != null)
                BusDoorOpenEvent.Invoke();



            yield return new WaitForSeconds(0.2f);
        }
    }

	private IEnumerator _getOffAnimate(List<IPassenger> getOffs, IBus bus, IBusView busView, IStation station, int orderBase) { 
        for (int i = 0; i < getOffs.Count; i++) {
            if (BusDoorOpenEvent != null)
                BusDoorOpenEvent.Invoke();

            yield return getOffs[i].View.GetOffBusToStationAnimation(station.Transform, i + orderBase);
        }
    }

	private IEnumerator _aboardAnimate(List<IPassenger> aboards, IBus bus, IBusView busView, IStation station) {
        for (int i = 0; i < aboards.Count; i++) {
            yield return aboards[i].View.AboardBusAnimate(station.Transform);

            if (BusDoorOpenEvent != null)
                BusDoorOpenEvent.Invoke();


        }
    }

    private void _rearrangeLineAnimate(IStation station) {
        station.RearrangePassengersView();
    }
}