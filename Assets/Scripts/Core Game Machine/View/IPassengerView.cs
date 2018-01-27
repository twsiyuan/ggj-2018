using System.Collections;
using UnityEngine;

public interface IPassengerView
{
    void ShowViewPositionAtStation(Transform stationTransform, int order);

    IEnumerator AboardBusAnimate(Transform stationTransform);
}