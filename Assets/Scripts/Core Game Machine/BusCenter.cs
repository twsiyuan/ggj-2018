using System;
using System.Collections.Generic;
using UnityEngine;

public class BusCenter : MonoBehaviour
{	

    Queue<IBus> depot;

    Action<bool> enableSensorInput;


    private void Init(Action<bool> enableSensorInput)
    {
        this.enableSensorInput = enableSensorInput;
    }
    

    public int GetNextBusDistance()
    {
        return depot.Peek().Distance;
    }


    public IBus LaunchBus(List<IStation> paths)
    {
        var bus = depot.Dequeue();

        bus.StartBusPath(paths);

        if(depot.Count == 0)
            enableSensorInput(false);

        return bus;
    }


    public void RecycleBus(IBus bus)
    {
        depot.Enqueue(bus);

        enableSensorInput(true);
    }
	
}