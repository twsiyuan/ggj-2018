using System;
using UnityEngine;

[Serializable]
public class PassengerViewFactory : IPassengerViewFactory
{
    public IPassengerView MakePassengerView() {
         
        GameObject prefab = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Passenger1") as GameObject);
        return new PassengerView(prefab);
    }
}