using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Bus : IBus {

    private int _distance;
    private int _capacity;
    private int _viewID;
    public int Distance { get { return _distance; } }
    public int ViewID { get { return _viewID; } }

    private IStation _start;
    private IStation _goal;
    private Queue<IStation> _path;
    public List<IStation> BusPath { get { return new List<IStation>(_path); } }
    public bool HasNextStationOnPath { get { return _path.Count != 0; } }

    private List<IPassenger> _passengers;

    public Bus(int distance, int capacity, int viewID) {
        _distance = distance;
        _capacity = capacity;
        _viewID = viewID;
        _passengers = new List<IPassenger>();
    }

	public IEnumerable<IPassenger> GetPassengers(){
		if (this._passengers != null) {
			foreach (var p in _passengers) {
				yield return p;
			}
		}
	}

    public void StartBusPath(List<IStation> path) {
        if (path.Count <= 0) {
            throw new Exception("wrong station path");
        }

        _path = new Queue<IStation>(path);
        _start = path[0];
        _goal = path[path.Count - 1];
    }

    public List<IPassenger> PassThroughNextStation() {
        IStation station = _path.Dequeue();
        if (_path.Count != 0) {
            var arriveds = _busPassengersCheckStation(station);
            var aboards = _stationPassengersAboardBus(station);
            arriveds.AddRange(aboards);
            return arriveds;
        }
        else {
            var dictInfo = _busPassengersAllGetOff(station);
            return dictInfo;
        }
    }

    public void PassengerGetOff(IPassenger passenger) {
        _passengers.Remove(passenger);
    }

    private List<IPassenger> _busPassengersCheckStation(IStation station) {
        List<IPassenger> arriveds = new List<IPassenger>();
        _passengers.ForEach( (passenger) => {
            bool isArrived = passenger.PassThroughNextStation(station, this); 
            if (isArrived) {
                arriveds.Add(passenger);
            }
        }); 
        return arriveds;
    }

    private List<IPassenger> _stationPassengersAboardBus(IStation station) {
		var waitingPassengers = new List<IPassenger> ();
		station.PickupPassengers(_capacity - _passengers.Count, waitingPassengers); 
        foreach (IPassenger passneger in waitingPassengers) {
            passneger.AboardBus(this);
            _passengers.Add(passneger);
        }
        return waitingPassengers;
    }

    private List<IPassenger> _busPassengersAllGetOff(IStation station) {
        List<IPassenger> getOffs = new List<IPassenger>();
        for (int i = 0; i < _passengers.Count; i++) {
            bool arrived = _passengers[i].GetOffFromBusAndArriveStation(station);
            getOffs.Add(_passengers[i]);
        }
        _passengers.Clear();
        return getOffs;
    }
}