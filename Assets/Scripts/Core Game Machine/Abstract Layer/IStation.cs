using System.Collections.Generic;
public interface IStation
{	

    List<IPassenger> GetPassengers();


    bool IsMainStation();


	bool IsNeighbor();

}