using System.Collections.Generic;
using UnityEngine;

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
		this.stationsBuffer.Enqueue (e.Stations);
	}

    private void Start() {
        _passengerMgr = new PassengerManager();
        _passengerGenerator = new PassengerGenerator(map, _passengerMgr); 

		InitBusCenter();

        StartCoroutine(this.MainLoop());
    }

}