using System.Collections;
using UnityEngine;

public interface IPassengerView
{
    void ShowViewPositionAtStation(Transform stationTransform, int order);

    IEnumerator AboardBusAnimate(Transform stationTransform);

    IEnumerator GetOffBusToStationAnimation(Transform stationTransform, int order);

    void ArrivedStationAnimate(Transform busTransform, int order);
}