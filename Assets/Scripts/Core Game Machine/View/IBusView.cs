using System.Collections;
using UnityEngine;

public interface IBusView
{
    Transform Transform { get; }
	IEnumerator InitAnimate(Vector3 position);
	IEnumerator MoveToStationAnimate(IRoad road);
}