using System;
using UnityEngine;

[Serializable]
public class PassengerEventManager : IPassengerEventManager {
    private IPassengerGenerator _passengerGenerator;

    public event Action<IPassenger> StartWaitingEvent;
    public event Action<IPassenger> StopWaitingEvent;
    public event Action<IPassenger> SuccessArriveEvent;
    public event Action<IPassenger> AngryExitEvent;

    public PassengerEventManager(IPassengerGenerator passengerGenerator) {
        StartWaitingEvent = null;
        StopWaitingEvent = null;
        SuccessArriveEvent = null;
        AngryExitEvent = null;

        _passengerGenerator = passengerGenerator;
        _passengerGenerator.GeneratePassengerEvent += _addEventOnNewPassenger; 
    }

    private void _addEventOnNewPassenger(IPassenger passenger) {
        passenger.StartWaitingEvent += _startWaiting;
        passenger.StopWaitingEvent += _stopWaiting;
        passenger.SuccessArriveEvent += _successArrive;
        passenger.AngryExitEvent += _angryExit;
    }

    private void _startWaiting(IPassenger passenger) {
        if (StartWaitingEvent != null)
            StartWaitingEvent(passenger);
    }
    private void _stopWaiting(IPassenger passenger) {
        if (StopWaitingEvent != null)
            StopWaitingEvent(passenger);
    }
    private void _successArrive(IPassenger passenger) {
        if (SuccessArriveEvent != null)
            SuccessArriveEvent(passenger);
    }
    private void _angryExit(IPassenger passenger) {
        if (AngryExitEvent != null)
            AngryExitEvent(passenger);
    }
}