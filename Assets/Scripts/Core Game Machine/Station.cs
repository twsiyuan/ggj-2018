using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Station : IStation
{
	public IMap Map
	{
		get;
		internal set;
	}

	public bool IsMainStation {
		get;
		set;
	}

	public int Index { 
		get
		{
			return this.Map.GetStationIndex (this);
		}
	}
		
	private List<IPassenger> passengers = new List<IPassenger>();

	public bool IsNeighbor(int index)
    {
		return this.Map.IsNeighbor (this.Index, index);
    }
		
	public bool IsNeighbor(IStation station){
		return this.Map.IsNeighbor(this, station);
	}

    public void NewPassenger(IPassenger passenger)
    {
        passengers.Add(passenger);
    }

	public void PickupPassengers(int seats, List<IPassenger> output)
    {
        int num = passengers.Count > seats ? seats : passengers.Count;
		output.Clear ();
        for (int i = 0; i < num; i++) {
			output.Add(passengers[i]);
        }
        this.passengers.RemoveRange(0, num);
    }
    
}