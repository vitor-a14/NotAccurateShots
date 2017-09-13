using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWave : MonoBehaviour {

	public int wave; //Wave number
	public int enemysPerWave; //Enemys in the wave
	public float timeToNextWave; //Time to start the next wave
	public GameObject enemyInstance; //The gameobject of the waves
	public GameObject bossInstance;
	public Text waveText; //Text for show the informations of waves

	private List<GameObject> atualEnemys = new List<GameObject>(); //Enemys in the scene
	private GameObject[] spawnpoints; //Spawnpoints in the scene
	private float timeCount; //Clock to count the time
	private int specialWave = 3;

	void Start () {
		//Get the spawnpoints in scene
		spawnpoints = GameObject.FindGameObjectsWithTag ("SpawnEnemy"); 
	}

	void Update () {
		//If one enemy is missed, remove them from the list of enemys
		for (int i = 0; i < atualEnemys.Count; i++) {
			if (atualEnemys [i].gameObject == null) {
				atualEnemys.Remove (atualEnemys [i]);
			}
		}

		//If is 0 enemys in the scene, go to next wave
		if (atualEnemys.Count == 0) {
			timeCount += Time.deltaTime; //Clock count
			waveText.text = Mathf.Round(timeToNextWave - timeCount) + " to next"; //Show the time for next wave

			if (timeCount >= timeToNextWave) { //Time is ready
				wave++; //Next wave
				waveText.text = "Wave " + wave; //Wave number in screen
				enemysPerWave++; //Add number of enemys per wave
				SpawnEnemys (enemysPerWave); //Spawn enemys
				timeCount = 0; //Reset clock
			}
		}
	}

	//Spawn enemys function
	void SpawnEnemys (int count) {
		if (wave == specialWave) {
			GameObject boss = Instantiate (bossInstance, spawnpoints [0].transform.position, Quaternion.identity) as GameObject;
			atualEnemys.Add (boss);

			for (int i = 0; i < count; i++) {
				GameObject atualInstance = Instantiate (enemyInstance, spawnpoints [Random.Range (0, spawnpoints.Length)].transform.position, Quaternion.identity) as GameObject;
				atualInstance.GetComponent<DuckManager> ().jumpForce *= 3;
				atualEnemys.Add (atualInstance);
			}
			specialWave += 3;
		} else {
			for (int i = 0; i < count; i++) {
				GameObject atualInstance = Instantiate (enemyInstance, spawnpoints [Random.Range (0, spawnpoints.Length)].transform.position, Quaternion.identity) as GameObject;
				atualEnemys.Add (atualInstance);
			}
		}
	}
}
