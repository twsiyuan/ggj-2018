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

    public PassengerGenerator() {
        _rand = new System.Random();
        _waitInterval = 5;
    }

    public void Initialize(IMap map, IPassengerManager passengerMgr) {
        _map = map;
        _passengerMgr = passengerMgr;
    }

    public void UpdateTimer() {
        _timer -= Time.deltaTime;
        if (_timer < 0) {
            _generatePassenger();
            _resetTimer();
        }
    }

    private void _generatePassenger() {
        List<IStation> stations = _map.GetAllStations();
        int startIdx = _rand.Next(stations.Count);
        IStation start = stations[startIdx];
        stations.Remove(start);
        int goalIdx = _rand.Next(stations.Count);
        IStation goal = stations[goalIdx];

        _passengerMgr.AddPassenger( new Passenger(start, goal) );
    }

    private void _resetTimer() {
        _timer = _rand.Next(_waitInterval);
    }
}