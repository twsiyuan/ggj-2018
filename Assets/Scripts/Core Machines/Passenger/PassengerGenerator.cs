using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassengerGenerator : MonoBehaviour
{
    private System.Random _rand;
    private float _timer;
     
    [SerializeField]
    private Map _map;

    [SerializeField]
    private PassengerViewFactory _passengerViewFactory;

    [SerializeField]
    private float _waitIntervalMax;
    [SerializeField]
    private float _waitIntervalMin;

    public event Action<IPassenger> GeneratePassengerEvent; 

    private void Start() {
        _rand = new System.Random();
        _resetTimer();

        GeneratePassengerEvent = null;
    }

    public void UpdateTimer() {
        _timer -= Time.deltaTime;
        if (_timer < 0) {
            _generatePassenger();
            _resetTimer();
        }
    }

    private void _generatePassenger() {
		var stations = new List<IStation>(); 
		_map.GetAllStations(stations);
        int startIdx = _rand.Next(stations.Count);
        IStation start = stations[startIdx];

        stations.Remove(start);
        int goalIdx = _rand.Next(stations.Count);
        IStation goal = stations[goalIdx];

        int lableIdx = _map.GetStationIndex(goal);
        IPassengerView passengerView = _passengerViewFactory.MakePassengerView(lableIdx);

        IPassenger newPassenger = new Passenger(start, goal, passengerView);
        if (GeneratePassengerEvent != null) { 
            GeneratePassengerEvent.Invoke(newPassenger);
        }
         
        newPassenger.WaitingAtStation(start);
    }

    private void _resetTimer() {
        _timer = UnityEngine.Random.Range(_waitIntervalMin, _waitIntervalMax);
        Debug.Log(_timer);
    }
}