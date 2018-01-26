using System.Collections.Generic;

public interface IBus
{
    void StartBusPath(List<IStation> path);

    void PassThroughStation(IStation station);

    void PassengerGetOff(IPassenger passenger);
}