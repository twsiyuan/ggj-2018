using System;
using UnityEngine;

[Serializable]
public class BusView :IBusView
{
    private GameObject _busPrefab;

    public BusView(GameObject prefab) {
        _busPrefab = prefab;
    }

    public void InitAnimate() {
    }

    public void MoveToStationAnimate() {
    }

    public void WaitAboardAnimate() {
    }
}