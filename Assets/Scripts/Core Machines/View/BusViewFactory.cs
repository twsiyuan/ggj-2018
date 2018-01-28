using System;
using UnityEngine;

[Serializable]
public class BusViewFactory : MonoBehaviour {

    [SerializeField]
    private BusSetting _allBus; 

    public IBusView MakeBusView(IBus bus) {
         
        GameObject prefab = UnityEngine.Object.Instantiate(_allBus.GetGameObjectOfIndex(bus.ViewID));
        return new BusView(prefab);
    }

    public GameObject MakeBusIconView(IBus bus) {
        GameObject obj = UnityEngine.Object.Instantiate(_allBus.GetGameObjectOfIndex(bus.ViewID));
        Quaternion q = obj.transform.localRotation;
        q.eulerAngles = new Vector3(0, 180, 0f);
        obj.transform.localRotation = q;
        obj.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        return obj;
    }

    public int GetRandomIndex() {
        return _allBus.GetRandomIndex();
    }
}