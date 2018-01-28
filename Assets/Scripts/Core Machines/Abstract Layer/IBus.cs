using System.Collections.Generic;

public interface IBus
{
    int Distance { get; }
    int ViewID { get; }

    void StartBusPath(List<IStation> path);

    List<IStation> BusPath { get; }

    bool HasNextStationOnPath { get; }

    List<IPassenger> PassThroughNextStation();

    void PassengerGetOff(IPassenger passenger);
}