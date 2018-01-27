using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStation : MonoBehaviour 
{
	[SerializeField]
	bool mainStation = false;

	public bool IsMainStation {
		get{
			return this.mainStation;
		}
	}
}
