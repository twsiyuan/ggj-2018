﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Bus : IBus {

    private int _distance;
    private int _capacity;
    public int Distance { get { return _distance; } }

    private IStation _start;
    private IStation _goal;
    private IStation _current;
    private List<IStation> _path;
    public List<IStation> BusPath { get { return _path; } }

    private List<IPassenger> _passengers;

    public Bus(int distance, int capacity) {
        _distance = distance;
        _capacity = capacity;
        _passengers = new List<IPassenger>();
    }

    public void StartBusPath(List<IStation> path) {
        if (path.Count <= 0) {
            throw new Exception("wrong station path");
        }

        _path = path;
        _start = path[0];
        _goal = path[path.Count - 1];
         
    }

    public List<IPassenger> PassThroughStation(IStation station) { 
        if (station != _goal) {
            var arriveds = _busPassengersCheckStation(station);
            var aboards = _stationPassengersAboardBus(station);
            arriveds.AddRange(aboards);
            return arriveds;
        }
        else {
            var dictInfo = _busPassengersAllGetOff(station);
            return dictInfo;
        }
    }

    public void PassengerGetOff(IPassenger passenger) {
        _passengers.Remove(passenger);
    }

    private List<IPassenger> _busPassengersCheckStation(IStation station) {
        List<IPassenger> arriveds = new List<IPassenger>();
        for (int i = 0; i < _passengers.Count; i++) {
            IPassenger current = _passengers[i];
            bool isArrived = current.PassThroughNextStation(station, this); 
            if (isArrived) {
                arriveds.Add(current);
            }
        }
        return arriveds;
    }

    private List<IPassenger> _stationPassengersAboardBus(IStation station) {
		var waitingPassengers = new List<IPassenger> ();
		station.PickupPassengers(_capacity - _passengers.Count, waitingPassengers); 
        foreach (IPassenger passneger in waitingPassengers) {
            passneger.AboardBus(this);
            _passengers.Add(passneger);
        }
        return waitingPassengers;
    }

    private List<IPassenger> _busPassengersAllGetOff(IStation station) {
        List<IPassenger> getOffs = new List<IPassenger>();
        for (int i = 0; i < _passengers.Count; i++) {
            bool arrived = _passengers[i].GetOffFromBusAndArriveStation(station);
            getOffs.Add(_passengers[i]);
        }
        _passengers.Clear();
        return getOffs;
    }
}