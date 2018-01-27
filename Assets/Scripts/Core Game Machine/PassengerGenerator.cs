using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassengerGenerator : IPassengerGenerator
{
    private System.Random _rand;
    private float _timer;
    private readonly int _waitInterval; 

    private IMap _map;
    private IPassengerManager _passengerMgr;
    private PassengerViewFactory _passengerViewFactory;

    public PassengerGenerator(
        IMap map, IPassengerManager passengerMgr, PassengerViewFactory passengerViewFactory) {
        _map = map;
        _passengerMgr = passengerMgr;
        _passengerViewFactory = passengerViewFactory;

        _rand = new System.Random();
        _waitInterval = 5;
        _resetTimer();
    } 

    public void UpdateTimer() {
        _timer -= Time.deltaTime;
        if (_timer < 0) {
            _generatePassenger();
            _resetTimer();
        }
    }

    private void _generatePassenger() {
		// TODO: Buffer
		var stations = new List<IStation>();
		_map.GetAllStations(stations);
        int startIdx = _rand.Next(stations.Count);
        IStation start = stations[startIdx];
        stations.Remove(start);
        int goalIdx = _rand.Next(stations.Count);
        IStation goal = stations[goalIdx];

        Debug.Log("passenger at " + startIdx + " goto "+ goalIdx);
        IPassengerView passengerView = _passengerViewFactory.MakePassengerView();
        _passengerMgr.AddPassenger( new Passenger(start, goal, passengerView) );
    }

    private void _resetTimer() {
        _timer = _rand.Next(_waitInterval);
    }
}