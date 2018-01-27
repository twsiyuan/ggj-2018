using System;
using UnityEngine;

[Serializable]
public class Passenger : IPassenger {

    private enum PassengerStatus {
        Waiting,
        Moving,
        Arrived,
        Failed,
    }
    private PassengerStatus _status; 
    public bool IsWaiting { get { return _status == PassengerStatus.Waiting; } }
    public bool IsMoving { get { return _status == PassengerStatus.Moving; } }
    public bool IsArrived { get { return _status == PassengerStatus.Arrived; } }

    private readonly int _rageMax = 5000;
    private int _rage;
    private readonly int _waitingRage = 1;
    private readonly int _movingRage = 0;
    private readonly int _wrongStationRage = 2;

    private IStation _start;
    private IStation _goal;
    private IPassengerView _view;
    public IPassengerView View { get { return _view; } }

    private GameController _gameCtrl;

    public Passenger(IStation start, IStation goal, IPassengerView view, GameController gameCtrl) {
        _start = start;
        _goal = goal;
        _rage = 0;
        _view = view;
        _gameCtrl = gameCtrl;

        _waitingAtStation(start);
    }

    public void AboardBus(IBus bus) {
        _status = PassengerStatus.Moving;
    } 

    public bool PassThroughNextStation(IStation station, IBus bus) {

        if (station == _goal) {
            bus.PassengerGetOff(this);
            _success();
            return true;
        }
        return false;
    }

    public bool GetOffFromBusAndArriveStation(IStation station) {
        if (station == _goal) {
            _success();
            return true;
        }
        else {
            _remainWaitingAtStation(station);
            return false;
        }
    }

    public void UpdateRage() {
        if (_status == PassengerStatus.Waiting) {
            _addWaitingRage();
        }
        else if (_status == PassengerStatus.Moving) {
            _addMovingRage();
        }

        _updateRageFace();

        if (_rage > _rageMax) {
            _fail();
        }
    }

    private void _updateRageFace() {
        float rageScale = (float)_rage / (float)_rageMax;
        if (rageScale < 0.5f) View.ChangeToFace1();
        else if (rageScale < 0.8f) View.ChangeToFace2();
        else View.ChangeToFace3();
    }

    private void _waitingAtStation(IStation station) {
        int order = station.NewPassenger(this);
        _status = PassengerStatus.Waiting;

        if (_view != null) {
            _view.ShowViewPositionAtStation(station.Transform, order);
        }
    }

    private void _remainWaitingAtStation(IStation station) {
        _waitingAtStation(station);
        _addGetOffWrongStationRage();
    }

    private void _success() {
        _status = PassengerStatus.Arrived;
    }

    private void _fail() {
        _status = PassengerStatus.Failed;
        Debug.Log("passenger ANGRY!");
        _gameCtrl.AddRage(1);
        _view.FailedAnimate();
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
