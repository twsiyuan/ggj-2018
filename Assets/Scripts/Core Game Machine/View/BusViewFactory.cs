using System;
using UnityEngine;

[Serializable]
public class BusViewFactory : MonoBehaviour {

    [SerializeField]
    GameObject _Bus;

    public IBusView MakeBusView() {

        GameObject prefab = UnityEngine.Object.Instantiate(_Bus);
        return new BusView(prefab);
    }
}