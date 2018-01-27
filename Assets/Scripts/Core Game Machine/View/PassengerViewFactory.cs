using System;
using UnityEngine;

[Serializable]
public class PassengerViewFactory : MonoBehaviour
{
    [SerializeField]
    GameObject _passenger1;

    public IPassengerView MakePassengerView() {
         
        GameObject prefab = UnityEngine.Object.Instantiate(_passenger1);
        return new PassengerView(prefab);
    }
}