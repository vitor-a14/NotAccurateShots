using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void ArcadeMode () {
		int randomMap = Random.Range (1, 5);			
		SceneManager.LoadScene ("Map" + randomMap);
	}

	public void ZombieMode () {
		SceneManager.LoadScene ("SurvivalMap1");
	}

	public void ExitGame () {
		Application.Quit ();
	}
}
