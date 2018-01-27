using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class BusView :IBusView
{
    private GameObject _busPrefab;

    public BusView(GameObject prefab) {
        _busPrefab = prefab;
        _busPrefab.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    public IEnumerator InitAnimate(Transform stationTransform) {
        float duration = 0.5f;
        _busPrefab.transform.position = stationTransform.position;
        yield return new WaitForSeconds(duration);
        Debug.Log("arrive " + _busPrefab.transform.position);
    }

    public IEnumerator MoveToStationAnimate(Transform stationTransform) {
        float duration = 2;
        _busPrefab.transform.DOMove(stationTransform.position, duration).Play();
        yield return new WaitForSeconds(duration);
        Debug.Log("arrive " + _busPrefab.transform.position);
    }

    public void WaitAboardAnimate() {
    }
}