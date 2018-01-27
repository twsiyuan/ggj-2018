using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Bus : IBus {

    private int _distance;
    private int _capacity;
    public int Distance { get { return _distance; } }

    private IStation _start;
    private IStation _goal;
    private IStation _current;
    private List<IStation> _path;
    public List<IStation> BusPath { get { return _path; } }

    private List<IPassenger> _passengers;

    public Bus(int distance, int capacity) {
        _distance = distance;
        _capacity = capacity;
        _passengers = new List<IPassenger>();
    }

    public void StartBusPath(List<IStation> path) {
        if (path.Count <= 0) {
            throw new Exception("wrong station path");
        }

        _path = path;
        _start = path[0];
        _goal = path[path.Count - 1];
         
    }

    public void PassThroughStation(IStation station) {
        if (station != _goal) {
            _busPassengersCheckStation(station);
            _stationPassengersAboardBus(station);
        }
        else {
            _busPassengersAllGetOff(station);
        }
    }

    public void PassengerGetOff(IPassenger passenger) {
        _passengers.Remove(passenger);
    }

    private void _busPassengersCheckStation(IStation station) {
        Debug.Log(_passengers + "" + _passengers.Count);
        for (int i = 0; i < _passengers.Count; i++) {
            _passengers[i].PassThroughNextStation(station, this);
        } 
    }

    private void _stationPassengersAboardBus(IStation station) {
		var waitingPassengers = new List<IPassenger> ();
		station.PickupPassengers(_capacity - _passengers.Count, waitingPassengers);
        foreach (IPassenger passneger in waitingPassengers) {
            passneger.AboardBus(this);
            _passengers.Add(passneger);
        }
    }

    private void _busPassengersAllGetOff(IStation station) {
        for (int i = 0; i < _passengers.Count; i++) {
            _passengers[i].GetOffFromBusAndArriveStation(station);
        }
        _passengers.Clear();
    }
}