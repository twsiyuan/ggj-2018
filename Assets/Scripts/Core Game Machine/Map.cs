using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour, IMap
{	
	public event System.EventHandler MapChanged;

	List<IStation> stations = new List<IStation>();

	// One-way link
	struct Link{
		public IStation V1 {
			get;
			set;
		}

		public IStation V2 {
			get;
			set;
		}

		public bool IsLinked(IStation v1, IStation v2){
			return (this.V1 == v1 && this.V2 == v2) ||
				(this.V1 == v2 && this.V2 == v1);
		}
	}

	List<Link> links = new List<Link>();

	bool editing = false;
	bool dirty = false;

	public void StartEditing(){
		this.editing = true;
	}

	public void StopEditing(){
		this.editing = false;
		if (this.dirty) {
			this.dirty = false;

			if (this.MapChanged != null) {
				this.MapChanged (this, System.EventArgs.Empty);
			}
		}
	}

	void OnEdited(){
		if (this.editing) {
			this.dirty = true;
		} else {
			this.dirty = false;
			if (this.MapChanged != null) {
				this.MapChanged (this, System.EventArgs.Empty);
			}
		}
	}
 
	public int AddStation(IStation station)
    {
        stations.Add(station);

		if (station is Station) {
			(station as Station).Map = this;
		}

		this.OnEdited ();
		return stations.Count - 1;
    }

    public IStation GetStation(int index)
    {
        return stations[index];
    }
		
	public int GetStationIndex(IStation station)
	{
		return stations.IndexOf (station);
	}

	public void AddLink (IStation stationA, IStation stationB){
		if (stationA == null || stationB == null) {
			throw new System.ArgumentNullException ();
		}

		this.links.Add (new Link () {
			V1 = stationA,
			V2 = stationB,
		});

		this.OnEdited ();
	}

	public void AddLink (int indexA, int indexB){
		this.AddLink (GetStation(indexA), GetStation(indexB));
	}
		
	public bool IsNeighbor(int indexA, int indexB){
		return this.IsNeighbor (GetStation(indexA), GetStation(indexB));
	}
		
	public bool IsNeighbor(IStation stationA, IStation stationB){
		foreach (var l in this.links) {
			if (l.IsLinked (stationA, stationB)) {
				return true;
			}
		}

		return false;
	}

	public IEnumerable<IStation> GetAllStations()
	{
		foreach (var s in stations) {
			yield return s;
		}
	}

	public void GetAllStations(List<IStation> output)
    {
		output.Clear ();
        foreach (var s in stations)
            output.Add(s);
    }
    
}