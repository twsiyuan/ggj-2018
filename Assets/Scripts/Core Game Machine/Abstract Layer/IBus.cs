using System.Collections.Generic;

public interface IBus
{
    int Distance { get; }

    void StartBusPath(List<IStation> path);

    void PassThroughStation(IStation station);

    void PassengerGetOff(IPassenger passenger);
}