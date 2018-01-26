using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMachine_Prototype : MonoBehaviour, IMap
{	
    
    [SerializeField]
    private List<IStation> stations;

    private StationAssigner assigner;

    private void Start()
    {
        assigner = GetComponent<StationAssigner>();

        AddBeginStations();
    }

    private void AddBeginStations()
    {
        var stations = assigner.GetBeginStations();
        stations.AddRange(stations);
    }

    public void AddStation(IStation station)
    {
        stations.Add(station);
    }

	public List<IStation> GetAllStations()
    {
        return stations;
    }

}