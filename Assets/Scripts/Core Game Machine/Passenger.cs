using System;
using UnityEngine;

[Serializable]
public class Passenger : IPassenger {

    private enum PassengerStatus {
        Waiting,
        Moving,
        Arrived,
    }
    private PassengerStatus _status;

    private readonly int _rageMax;
    private int _rage;
    private readonly int _waitingRage;
    private readonly int _movingRage;
    private readonly int _wrongStationRage;

    private IStation _start;
    private IStation _goal;

    public Passenger(IStation start, IStation goal) {
        _start = start;
        _goal = goal;
        _rage = 0; 
    }

    public void AboardBus(IBus bus) {
        _status = PassengerStatus.Moving;
    } 

    public void PassThroughNextStation(IStation station, IBus bus) {
        if (station == _goal) {
            bus.PassengerGetOff(this);
            _success();
        }
    }

    public void GetOffFromBusAndArriveStation(IStation station) {
        if (station == _goal) {
            _success();
        }
        else {
            _remainWaitingAtStation(station);
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

    public void WaitingAtStation(IStation station) {
        station.AddWaitingPassenger(this);
        _status = PassengerStatus.Waiting;
    }

    private void _remainWaitingAtStation(IStation station) {
        WaitingAtStation(station);
        _addGetOffWrongStationRage();
    }

    private void _success() {
        _status = PassengerStatus.Arrived;
    }

    private void _addWaitingRage() {
        _rage += _waitingRage;
    }

    private void _addMovingRage() {
        _rage += _movingRage;
    }

    private void _addGetOffWrongStationRage() {
        _rage += _wrongStationRage;
    }
}
