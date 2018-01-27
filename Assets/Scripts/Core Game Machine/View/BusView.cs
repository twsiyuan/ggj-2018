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

	public IEnumerator InitAnimate(Vector3 position) {
        float duration = 0.5f;
        _busPrefab.transform.position = position;
        yield return new WaitForSeconds(duration);
        Debug.Log("arrive " + _busPrefab.transform.position);
    }

	public IEnumerator MoveToStationAnimate(IRoad road) {
        float duration = 2;
		var startTime = Time.time;
		var endTime = startTime + duration;
		while (Time.time < endTime){
			var t = Mathf.InverseLerp (startTime, endTime, Time.time);
			_busPrefab.transform.position = road.GetPosition (t);
			yield return null;
		}
		_busPrefab.transform.position = road.GetPosition (1);
        Debug.Log("arrive " + _busPrefab.transform.position);
    }

    public void WaitAboardAnimate() {
    }
}