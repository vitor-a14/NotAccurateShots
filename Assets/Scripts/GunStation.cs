using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStation : MonoBehaviour {

	public Sprite gun;
	public GameObject gunInstance;

	void Start () {
		GetComponent<SpriteRenderer> ().sprite = gun;
		Destroy (gameObject, 14);
	}

	void Update () {
		transform.localScale = new Vector3 ( 2 + Mathf.PingPong (Time.time, 1), 2 + Mathf.PingPong (Time.time, 1), 1);
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.tag == "Player") {
			Destroy (coll.transform.GetChild (0).transform.GetChild (0).transform.GetChild (0).gameObject);

			Vector3 spawnPoint = new Vector3 (coll.transform.GetChild (0).transform.GetChild (0).transform.position.x + coll.transform.GetChild (0).transform.GetChild (0).transform.right.x *  0.09f, 
				coll.transform.GetChild (0).transform.GetChild (0).transform.position.y,
				coll.transform.GetChild (0).transform.GetChild (0).transform.position.z);
			
			Instantiate (gunInstance, spawnPoint, 
				coll.transform.GetChild (0).transform.GetChild (0).transform.rotation, 
				coll.transform.GetChild (0).transform.GetChild (0).transform);

			Destroy (gameObject);
		}
	}
}
