using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

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

    public IEnumerator AboardBusAnimate(Transform stationTransform) {
        float duration = 0.5f;
        _passengerPrefab.transform.DOMoveX(stationTransform.position.x, duration);
        yield return new WaitForSeconds(duration);
        _passengerPrefab.SetActive(false);
    }

    public IEnumerator GetOffBusToStationAnimation(Transform stationTransform, int order) {
        float duration = 0.5f;
        _passengerPrefab.transform.position = stationTransform.position;
        _passengerPrefab.SetActive(true);
        _passengerPrefab.transform.DOMoveX(stationTransform.position.x + order * 0.4f, duration);
        yield return new WaitForSeconds(duration);
    }
}