using System;
using UnityEngine;

[CreateAssetMenu]
public class PassengerSettings : ScriptableObject
{
    [SerializeField]
	Data[] settings = new Data[0];

	[Serializable]
	class Data{
		[SerializeField]
		public GameObject prefab;

		[SerializeField]
		public int probabilityWeight;
	}

	public GameObject GetPrefabRandom(){
		if (this.settings.Length <= 0) {
			return null;
		}

		var t = 0;
		foreach (var s in this.settings) {
			t += s.probabilityWeight;
		}

		var r = UnityEngine.Random.Range (0f, 1f);
		foreach (var s in this.settings) {
			var p = (float)s.probabilityWeight / t;
			if (r < p) {
				return s.prefab;
			} else {
				r -= p;
			}
		}

		return this.settings [0].prefab;
	}
}