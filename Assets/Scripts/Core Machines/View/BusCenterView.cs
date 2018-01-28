using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BusCenterView : MonoBehaviour
{ 
    [SerializeField]
    private BusViewFactory _busViewFactory;
     
    private List<GameObject> _busObjsShowing;
    private Dictionary<IBus, GameObject> _busObjDict;

    void Awake() { 
        _busObjsShowing = new List<GameObject>();
        _busObjDict = new Dictionary<IBus, GameObject>();
    }

    public void InsertNewBus(IBus bus) {
        GameObject busObj = _busViewFactory.MakeBusIconView(bus); 
        _busObjDict[bus] = busObj;
    }

    public void LaunchBusObj(IBus bus) {
        GameObject busObj = _busObjDict[bus];
        if (_busObjsShowing.Contains(busObj)) {
            _hideBusObject(busObj);
        }
    }

    public void RecycleBusObj(IBus bus) {
        GameObject busObj = _busObjDict[bus];
        if (!_busObjsShowing.Contains(busObj)) {
            _showBusObject(busObj, _busObjsShowing.Count);
        }
    }

    private void _hideBusObject(GameObject busObj) {
        _busObjsShowing.Remove(busObj);
        busObj.SetActive(false);
        for (int i = 0; i < _busObjsShowing.Count; i++) {
            _shiftBusObjectAnimate(_busObjsShowing[i], i);
        }
    }

    private void _showBusObject(GameObject busObj, int order) {
        _busObjsShowing.Add(busObj);
        busObj.SetActive(true);
        busObj.transform.position = new Vector3(-8.2f, _countPosYFormOrder(order+1), -10);
        busObj.transform.DOMoveY(_countPosYFormOrder(order), 1f).Play();
    }

    private void _shiftBusObjectAnimate(GameObject busObj, int order) {
        busObj.transform.DOMoveY(_countPosYFormOrder(order), 1f).Play();
    }

    private float _countPosYFormOrder(int order) {
        return 2f + order * -1.2f * 0.6f;
    }
}