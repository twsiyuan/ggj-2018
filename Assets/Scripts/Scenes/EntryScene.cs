using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryScene : MonoBehaviour {

	[SerializeField]
	string nextScene;

	public void ToMenu(){
		SceneManager.LoadScene (this.nextScene, LoadSceneMode.Single);
	}

	public void ToCredit(){
		// TODO: To Credit
	}


}
