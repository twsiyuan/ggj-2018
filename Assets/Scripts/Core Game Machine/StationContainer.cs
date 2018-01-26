using System.Collections.Generic;
using UnityEngine;

public class StationContainer : MonoBehaviour
{	

    [SerializeField]
    private List<Station> beginStations;

    [SerializeField]
    private List<Station> hidingStations;

    public List<Station> GetBeginStations()
    {
        return beginStations;
    }

    public bool HasHideStations()
    {
        return hidingStations.Count > 0;
    }

    public Station GetStation()
    {
        var pos = UnityEngine.Random.Range(0, hidingStations.Count);

        var station = hidingStations[pos];

        hidingStations.RemoveAt(pos);

        return station;
    }

}