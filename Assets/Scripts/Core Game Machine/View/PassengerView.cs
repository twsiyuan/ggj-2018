using System;
using UnityEngine;

[Serializable]
public class PassengerView : IPassengerView
{
    private GameObject _passengerPrefab;

    public PassengerView(GameObject prefab) {
        _passengerPrefab = prefab;
        _passengerPrefab.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
    }

    public void ShowViewPositionAtStation(Transform stationTransform, int order) {
        Vector3 pos = stationTransform.position;
        pos.x += order * 0.4f;
        _passengerPrefab.transform.position = pos;
    }
}