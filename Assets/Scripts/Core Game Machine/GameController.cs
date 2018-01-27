using UnityEngine;
using System.Collections.Generic;

public partial class GameController : MonoBehaviour
{
    [SerializeField]
    private AnimateManager _animateMgr;

    [SerializeField]
	private MapInput mapInput;

	[SerializeField]
	private Map map;

    [SerializeField]
    private PassengerViewFactory passengerViewFactory;

    private IPassengerManager _passengerMgr;
    private IPassengerGenerator _passengerGenerator;

    Queue<IStation[]> stationsBuffer = new Queue<IStation[]>();

	void Reset(){
		this.map = this.GetComponentInChildren<Map> ();
		this.mapInput = this.GetComponentInChildren<MapInput> ();
	}

	void Awake(){
		this.mapInput.SelectEnded += this.OnMapInput_SelectEnded;
	}

	void OnDestroy(){
		if (this.mapInput != null) {
			this.mapInput.SelectEnded -= this.OnMapInput_SelectEnded;
		}
	}

	void OnMapInput_SelectEnded (object sender, MapInput.SelectEndEventArgs e)
	{
		this.stationsBuffer.Enqueue (e.Stations);
	}

    void Start() {
        _passengerMgr = new PassengerManager();
        _passengerGenerator = new PassengerGenerator(map, _passengerMgr, passengerViewFactory); 

        StartCoroutine(this.MainLoop());
    }
}