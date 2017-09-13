using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombActivation : MonoBehaviour {

	public GameObject explosion;

	void OnTriggerEnter2D (Collider2D coll) {
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}

}
