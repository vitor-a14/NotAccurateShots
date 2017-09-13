using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EviromentData : MonoBehaviour {

	public float life = 2;
	public bool explosive;
	public GameObject explosion;
	public Sprite destructTexture;

	public void DestructThis () {
		if (explosion) {
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag == "Explosion") {
			if (explosion) {
				Instantiate (explosion, transform.position, transform.rotation);
				Destroy (gameObject);
			} else {
				DestructThis ();
			}
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Rail") {
			if (explosion) {
				Instantiate (explosion, transform.position, transform.rotation);
				Destroy (gameObject);
			} else {
				DestructThis ();
			}
		}
	}

}
