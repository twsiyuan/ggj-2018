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

    #region [ Fields / Properties - Sensor]

	Map map;

    [SerializeField]
    private Image[] marks;

    [SerializeField]
    private Image[] imageLinkers;

    [SerializeField]
    private EmptyGraphic[] sensors;

    private Dictionary<string, Image> linkers = new Dictionary<string, Image>();

    private Vector2 pressPos;


    [SerializeField]
    private int start = -1;

    [SerializeField]
    private int end = -1;

    [SerializeField]
    private List<int> busTargets = new List<int>();

	bool selecting = false;
    
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

	private void OnMapChanged (object sender, System.EventArgs e)
	{
		// TODO: 地圖改變，修改 Sensor	
	}

    private void Start()
    {
        InitLinkers();
        InitSensors();
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
		foreach (var s in this.map.GetAllStations()) {
			var g = s.Transform.GetComponent<MaskableGraphic> ();
			TableSlotDragSensor.Init (g, this, id++);
		}

        sensors = null;
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

        end = sensorID;

        var match = string.Format("{0}-{1}", Mathf.Min(start, end), Mathf.Max(start, end));
        EnableLinker(match, true);

        start = end;

        busTargets.Add(end);

        OverlapSensorHook();
    }

    private void EnableLinker(string index, bool enable)
    {
		if (linkers.ContainsKey(index))
		{
        	var img = linkers[index];
        	img.color = enable? new Color32(150, 150, 150, 255) : new Color32(150, 150, 150, 50);
		}
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
         busTargets.Add(start);
    }

    protected virtual void OverlapSensorHook() { }

    protected virtual void SplitSensorHook() { }

    protected virtual void RemoveSensorHook()
    {
        busTargets.Clear();

        foreach (var linker in linkers)
            EnableLinker(linker.Key, false);

		// 
    }

  
    #endregion
    
}