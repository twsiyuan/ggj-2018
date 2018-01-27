using System.Collections;
using UnityEngine;

public interface IBusView
{
	IEnumerator InitAnimate(Vector3 position);
	IEnumerator MoveToStationAnimate(IRoad road);

}