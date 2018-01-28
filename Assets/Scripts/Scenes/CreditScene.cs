using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScene : MonoBehaviour {

	[SerializeField]
	string nextScene;

	public void ToNext(){
		SceneManager.LoadScene (this.nextScene, LoadSceneMode.Single);
	}

	void Update(){
		if (Input.GetKey (KeyCode.Escape)) {
			this.ToNext ();
		}
	}


}
