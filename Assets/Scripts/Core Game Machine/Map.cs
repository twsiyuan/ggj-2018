using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour, IMap, IDragSensorManager
{	

    #region [ Fields / Properties - Stations]

    [SerializeField]
    private List<Station> stations;

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
        if(!station.IsMainStation())
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