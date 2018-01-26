using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour, IMap
{	

    [SerializeField]
    private List<Station> stations;

    [SerializeField]
    private List<Station> hidingStations;


    public bool HasHidingStation()
    {
        return hidingStations.Count > 0; 
    }
    
    public void AddStation()
    {
        var pos = UnityEngine.Random.Range(0, hidingStations.Count);

        var station = hidingStations[pos];

        hidingStations.RemoveAt(pos);

        stations.Add(station);
    }

    public IStation GetStation(int index)
    {
        return stations[index];
    }

    public List<IStation> GetAllStations()
    {
        var output = new List<IStation>();

        foreach (var s in stations)
            output.Add(s);

        return output;
    }

}