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

    private enum MoodStatus {
        Happy,
        Impatient,
        Furious
    }
    private MoodStatus _mood;

    private readonly int _rageMax = 3000;
    private int _rage;
    private readonly int _waitingRage = 1;
    private readonly int _movingRage = 0;
    private readonly int _wrongStationRage = 200;

    private IStation _start;
    private IStation _goal;
    private IStation _stand;
    private IPassengerView _view;
    public IPassengerView View { get { return _view; } }

    private GameController _gameCtrl;

    public event Action<IPassenger> StartWaitingEvent;
    public event Action<IPassenger> StopWaitingEvent;
    public event Action<IPassenger> SuccessArriveEvent;
    public event Action<IPassenger> AngryExitEvent;

    public Passenger(IStation start, IStation goal, IPassengerView view, GameController gameCtrl) {
        _start = start;
        _goal = goal;
        _rage = 0;
        _mood = MoodStatus.Happy;
        _view = view;
        _view.ChangeToFace1();
        _gameCtrl = gameCtrl;
    }

    public void AboardBus(IBus bus) {
        _status = PassengerStatus.Moving;
        _stand = null;

        if (StopWaitingEvent != null)
            StopWaitingEvent.Invoke(this);
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

        if (_status == PassengerStatus.Waiting ||
            _status == PassengerStatus.Moving) {

            _updateRageFace();

            if (_rage > _rageMax) {
                _fail();
            }
        }
    }

    private void _updateRageFace() {
        float rageScale = (float)_rage / (float)_rageMax;
        if (rageScale > 0.5f && rageScale < 0.8f && _mood == MoodStatus.Happy) {
            _mood = MoodStatus.Impatient;
            View.ChangeToFace2();
        }
        else if (rageScale > 0.8f && rageScale < 1.0f && _mood == MoodStatus.Impatient) {
            _mood = MoodStatus.Furious;
            View.ChangeToFace3();
        } 
    }

    public void WaitingAtStation(IStation station) {
        int order = station.NewPassenger(this);
        _status = PassengerStatus.Waiting;
        _stand = station;

        if (StartWaitingEvent != null)
            StartWaitingEvent.Invoke(this);

        if (_view != null) {
            _view.ShowViewPositionAtStation(station.Transform, order);
        }
    }

    private void _remainWaitingAtStation(IStation station) {
        WaitingAtStation(station);
        _addGetOffWrongStationRage();
    }

    private void _success() {
        _status = PassengerStatus.Arrived;
        if (SuccessArriveEvent != null)
            SuccessArriveEvent.Invoke(this);
    }

    private void _fail() {
        _status = PassengerStatus.Failed;
        if (StopWaitingEvent != null)
            StopWaitingEvent.Invoke(this);
        if (AngryExitEvent != null)
            AngryExitEvent.Invoke(this); 

        _view.FailedAnimate(_stand);
        _stand.ExitStation(this);
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
