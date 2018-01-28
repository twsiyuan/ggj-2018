using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PassengerManager : IPassengerManager {

    private List<IPassenger> _passengers;
    public int WaitingPeopleNumber {
        get {
            int n = 0;
            _passengers.ForEach((p) => { if (p.IsWaiting) n += 1; });
            return n;
        }
    }

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