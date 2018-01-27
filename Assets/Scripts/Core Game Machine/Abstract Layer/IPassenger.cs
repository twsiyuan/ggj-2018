public interface IPassenger
{
    bool IsWaiting { get; }
    bool IsMoving { get; }
    bool IsArrived { get; } 

    void AboardBus(IBus bus); 

    void PassThroughNextStation(IStation station, IBus bus);

    void GetOffFromBusAndArriveStation(IStation station);

    void UpdateRage();
}