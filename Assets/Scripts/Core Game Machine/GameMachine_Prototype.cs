using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMachine_Prototype : MonoBehaviour, IDragSensorManager
{	
    
    #region [ Fields / Properties ]
    
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private IMap map;

    [SerializeField]
    private Image[] imageLinkers;

    [SerializeField]
    private int start = -1;

    [SerializeField]
    private int end = -1;

    [SerializeField]
    private Image[] marks;

    [SerializeField]
    private EmptyGraphic[] sensors;

    [SerializeField]
    private List<int> busTargets = new List<int>();

    private Dictionary<string, Image> linkers = new Dictionary<string, Image>();

    private Vector2 pressPos;
    
    #endregion

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
        SensorComplete.Invoke(busTargets);
        busTargets.Clear();

        foreach (var linker in linkers)
            EnableLinker(linker.Key, false);

		if (this.Completed != null) {
			this.Completed (this, new CompletedEventArgs(){
				Stations = null,
			});
		}
    }

    public Action<List<int>> SensorComplete;
	public event System.EventHandler<CompletedEventArgs> Completed;

	public class CompletedEventArgs : System.EventArgs
	{
		public List<IStation> Stations
		{
			get;
			set;
		}
	}
}

