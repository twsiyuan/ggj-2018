using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassengerGenerator : IPassengerGenerator
{
    private System.Random _rand;
    private float _timer;
    private readonly int _waitInterval;

    private GameController _gameCtrl;
    private IMap _map;
    private IPassengerManager _passengerMgr;
    private PassengerViewFactory _passengerViewFactory;

    public event Action<IPassenger> GeneratePassengerEvent;

    public PassengerGenerator(
        GameController gameCtrl, IMap map, IPassengerManager passengerMgr, PassengerViewFactory passengerViewFactory) {
        _gameCtrl = gameCtrl;
        _map = map;
        _passengerMgr = passengerMgr;
        _passengerViewFactory = passengerViewFactory;

        _rand = new System.Random();
        _waitInterval = 5;
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

        int lableIdx = _gameCtrl.GetStationIndex(goal);
        IPassengerView passengerView = _passengerViewFactory.MakePassengerView(lableIdx);

        IPassenger newPassenger = new Passenger(start, goal, passengerView, _gameCtrl);
        if (GeneratePassengerEvent != null) { 
            GeneratePassengerEvent.Invoke(newPassenger);
        }
         
        _passengerMgr.AddPassenger(newPassenger);
        newPassenger.WaitingAtStation(start);
    }

    private void _resetTimer() {
        _timer = Mathf.Max(3f, _rand.Next(_waitInterval) );
    }
}