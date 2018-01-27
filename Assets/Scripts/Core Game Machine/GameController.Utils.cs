using System.Collections.Generic;
using UnityEngine;

public partial class GameController
{	
	
    private void InitBusCenter()
	{
		var bus = new List<IBus>();
		for (int i = 0; i < 3; i++)
			bus.Add(new Bus(5, 5));

		busCenter.Init((enabled)=> mapInput.enabled = enabled, bus);
	}

    

}