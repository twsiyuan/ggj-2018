public interface IPassenger
{
    bool IsWaiting { get; }
    bool IsMoving { get; }
    bool IsArrived { get; }

    IPassengerView View { get; }

    void AboardBus(IBus bus); 

    bool PassThroughNextStation(IStation station, IBus bus);

    bool GetOffFromBusAndArriveStation(IStation station);

    void UpdateRage();
}