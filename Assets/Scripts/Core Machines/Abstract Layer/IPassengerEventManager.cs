using System;
 
public interface IPassengerEventManager
{ 
    event Action<IPassenger> StartWaitingEvent;
    event Action<IPassenger> StopWaitingEvent;
    event Action<IPassenger> SuccessArriveEvent;
    event Action<IPassenger> AngryExitEvent;
}