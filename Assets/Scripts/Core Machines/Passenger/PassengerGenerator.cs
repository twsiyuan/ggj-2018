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
    [SerializeField]
    private float _maxWaitingPeople;

    [SerializeField]
    private StartEndOption[] _options;
    [Serializable]
    private class StartEndOption {
        [SerializeField]
        public int Start;
        [SerializeField]
        public int End;
        [SerializeField]
        public int Weight;
    }

    private IPassengerManager _passengerManager;

    public event Action<IPassenger> GeneratePassengerEvent; 

    private void Start() {
        _rand = new System.Random();
        _resetTimer(); 
    }

    public void InsertPassengerManager(IPassengerManager passengerManager) {
        _passengerManager = passengerManager;
    }

    public void UpdateTimer() {
        _timer -= Time.deltaTime;
        if (_timer < 0 && _passengerManager.WaitingPeopleNumber < _maxWaitingPeople ) {
            _generatePassenger();
            _resetTimer();
        }
    }

    private void _generatePassenger() {
        IPassenger newPassenger;
        if (_options.Length == 0) {
            newPassenger = _genertateRandomPassenger();
        }
        else {
            newPassenger = _generateOptionPassenger();
        }
    }

    private IPassenger _genertateRandomPassenger() { 
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

        return newPassenger;
    }

    private IPassenger _generateOptionPassenger() {
		var stations = new List<IStation>(); 
		_map.GetAllStations(stations);

        StartEndOption option = _getRandomOption();

        int startIdx = option.Start;
        IStation start = stations[startIdx];
         
        int goalIdx = option.End;
        IStation goal = stations[goalIdx];

        int lableIdx = _map.GetStationIndex(goal);
        IPassengerView passengerView = _passengerViewFactory.MakePassengerView(lableIdx);

        IPassenger newPassenger = new Passenger(start, goal, passengerView);
         
        if (GeneratePassengerEvent != null) { 
            GeneratePassengerEvent.Invoke(newPassenger);
        }

        newPassenger.WaitingAtStation(start);

        return newPassenger;
    }

    private StartEndOption _getRandomOption() {
		var t = 0;  
        foreach (var s in _options) {
			t += s.Weight;
		} 

		var r = UnityEngine.Random.Range (0f, 1f);
		foreach (var s in _options) {
			var p = (float)s.Weight / t;
			if (r < p) {
				return s;
			} else {
				r -= p;
			}
		}

		return _options[0];
    }

    private void _resetTimer() {
        _timer = UnityEngine.Random.Range(_waitIntervalMin, _waitIntervalMax); 
    }
}