using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

[Serializable]
public class BusView : IBusView
{
    private GameObject _busPrefab;
    public Transform Transform { get { return _busPrefab.transform; } }

    public BusView(GameObject prefab) {
        _busPrefab = prefab;
        _busPrefab.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

	public IEnumerator InitAnimate(Vector3 position) {
        float duration = 0.5f;
        _busPrefab.transform.position = position;
        yield return new WaitForSeconds(duration);
        Debug.Log("arrive " + _busPrefab.transform.position);
    }

	public IEnumerator MoveToStationAnimate(IRoad road) {
        _setLeftRightSpriteDircection(road.GetPosition(1).x - road.GetPosition(0).x);
        float duration = road.GetTotalDistance() * 0.3f;
		var startTime = Time.time;
		var endTime = startTime + duration;

        while (Time.time < endTime){
			var t = Mathf.InverseLerp (startTime, endTime, Time.time);
			_busPrefab.transform.position = road.GetPosition (t);
			yield return null;
		}

		_busPrefab.transform.position = road.GetPosition (1); 
    }

    private void _setLeftRightSpriteDircection(float xDir) {
        float yAngle = xDir > 0 ? 180f : 0f;
        Quaternion angle = _busPrefab.transform.localRotation;
        angle.eulerAngles = new Vector3(0f, yAngle, 0f);
        _busPrefab.transform.localRotation = angle;
    }
}