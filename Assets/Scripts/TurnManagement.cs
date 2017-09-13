using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnManagement : MonoBehaviour {

	public int maxPlayers;
	public List<GameObject> players = new List<GameObject>(); //List of players in match
	public List<GameObject> playersInstance = new List<GameObject>(); 
	public List<GameObject> spawnpoints = new List<GameObject>(); 

	public bool pause = false;
	public GameObject selectedPlayerInstance;
	public int atualPlayer; //The atual player of match
	public Text playerText;
	public Text HPtext;
	public Text winTitle;

	private float timeToNext; //Time to next player turn
	private float timeToEnd; //Time to next round
	private bool next; //Autorize to go to next player
	private Vector3 newScale;
	private GameObject selectedPlayer;

	void Start () {
		spawnpoints.AddRange (GameObject.FindGameObjectsWithTag ("Spawnpoint"));

		for (int i = 0; i < maxPlayers; i++) {
			int randomPlayer = Random.Range (0, playersInstance.Count);
			int randomSpawnpoint = Random.Range (0, spawnpoints.Count);

			Instantiate (playersInstance [randomPlayer], spawnpoints [randomSpawnpoint].transform.position, Quaternion.identity);

			playersInstance.Remove (playersInstance [randomPlayer]);
			spawnpoints.Remove (spawnpoints [randomSpawnpoint]);
		}

		selectedPlayer = Instantiate (selectedPlayerInstance, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		players.AddRange (GameObject.FindGameObjectsWithTag ("Player"));
		GetComponent<CameraSystem> ().target = players [0]; //Set the start player position

		for (int i = 0; i < players.Count; i++) {
			players [i].GetComponent<IndividualPlayer> ().playerName = " Player " + (i + 1);
		}
	}

	void Update () {
		//Use the function NextPlayer if is autorized
		if (next) {
			NextPlayer (1); 
		}

		if (players.Count <= 1) {
			EndRound ();
		}

		float offset = Mathf.PingPong (Time.time / 2, 0.1f);
		selectedPlayer.transform.position = new Vector3 (players [atualPlayer].transform.position.x, players [atualPlayer].transform.position.y + 0.20f + offset, players [atualPlayer].transform.position.z);

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !next) {
			if (Input.GetTouch(0).position.x > Screen.width / 2) { 
				players [atualPlayer].transform.rotation = Quaternion.Euler (0, 0, 0); //Touch is in right or in left and change the rotation

				//Change Arm Position//
				newScale = players [atualPlayer].transform.GetChild (0).transform.localScale;
				newScale.y = 1;
				players [atualPlayer].transform.GetChild (0).transform.localScale = newScale;
			} else { //Same stuff, just for left
				players [atualPlayer].transform.rotation = Quaternion.Euler (0, 180, 0); 

				//Change Arm Position//
				newScale = players [atualPlayer].transform.GetChild (0).transform.localScale;
				newScale.y = -1;
				players [atualPlayer].transform.GetChild (0).transform.localScale = newScale;
			}

			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position); //Get mouse position in 3D dimension
			mousePos.y *= 1.4f;
			players [atualPlayer].GetComponent<Rigidbody2D>().AddForce((players [atualPlayer].transform.position - mousePos) * 110); //Apply recoil in player
				
			Vector3 dir = Camera.main.ScreenToWorldPoint (Input.GetTouch(0).position) - players [atualPlayer].transform.GetChild (0).transform.position; 
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			players [atualPlayer].transform.GetChild (0).transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward); //rotation the atual player hand to touch
			players [atualPlayer].transform.GetChild (0).GetChild (0).GetChild (0).GetComponent<GunSystem>().shootPermission = true; //Active permission to shoot
			next = true; //Autorize the next player
		}
	}

	//Function the pause the game
	public void PauseGame () {
		pause = !pause;
		if (pause) {
			Time.timeScale = 0;
			winTitle.color = Color.white;
			winTitle.text = " Paused";
		} else {
			winTitle.text = " ";
			Time.timeScale = 1;
		}
	}

	//End the round function
	void EndRound () {
		timeToEnd += Time.deltaTime;

		if (players.Count != 0) {
			winTitle.color = players [0].GetComponent<IndividualPlayer> ().playerColor;
			winTitle.text = players [0].GetComponent<IndividualPlayer> ().playerName + " Wins!";
			players [0].GetComponent<IndividualPlayer> ().life = 100;
		} else {
			winTitle.color = Color.red;
			winTitle.text = " No one wins!";
		}

		if (timeToEnd >= 2) {
			int randomMap = Random.Range (1, 5);			
			SceneManager.LoadScene ("Map" + randomMap);
		}
	}

	//Programation to move camera to next player
	public void NextPlayer (float endTime) { 
		timeToNext += Time.deltaTime;

		if (timeToNext >= endTime) {
			if (atualPlayer < players.Count - 1) {
				atualPlayer++;
				GetComponent<CameraSystem> ().target = players [atualPlayer];

				HPtext.text = " HP " + players [atualPlayer].GetComponent<IndividualPlayer> ().life.ToString();
				playerText.color = players [atualPlayer].GetComponent<IndividualPlayer> ().playerColor;
				HPtext.color = players [atualPlayer].GetComponent<IndividualPlayer> ().playerColor;
				playerText.text = players [atualPlayer].GetComponent<IndividualPlayer> ().playerName;
			} else {
				atualPlayer = 0;
				GetComponent<CameraSystem> ().target = players [atualPlayer];

				HPtext.text = " HP " + players [atualPlayer].GetComponent<IndividualPlayer> ().life.ToString();
				playerText.color = players [atualPlayer].GetComponent<IndividualPlayer> ().playerColor;
				HPtext.color = players [atualPlayer].GetComponent<IndividualPlayer> ().playerColor;
				playerText.text = players [atualPlayer].GetComponent<IndividualPlayer> ().playerName;
			}

			timeToNext = 0;
			next = false;
		}
	}
}
