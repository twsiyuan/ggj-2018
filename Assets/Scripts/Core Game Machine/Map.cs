using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour, IMap, IDragSensorManager
{	

    #region [ Fields / Properties - Stations]

    [SerializeField]
    private List<IStation> stations;

    [SerializeField]
    private List<Station> hidingStations;
    
    #endregion


    #region [ Fields / Properties - Sensor]
    
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private IMap map;

    [SerializeField]
    private int start = -1;

    [SerializeField]
    private int end = -1;

    [SerializeField]
    private Image[] marks;

    [SerializeField]
    private Image[] imageLinkers;

    [SerializeField]
    private EmptyGraphic[] sensors;

    [SerializeField]
    private List<int> busTargets = new List<int>();

    private Dictionary<string, Image> linkers = new Dictionary<string, Image>();

    private Vector2 pressPos;
    
    #endregion


    #region [ Stations ]
	// One-way link
	Dictionary<IStation, IStation> links = new Dictionary<IStation, IStation>();
    
    public bool HasHidingStation()
    {
        return hidingStations.Count > 0; 
    }
    
	public int AddStation(IStation station)
    {
        stations.Add(station);
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

		this.links.Add (stationA, stationB);
		this.links.Add (stationB, stationA);
	}

	public void AddLink (int indexA, int indexB){
		this.AddLink (GetStation(indexA), GetStation(indexB));
	}
		
	public bool IsNeighbor(int indexA, int indexB){
		return this.IsNeighbor (GetStation(indexA), GetStation(indexB));
	}
		
	public bool IsNeighbor(IStation stationA, IStation stationB){
		IStation temp;
		if (this.links.TryGetValue (stationA, out temp)) {
			if (stationB == temp) {
				return true;
			}
		}

		if (this.links.TryGetValue (stationB, out temp)) {
			if (stationA == temp) {
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
    
    #endregion


    #region [ Sensors ]
    
    protected virtual void Awake()
    {
        InitTable();

        InitLinkers();

        InitSensors();
    }

    private void InitTable()
    {
        map = GetComponent<IMap>();
    }

    private void InitLinkers()
    {
        foreach (var linker in imageLinkers)
            linkers.Add(linker.name, linker);

        imageLinkers = null;
    }
    
    private void InitSensors()
    {
        var id = 0;
        foreach(var s in sensors)
            TableSlotDragSensor.Init(s, this, id++);

        sensors = null;
    }

    public void RegisterSensor(int sensorID)
    {
        var station = map.GetStation(sensorID);
        if(!station.IsMainStation)
            return;

        if(start == end)
            end = -1;

        start = sensorID;

        RegisterSensorHook();
    }

    public void OverlapSensor(int sensorID)
    {
        if(start == -1)
            return;

        if(!IsNeighbor(sensorID))
            return;

        end = sensorID;

        var match = string.Format("{0}-{1}", Mathf.Min(start, end), Mathf.Max(start, end));
        EnableLinker(match, true);

        start = end;

        busTargets.Add(end);

        OverlapSensorHook();
    }

    private void EnableLinker(string index, bool enable)
    {
        var img = linkers[index];
        img.color = enable? new Color32(150, 150, 150, 255) : new Color32(150, 150, 150, 50);
    }

    bool IsNeighbor(int id)
    {
        var station = map.GetStation(id);
        return station.IsNeighbor(start);
    }

    public void SplitSensor(int sensorID)
    {
        if(end == -1)
            return;

        SplitSensorHook();

        end = -1;
    }

    public void RemoveSensor(int sensorID)
    {
        RemoveSensorHook();

        start = -1;

        end = -1;
    }

    public virtual void DragSensor(Vector2 pos) { }

    protected virtual void RegisterSensorHook()
    { 
         busTargets.Add(start);
    }

    protected virtual void OverlapSensorHook()
    {

    }

    protected virtual void SplitSensorHook() { }

    protected virtual void RemoveSensorHook()
    {
        busTargets.Clear();

        foreach (var linker in linkers)
            EnableLinker(linker.Key, false);
    }
    
    #endregion
    
}