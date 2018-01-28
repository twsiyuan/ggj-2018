using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Map))]
public class MapInput : MonoBehaviour, IDragSensorManager
{
    public class SelectStartEventArgs : System.EventArgs
    {
        public IStation Station {
            get;
            set;
        }
    }

    public class SelectEndEventArgs : System.EventArgs
    {
        public IEnumerable<IStation> Stations {
            get;
            set;
        }
    }

    public class SelectingEventArgs : System.EventArgs
    {
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

    #region [ Mono Behaviour ]

    private void Awake()
    {
        map = GetComponent<Map>();
        map.MapChanged += this.OnMapChanged;
    }

    private void Start()
    {
        InitSensors();
    }

    private void Update()
    {
        if(this.selecting) {
            if(this.SelectProcessing != null) {
                var e = this.selectingEventArgs;
                e.Stations = busTargets.Select(v => map.GetStation(v));
                e.CurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                this.SelectProcessing(this, e);
            }
        }
    }

    private void OnDisable()
    {
        if(this.selecting) {
            this.InvokeEndEvent();
        }

        this.ClearSelectionState();
    }

    private void OnDestroy()
    {
        if(map != null) {
            map.MapChanged -= this.OnMapChanged;
        }
    }

    #endregion

    #region [ Core ]

    public void StartDragging(int sensorID)
    {
        if(!this.enabled)
            return;

        var station = map.GetStation(sensorID);
        if(!station.IsMainStation)
            return;

        if(start == end)
            end = -1;

        start = sensorID;

        numberOfLinkTimes = syncNumberOfLinkTimes();

        busTargets.Add(start);

        // This is start
        this.selecting = true;

        if(this.SelectStarted != null) {
            this.SelectStarted(this, new SelectStartEventArgs() {
                Station = map.GetStation(start),
            });
        }
    }

    public void CheckInSensor(int sensorID)
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

        if(--numberOfLinkTimes == 0)
            CompleteDragging(end);
    }

    public void CheckOutSensor(int sensorID)
    {
        if(!this.enabled)
            return;

        if(end == -1)
            return;

        end = -1;
    }

    public void CompleteDragging(int sensorID)
    {
        if(!this.enabled)
            return;

        if(start == -1)
            return;

        this.InvokeEndEvent();
        this.ClearSelectionState();
    }

    private void InvokeEndEvent()
    {
        // This is end
        if(this.SelectEnded != null) {
            this.SelectEnded(this, new SelectEndEventArgs() {
                Stations = busTargets.Select(v => map.GetStation(v)),
            });
        }
    }

    private void ClearSelectionState()
    {
        start = -1;
        end = -1;
        this.selecting = false;

        linkFlags.Clear();
        busTargets.Clear();
    }

    #endregion

    #region [ Utils ]

    public void InitLinkTimesSyncCall(Func<int> syncNumberOfLinkTimes)
    {
        this.syncNumberOfLinkTimes = syncNumberOfLinkTimes;
    }

    public void SyncPointerLocation(Vector2 pos) { }

    public void RejectRepeatLink(bool rejectRepeatLink)
    {
        this.rejectRepeatLink = rejectRepeatLink;
    }

    private bool IsNeighbor(int id)
    {
        var station = map.GetStation(id);
        return station.IsNeighbor(start);
    }

    private void InitSensors()
    {
        var id = 0;
        foreach(var s in this.map.GetAllStations()) {
            var g = s.Transform.GetComponent<MaskableGraphic> ();
            TableGameDragSensor.Init(g, this, id++);
        }
    }

    private void OnMapChanged(object sender, System.EventArgs e)
    {
        // TODO: 地圖改變，修改 Sensor	
    }

    #endregion

}