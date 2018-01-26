public interface IPassengerGenerator
{
    void Initialize(IMap map, IPassengerManager passengerMgr);
    void UpdateTimer();
}