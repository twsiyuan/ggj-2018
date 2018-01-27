using System.Collections;
using UnityEngine;

public interface IBusView
{
    IEnumerator InitAnimate(Transform stationTransform);
    IEnumerator MoveToStationAnimate(Transform stationTransform);
    void WaitAboardAnimate();

}