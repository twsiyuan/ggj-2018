public interface IPassengerManager
{
    void AddPassenger(IPassenger newPassenger);

    void UpdateTimer();

    int WaitingPeopleNumber { get; }
}