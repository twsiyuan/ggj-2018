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
        List<IPassenger> waitingPassengers = station.GetPassengers();
        while (_canAboard(waitingPassengers)) {
            IPassenger nextPassenger = waitingPassengers[0];
            _passengers.Add(nextPassenger);
            waitingPassengers.Remove(nextPassenger);
        }
    }

    private bool _canAboard(List<IPassenger> waitingPassengers) {
        return _passengers.Count < _capacity && waitingPassengers.Count > 0;
    }

    private void _busPassengersAllGetOff(IStation station) {
        foreach(IPassenger passenger in _passengers){
            _passengers.Remove(passenger);
            passenger.GetOffFromBusAndArriveStation(station);
        }
    }
}