using UnityEngine;

[CreateAssetMenu]
public class BusSetting : ScriptableObject
{
    [SerializeField]
    GameObject[] settings = new GameObject[0];

	public GameObject GetGameObjectOfIndex(int index){
		if (this.settings.Length <= 0 || 
            index < 0 || index >= this.settings.Length) {
			return null;
		}
        
		return this.settings[index];
	}

    public int GetRandomIndex() {
        return UnityEngine.Random.Range(0, this.settings.Length);
    }
}