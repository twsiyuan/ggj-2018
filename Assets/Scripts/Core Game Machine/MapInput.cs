using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Map))]
public class MapInput : MonoBehaviour, IDragSensorManager
{	
	public class SelectStartEventArgs : System.EventArgs{
		public IStation Station {
			get;
			set;
		}
	}

	public class SelectEndEventArgs : System.EventArgs{
		public IEnumerable<IStation> Stations {
			get;
			set;
		}
	}

	public class SelectingEventArgs : System.EventArgs{
		public IEnumerable<IStation> Stations {
			get;
			set;
		}

		public Vector3 CurrentPosition {
			get;
			set;
		}
	}

	SelectingEventArgs selectingEventArgs = new SelectingEventArgs();

	public event System.EventHandler<SelectStartEventArgs> SelectStarted;

	public event System.EventHandler<SelectEndEventArgs> SelectEnded;

	public event System.EventHandler<SelectingEventArgs> SelectProcessing;

    #region [ Fields / Properties]

	Map map;

    private Vector2 pressPos;

    [SerializeField]
    private bool rejectRepeatLink = true;

    [SerializeField]
    private int numberOfLinkTimes;
    
    [SerializeField]
    private int start = -1;

    [SerializeField]
    private int end = -1;

    [SerializeField]
    private List<int> busTargets = new List<int>();

    [SerializeField]
    private List<string> linkFlags = new List<string>();

	private bool selecting;

    private Func<int> syncNumberOfLinkTimes;
    
    #endregion


    #region [ Sensors ]
    
	private void Awake(){
		map = GetComponent<Map>();
		map.MapChanged += this.OnMapChanged;
	}

	private void OnDestroy(){
		if (map != null) {
			map.MapChanged -= this.OnMapChanged;
		}
	}

    public void InitLinkTimesSyncCall(Func<int> syncNumberOfLinkTimes)
    {
        this.syncNumberOfLinkTimes = syncNumberOfLinkTimes;
    }

    private void RejectRepeatLink(bool rejectRepeatLink)
    {
        this.rejectRepeatLink = rejectRepeatLink;
    }


	private void OnMapChanged (object sender, System.EventArgs e)
	{
		// TODO: 地圖改變，修改 Sensor	
	}

    private void Start()
    {
        InitSensors();
    }
    
    private void InitSensors()
    {
        var id = 0;
		foreach (var s in this.map.GetAllStations()) {
			var g = s.Transform.GetComponent<MaskableGraphic> ();
			TableSlotDragSensor.Init (g, this, id++);
		}
    }

    public void RegisterSensor(int sensorID)
    {
        if(!this.enabled)
            return;

        var station = map.GetStation(sensorID);
        if(!station.IsMainStation)
            return;

        if(start == end)
            end = -1;

        start = sensorID;

        RegisterSensorHook();

		// This is start
		this.selecting = true;

		if (this.SelectStarted != null) {
			this.SelectStarted (this, new SelectStartEventArgs(){
				Station = map.GetStation(start),
			});
		}
    }

    public void OverlapSensor(int sensorID)
    {   
        if(!this.enabled)
            return;

        if(start == -1)
            return;

        if(!IsNeighbor(sensorID))
            return;

        var flag = string.Format("{0}-{1}", Mathf.Min(start, sensorID), Mathf.Max(start, sensorID));
        if(rejectRepeatLink && linkFlags.Contains(flag))
            return;

        linkFlags.Add(flag);

        end = sensorID;

        start = end;

        busTargets.Add(end);

        OverlapSensorHook();
    }

    bool IsNeighbor(int id)
    {
        var station = map.GetStation(id);
        return station.IsNeighbor(start);
    }

    public void SplitSensor(int sensorID)
    {   
        if(!this.enabled)
            return;

        if(end == -1)
            return;

        SplitSensorHook();

        end = -1;
    }

    public void RemoveSensor(int sensorID)
    {
        if(!this.enabled)
            return;

        if(start == -1)
            return;
  
		this.InvokeEndEvent ();
		this.ClearSelectionState ();
    }

	void OnDisable(){

		if (this.selecting) {
			this.InvokeEndEvent ();
		}

		this.ClearSelectionState ();

	}

	void ClearSelectionState(){
		RemoveSensorHook();

		start = -1;
		end = -1;
		this.selecting = false;
	}

	void Update(){
		if (this.selecting) {
			if (this.SelectProcessing != null) {
				var e = this.selectingEventArgs;
				e.Stations = busTargets.Select (v => map.GetStation (v));
				e.CurrentPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);

				this.SelectProcessing (this, e);
			}
		}
	}

	void InvokeEndEvent(){
		// This is end
		if (this.SelectEnded != null) {
			this.SelectEnded (this, new SelectEndEventArgs(){
				Stations = busTargets.Select(v => map.GetStation(v)),
			});
		}

		#if UNITY_EDITOR
		Debug.LogFormat ("Selected Bus: {0}", string.Join(", ", this.busTargets.Select(v => v.ToString()).ToArray()));
		#endif
	}

    public virtual void DragSensor(Vector2 pos) { }

    protected virtual void RegisterSensorHook()
    { 
        numberOfLinkTimes = syncNumberOfLinkTimes();
        
        busTargets.Add(start);
    }

    protected virtual void OverlapSensorHook()
     { 
        if(--numberOfLinkTimes == 0)
            RemoveSensor(end);
     }

    protected virtual void SplitSensorHook() { }

    protected virtual void RemoveSensorHook()
    {
        linkFlags.Clear();
        busTargets.Clear();
    }
  
    #endregion
    
}