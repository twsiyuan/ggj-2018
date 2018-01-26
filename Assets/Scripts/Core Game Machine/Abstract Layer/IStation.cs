using System.Collections.Generic;
public interface IStation
{	

    List<IPassenger> GetPassengers();

    void AddWaitingPassenger(IPassenger passenger);

    bool IsMainStation();

	bool IsNeighbor();

}