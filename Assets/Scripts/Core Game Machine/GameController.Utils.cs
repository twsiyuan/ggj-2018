using System.Collections.Generic;
using UnityEngine;
using MarsCode113.ServiceFramework;

public partial class GameController
{	
	
	private void InitSubSystems()
	{
		InitBusCenter();

		InitMapInput();
	}
	
	
    private void InitBusCenter()
	{
		var bus = new List<IBus>();

		for (int i = 0; i < 3; i++)
			bus.Add(new Bus(5, 5));

		busCenter.Init(
			(enabled)=> mapInput.enabled = enabled,
			(state)=> scoreBoard.UpdateDepot(state),
			 bus
		);
	}

	private void InitMapInput()
	{
		mapInput.InitLinkTimesSyncCall(busCenter.GetNextBusDistance);
	}

	public int GetNextBusDistance()
	{
		return busCenter.GetNextBusDistance();
	}

	public void AddPassengerCompletion()
	{
		// scoreBoard.AddPassengerCompletion();
	}

	public void AddRage(int value)
	{
		// scoreBoard.AddRage(value);
	}

	public void AdjustRageMax(int value)
	{
		scoreBoard.AdjustRageMax(value);
	}

    public int GetStationIndex(IStation station) {
        return map.GetStationIndex(station);
    }
}