using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryScene : MonoBehaviour {

	[SerializeField]
	string menuScene;

	[SerializeField]
	string creditScene;

	public void ToMenu(){
		SceneManager.LoadScene (this.menuScene, LoadSceneMode.Single);
	}

	public void ToCredit(){
		SceneManager.LoadScene (this.creditScene, LoadSceneMode.Single);
	}


}
