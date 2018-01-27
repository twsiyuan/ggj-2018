using System;
using System.Collections.Generic;
using UnityEngine;

public class BusCenter : MonoBehaviour
{	

    [SerializeField]
    private int busMax;
    
    Queue<IBus> depot = new Queue<IBus>();

    Action<bool> enableSensorInput;

    Action<string> updateDepotUI;


    /// <summary>
    /// 初始化會導致 Sensor 被呼叫，應該在 MainLoop 可以開始遊戲時再呼叫
    /// </summary>
    public void Init(Action<bool> enableSensorInput, List<IBus> busQueue)
    {
        this.enableSensorInput = enableSensorInput;

        foreach (var bus in busQueue)
            RecycleBus(bus);

        busMax = busQueue.Count;
    }

    public int GetNextBusDistance()
    {
        return depot.Peek().Distance;
    }

    public IBus LaunchBus(List<IStation> paths)
    {
        var bus = depot.Dequeue();

        bus.StartBusPath(paths);

        updateDepotUI(GetBusState());

        if(depot.Count == 0)
            enableSensorInput(false);

        return bus;
    }

    public void RecycleBus(IBus bus)
    {
        depot.Enqueue(bus);

        updateDepotUI(GetBusState());

        if(depot.Count == 1)
            enableSensorInput(true);
    }

    private string GetBusState()
    {
        return string.Format("{0}/{1}", depot.Count, busMax);
    }
	
}