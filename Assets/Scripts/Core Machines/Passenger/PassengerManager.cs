using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassengerManager : IPassengerManager {

    private List<IPassenger> _passengers; 

    public PassengerManager() {
        _passengers = new List<IPassenger>();
    }

    public void AddPassenger(IPassenger newPassenger) {
        _passengers.Add(newPassenger);
    }

    public void UpdateTimer() {
        for (int i = 0; i < _passengers.Count; i++) {
            _passengers[i].UpdateRage();
        }
    }
}