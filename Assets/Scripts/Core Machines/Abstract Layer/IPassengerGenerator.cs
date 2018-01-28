using System;

public interface IPassengerGenerator
{
    void UpdateTimer();
    event Action<IPassenger> GeneratePassengerEvent;
}