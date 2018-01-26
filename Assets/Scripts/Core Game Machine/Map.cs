using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : IMap
{	

    [SerializeField]
    private List<IStation> stations;


	public List<IStation> GetAllStations()
    {
        return stations;
    }

}