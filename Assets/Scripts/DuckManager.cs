using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckManager : MonoBehaviour {

	public int life; //Life of duck
	public int attack; //Attack value

	public float velocity; //Speed of movement
	public float jumpForce; //The force of the jump
	public float timeToMove; //Cooldown of the movement
	public float coolDown; //Cooldown of the attack

	private float timeCount; //Time clock to move 
	private float timeCountAttack; //Time clock to attack

	void Update () {
		//Turn around the duck if position is more than 6
		if (transform.position.x > 6) {
			transform.rotation = Quaternion.Euler (transform.rotation.x, 180, transform.rotation.z);
		} else if (transform.position.x < -6) { //Turn around the duck if position is more than -6
			transform.rotation = Quaternion.Euler (transform.rotation.x, 0, transform.rotation.z);
		}

		//Destroy duck if fall of the map
		if (transform.position.y <= -5) {
			Destroy (gameObject);
		}

		//Destroy duck if life is 0
		if (life <= 0) {
			Destroy (gameObject);
		}

		//Add time to clock
		timeCount += Time.deltaTime;

		//If clock is equals or more than movement cooldown, and random value is equal 2, can move
		if (timeCount >= timeToMove && Random.Range(1,3) == 2) {
			GetComponent<Rigidbody2D> ().AddForce (transform.up * jumpForce, ForceMode2D.Force); //Jump
			GetComponent<Rigidbody2D> ().AddForce (transform.right * velocity, ForceMode2D.Force); //Go aread
			timeCount = 0; //Reset clock
		}
	}
		
	//If collide with player, hit them
	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			coll.gameObject.GetComponent<IndividualPlayer> ().life -= attack;
		} 
	}
		
	void OnCollisionStay2D (Collision2D coll) {
		//If stay together of eviroment stuffs, damage them
		if (coll.gameObject.tag == "Eviroment") {
			if (coll.transform.GetComponent<EviromentData> ().life <= 1 && coll.transform.GetComponent<EviromentData> ().life > 0) {
				coll.transform.GetComponent<SpriteRenderer> ().sprite = coll.transform.GetComponent<EviromentData> ().destructTexture;
			} else if (coll.transform.GetComponent<EviromentData> ().life <= 0) {
				coll.transform.GetComponent<EviromentData> ().DestructThis ();
			}
			coll.gameObject.GetComponent<EviromentData> ().life -= 0.3f * Time.deltaTime;
		}else if (coll.gameObject.tag == "Player") { //If stay together of player, damage with cooldown clock
			timeCountAttack += Time.deltaTime;
			if (timeCountAttack >= coolDown) {
				coll.gameObject.GetComponent<IndividualPlayer> ().life -= attack;
				timeCountAttack = 0;
			}
		} 
	}
}
