using System.Collections.Generic;
using UnityEngine;
using MarsCode113.ServiceFramework;

public partial class GameController
{	
	
	private void InitSubSystems()
	{
        busCenter.Init(mapInput, scoreBoard);

		InitMapInput();
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