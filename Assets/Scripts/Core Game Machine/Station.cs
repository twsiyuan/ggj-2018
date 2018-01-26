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

    }

    public List<IPassenger> GetPassengers(int seats)
    {
        return null;
    }
    
}