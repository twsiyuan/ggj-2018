using UnityEngine;

[CreateAssetMenu]
public class PassengerLabelSettings : ScriptableObject
{
    [SerializeField]
    Sprite[] settings = new Sprite[0];

	public Sprite GetSpriteOfIndex(int index){
		if (this.settings.Length <= 0 || 
            index < 0 || index >= this.settings.Length) {
			return null;
		}
        
		return this.settings[index];
	}
}