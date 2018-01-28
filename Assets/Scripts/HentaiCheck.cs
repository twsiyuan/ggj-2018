using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HentaiCheck : MonoBehaviour 
{
	[SerializeField]
	AnimateManager animateManager;


	bool forceOpen = false;

	void Reset(){
		animateManager = FindObjectOfType<AnimateManager> ();
	}

	void Awake(){
		animateManager.BusArrived += OnArrived;
		animateManager.BusLeaved += OnLeaved;
	}

	void OnLeaved (object sender, AnimateManager.BusEventArgs e)
	{
		var shake = e.BusView.Transform.GetComponent<SecretShake> ();
		if (shake != null) {
			IPassenger x;
			var tags = e.Bus.GetPassengers ().Select (v => v.View.Transform.gameObject.tag);
			var maleCount = tags.Where (v => v == "Male").Count ();
			var femaleCount = tags.Where (v => v == "Female").Count ();
			shake.enabled = (femaleCount == 1 && maleCount >= 2) || forceOpen;
		}
	}

	void OnArrived (object sender, AnimateManager.BusEventArgs e)
	{
		var shake = e.BusView.Transform.GetComponent<SecretShake> ();
		if (shake != null) {
			shake.enabled = false;
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.H)) {
			this.forceOpen = !this.forceOpen;
		}
	}
}
