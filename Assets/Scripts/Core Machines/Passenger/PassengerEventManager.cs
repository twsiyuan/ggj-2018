using System;
using UnityEngine;

[Serializable]
public class PassengerEventManager : IPassengerEventManager {

    public event Action<IPassenger> StartWaitingEvent;
    public event Action<IPassenger> StopWaitingEvent;
    public event Action<IPassenger> SuccessArriveEvent;
    public event Action<IPassenger> AngryExitEvent;

    public PassengerEventManager(PassengerGenerator passengerGenerator) {  
        passengerGenerator.GeneratePassengerEvent += _addEventOnNewPassenger; 
    }

    private void _addEventOnNewPassenger(IPassenger passenger) { 
        passenger.StartWaitingEvent += _startWaiting;
        passenger.StopWaitingEvent += _stopWaiting;
        passenger.SuccessArriveEvent += _successArrive;
        passenger.AngryExitEvent += _angryExit;
    }

    private void _startWaiting(IPassenger passenger) {
        Debug.Log("Add");
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