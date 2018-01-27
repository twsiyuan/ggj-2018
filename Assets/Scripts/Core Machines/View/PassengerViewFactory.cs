using System;
using UnityEngine;

[Serializable]
public class PassengerViewFactory : MonoBehaviour
{
    [SerializeField]
	PassengerSettings settings;

    [SerializeField]
    PassengerLabelSettings labelSettings;

    public IPassengerView MakePassengerView(int dest) 
	{
		GameObject prefab = UnityEngine.Object.Instantiate(this.settings.GetPrefabRandom());

        Sprite label = UnityEngine.Object.Instantiate(this.labelSettings.GetSpriteOfIndex(dest));

        return new PassengerView(prefab, label);
    }
    
}