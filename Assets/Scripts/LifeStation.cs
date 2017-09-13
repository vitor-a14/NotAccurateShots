using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStation : MonoBehaviour {

	public Sprite gun;

	void Start () {
		GetComponent<SpriteRenderer> ().sprite = gun;
		Destroy (gameObject, 15);
	}

	void Update () {
		transform.localScale = new Vector3 ( 2 + Mathf.PingPong (Time.time, 1), 2 + Mathf.PingPong (Time.time, 1), 1);
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.tag == "Player") {
			coll.GetComponent<IndividualPlayer> ().life += 50;

			if (coll.GetComponent<IndividualPlayer> ().life > 100)
				coll.GetComponent<IndividualPlayer> ().life = 100;
			
			Destroy (gameObject);
		}
	}
}
