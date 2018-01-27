using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseRoad : IRoad {

	IRoad road;
	public ReverseRoad(IRoad r){
		this.road = r;
	}

	public Vector3 GetPosition(float t)
	{
		return this.road.GetPosition (1- t);
	}

}
