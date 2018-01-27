using System;
using UnityEngine;

[Serializable]
public class BusViewFactory : IBusViewFactory
{
    public IBusView MakeBusView() {

        GameObject prefab = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Bus") as GameObject);
        return new BusView(prefab);
    }
}