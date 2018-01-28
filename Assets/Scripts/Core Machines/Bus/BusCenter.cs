using System;
using System.Collections.Generic;
using UnityEngine;

public class BusCenter : MonoBehaviour
{	

    [SerializeField]
    private int busMax = 3;

    [SerializeField]
    private BusCenterView busCenterView;
    [SerializeField]
    private BusViewFactory busViewFactory;

    Queue<IBus> depot = new Queue<IBus>();

    Action<bool> enableSensorInput;

    Action<string> updateDepotUI;


    /// <summary>
    /// 初始化會導致 Sensor 被呼叫，應該在 MainLoop 可以開始遊戲時再呼叫
    /// </summary>
    public void Init(MapInput mapInput, ScoreBoard scoreBoard)
    {
        this.enableSensorInput = (enabled) => mapInput.enabled = enabled;
        
        this.updateDepotUI = (state) => scoreBoard.UpdateDepot(state);

        List<IBus> busQueue = InitBusQueue();
        foreach (var bus in busQueue)
            RecycleBus(bus);
    }

    private List<IBus> InitBusQueue()
	{
		var buses = new List<IBus>();

        for (int i = 0; i < busMax; i++) {
            int viewID = busViewFactory.GetRandomIndex();
            IBus newBus = new Bus(5, 5, viewID);
            buses.Add(newBus);
            busCenterView.InsertNewBus(newBus);
        }

        return buses;
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
        busCenterView.LaunchBusObj(bus); 

        if(depot.Count == 0)
            enableSensorInput(false);

        return bus;
    }

    public void RecycleBus(IBus bus)
    {
        depot.Enqueue(bus);

        updateDepotUI(GetBusState());
        busCenterView.RecycleBusObj(bus);

        if(depot.Count == 1)
            enableSensorInput(true);
    }

    private string GetBusState()
    {
        return string.Format("{0}/{1}", depot.Count, busMax);
    }
	
}