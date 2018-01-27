using System;

public interface IPassenger
{
    bool IsWaiting { get; }
    bool IsMoving { get; }
    bool IsArrived { get; }

    IPassengerView View { get; }

    event Action<IPassenger> StartWaitingEvent;
    event Action<IPassenger> StopWaitingEvent;
    event Action<IPassenger> SuccessArriveEvent;
    event Action<IPassenger> AngryExitEvent;

    void AboardBus(IBus bus);

    void WaitingAtStation(IStation station);

    bool PassThroughNextStation(IStation station, IBus bus);

    bool GetOffFromBusAndArriveStation(IStation station);

    void UpdateRage();
}