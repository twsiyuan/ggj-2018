using System;
using UnityEngine;

[Serializable]
public class Passenger : IPassenger {

    private enum PassengerStatus {
        Waiting,
        Moving,
    }
    private PassengerStatus _status;

    private readonly int _rageMax;
    private int _rage;

    private IStation _start;
    private IStation _goal;

    public Passenger(IStation start, IStation goal) {
        _start = start;
        _goal = goal;
        _rage = 0; 
    }

    public void AboardBus(IBus bus) {
    }

    public void GetOffBus(IStation station) {
        if (station != _goal) {
            _remainWaitingAtStation(station);
            _addGetOffWrongStationRage();
        }
        else {
            _success();
        }
    }

    public void UpdateRage() {
        if (_status == PassengerStatus.Waiting) {
            _addWaitingRage();
        }
        else if (_status == PassengerStatus.Moving) {
            _addMovingRage();
        }
    }

    private void _addWaitingRage() {
    }

    private void _addMovingRage() {
    }

    private void _addGetOffWrongStationRage() {
    }

    private void _remainWaitingAtStation(IStation station) {
    }

    private void _success() {
    }
}
