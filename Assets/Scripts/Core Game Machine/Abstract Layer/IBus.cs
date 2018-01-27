using System.Collections.Generic;

public interface IBus
{
    int Distance { get; }

    void StartBusPath(List<IStation> path);
    List<IStation> BusPath { get; }

    void PassThroughStation(IStation station);

    void PassengerGetOff(IPassenger passenger);
}