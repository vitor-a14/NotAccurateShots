using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualPlayer : MonoBehaviour {

	public int life; //Life of player
	public Color playerColor; //The text color of player
	public string playerName; //Player name
	public bool railDestruction; //If can destroy player  colliding with the rail

	void Start () {
		int num = Random.Range (1, 6); //Random number

		//Set random player color
		switch (num) { 
		case 1:
			playerColor = Color.blue;
			break;
		case 2:
			playerColor = Color.red;
			break;
		case 3:
			playerColor = Color.green;
			break;
		case 4: 
			playerColor = Color.cyan;
			break;
		case 5:
			playerColor = Color.magenta;
			break;
		}
	}

	void Update () {
		//Player dies if life is equals 0
		if (life <= 0) {
			PlayerDeath ();
		}

		//If player falls of the map, her dies
		if (transform.position.y <= -5) {
			Camera.main.GetComponent<TurnManagement>().players.Remove (gameObject);
			Camera.main.GetComponent<TurnManagement> ().NextPlayer (0);
			Destroy (this.gameObject);
		}
	}

	//Player death function
	void PlayerDeath () {
		Camera.main.GetComponent<TurnManagement>().players.Remove (gameObject);
		Camera.main.GetComponent<TurnManagement> ().NextPlayer (0);
		Destroy (gameObject);
	}

	//If collide with rail, destroy them (permission of bool necessary)
	void OnCollisionStay2D (Collision2D coll) {
		if (coll.gameObject.tag == "Rail" && railDestruction) {
			life = 0;
			transform.Translate (-transform.right * 15 * Time.deltaTime);
		}
	}
}
