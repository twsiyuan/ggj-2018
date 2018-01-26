﻿public interface IPassenger
{
    void WaitingAtStation(IStation station);

    void AboardBus(IBus bus); 

    void PassThroughNextStation(IStation station, IBus bus);

    void GetOffFromBusAndArriveStation(IStation station);

    void UpdateRage();
}