using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class PassengerView : IPassengerView
{
    private GameObject _passengerPrefab;
    private GameObject _facePrefab;

    private readonly float _interval = 0.5f;
    private readonly float _duration = 0.5f;
    private readonly float _fadeDuration = 1f;

    public PassengerView(GameObject passenger, GameObject face) {
        _passengerPrefab = passenger;
        _passengerPrefab.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        _facePrefab = face;
        _facePrefab.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    public void ShowViewPositionAtStation(Transform stationTransform, int order) {
        Vector3 pos = stationTransform.position;
        pos.x += order * _interval;
        _passengerPrefab.transform.position = pos;
    }
    public void RearrangePositionAnimateAtStation(Transform stationTransform, int order) {
        Vector3 pos = stationTransform.position;
        pos.x += order * _interval;
        _passengerPrefab.transform.DOMoveX(pos.x, _duration).Play();
    }

    public IEnumerator AboardBusAnimate(Transform stationTransform) { 
        _passengerPrefab.transform.DOMoveX(stationTransform.position.x, _duration).Play();
        yield return new WaitForSeconds(_duration);
        _passengerPrefab.SetActive(false);
    }

    public IEnumerator GetOffBusToStationAnimation(Transform stationTransform, int order) { 
        _passengerPrefab.transform.position = stationTransform.position;
        _passengerPrefab.SetActive(true);
        _passengerPrefab.transform.DOMoveX(stationTransform.position.x + order * _interval, _duration).Play();
        yield return new WaitForSeconds(_duration);
    }
    
    public void ArrivedStationAnimate(Transform busTransform, int order, int total) { 
        Vector3 pos = busTransform.position;
        pos.x += (order - total/2) * _interval;
        pos.y -= _interval;
        _passengerPrefab.transform.position = pos;
        _passengerPrefab.SetActive(true);
        _passengerPrefab.GetComponent<SpriteRenderer>().sortingOrder = 3;
        _passengerPrefab.GetComponent<SpriteRenderer>().DOFade(0.0f, _fadeDuration).Play().OnComplete(() => {
            _passengerPrefab.SetActive(false);
        });
    }
}