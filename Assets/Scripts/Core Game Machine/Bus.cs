using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Bus : IBus {

    private int _distance;
    private int _capacity;

    private IStation _start;
    private IStation _goal;
    private List<IStation> _path;

    private List<IPassenger> _passengers;

    public Bus(int distance, int capacity) {
        _distance = distance;
        _capacity = capacity;
        _passengers = new List<IPassenger>();
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
        foreach (IPassenger passenger in _passengers) {
            passenger.PassThroughNextStation(station, this);
        }
    }

    private void _stationPassengersAboardBus(IStation station) {
        var waitingPassengers = station.GetPassengers(_capacity - _passengers.Count);
        _passengers.AddRange(waitingPassengers);
    }

    private void _busPassengersAllGetOff(IStation station) {
        foreach(IPassenger passenger in _passengers){
            _passengers.Remove(passenger);
            passenger.GetOffFromBusAndArriveStation(station);
        }
    }
}