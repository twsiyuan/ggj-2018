using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Station : IStation
{

    [SerializeField]
    private int index;
    public int Index { get {return index;} }

    [SerializeField]
    private bool mainStation;

    [SerializeField]
    private int[] neighbors;

    private List<IPassenger> passengers;

    public Station() {
        passengers = new List<IPassenger>();
    }

    public bool IsMainStation()
    {
        return mainStation;
    }

	public bool IsNeighbor(int index)
    {
        foreach (var neighbor in neighbors)
            if(neighbor == index)
                return true;

        return false;
    }

    public void NewPassenger(IPassenger passenger)
    {
        passengers.Add(passenger);
    }

    public List<IPassenger> GetPassengers(int seats)
    {
        int num = passengers.Count > seats ? seats : passengers.Count;
        List<IPassenger> aboards = new List<IPassenger>();
        for (int i = 0; i < num; i++) {
            aboards.Add(passengers[i]);
        }
        passengers.RemoveRange(0, num);
        return aboards;
    }
    
}