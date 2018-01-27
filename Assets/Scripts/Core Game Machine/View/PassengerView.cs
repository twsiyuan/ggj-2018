using System;
using UnityEngine;

[Serializable]
public class PassengerView : IPassengerView
{
    private GameObject _passengerPrefab;

    public PassengerView() {
        _passengerPrefab = UnityEngine.Object.Instantiate(Resources.Load("Passenger") as GameObject);
    }
}