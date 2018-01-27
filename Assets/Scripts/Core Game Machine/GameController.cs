using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MarsCode113.ServiceFramework;

public partial class GameController : MonoBehaviour
{
    
	[SerializeField]
    private AnimateManager _animateMgr;

    [SerializeField]
	private BusCenter busCenter;
	
	[SerializeField]
	private Map map;

	[SerializeField]
	private MapInput mapInput;

	[SerializeField]
	private ScoreBoard scoreBoard;

    [SerializeField]
    private PassengerViewFactory passengerViewFactory;

    private IPassengerManager _passengerMgr;

    private IPassengerGenerator _passengerGenerator;

    Queue<IStation[]> stationsBuffer = new Queue<IStation[]>();

	private void Reset(){
		this.map = this.GetComponentInChildren<Map> ();
		this.mapInput = this.GetComponentInChildren<MapInput> ();
	}

	private void Awake(){
		this.mapInput.SelectEnded += this.OnMapInput_SelectEnded;
	}

	private void OnDestroy(){
		if (this.mapInput != null) {
			this.mapInput.SelectEnded -= this.OnMapInput_SelectEnded;
		}
	}

	private void OnMapInput_SelectEnded (object sender, MapInput.SelectEndEventArgs e)
	{
		var count = e.Stations.Count ();
		if (count > 1) {
			this.stationsBuffer.Enqueue (e.Stations.ToArray ());
		}
	}

    private void Start() {
        _passengerMgr = new PassengerManager();
        _passengerGenerator = new PassengerGenerator(map, _passengerMgr, passengerViewFactory); 

		InitBusCenter();

        StartCoroutine(this.MainLoop());
    }

}