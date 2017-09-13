using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour {

	private float shakeAmount = 0.05f;
	private float shake = 1.0f;
	private float decreaseFactor = 1.0f;

	void Start () {
		Destroy (gameObject, 0.5f);
	}

	void Update () {
		if (shake > 0) {
			Camera.main.transform.localPosition = new Vector3 (Camera.main.transform.position.x + Random.insideUnitCircle.x * shakeAmount,
				Camera.main.transform.position.y + Random.insideUnitCircle.x * shakeAmount, Camera.main.transform.position.z);
			shake -= Time.deltaTime * decreaseFactor;
		} else {
			shake = 0;
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.tag == "Player") {
			coll.GetComponent<IndividualPlayer> ().life -= 20;	
		} else if (coll.tag == "Enemy") {
			coll.GetComponent<DuckManager> ().life -= 20;	
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.transform.tag == "Player") {
			coll.transform.GetComponent<IndividualPlayer> ().life -= 20;	
		} else if (coll.transform.tag == "Enemy") {
			coll.transform.GetComponent<DuckManager> ().life -= 20;	
		}
	}

	void OnCollisionStay2D (Collision2D coll) {
		if (coll.transform.tag == "Player") {
			coll.transform.GetComponent<IndividualPlayer> ().life -= 20;	
		} else if (coll.transform.tag == "Enemy") {
			coll.transform.GetComponent<DuckManager> ().life -= 20;	
		}
	}
}
