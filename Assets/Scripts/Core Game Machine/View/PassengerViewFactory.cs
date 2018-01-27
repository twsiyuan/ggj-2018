using System;
using UnityEngine;

[Serializable]
public class PassengerViewFactory : MonoBehaviour
{
    [SerializeField]
	PassengerSettings settings = null;

    public IPassengerView MakePassengerView() 
	{
		GameObject prefab = UnityEngine.Object.Instantiate(this.settings.GetPrefabRandom());
		return new PassengerView(prefab);
    }
}