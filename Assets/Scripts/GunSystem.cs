using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour {

	public int damage; //Damage of the gun
	public bool explosion;
	public bool shootPermission; //Permission to shoot
	public GameObject shoot;
	public GameObject explosionInstance;

	void Update () {
		if (shootPermission) { //If can shoot
			RaycastHit2D hit = Physics2D.Raycast (transform.GetChild(0).transform.position, transform.right); //Instantiate the ray in gun to the mouse

			if (explosion) {
				//Instantiate explosion
				Instantiate (explosionInstance, hit.point, hit.transform.rotation);
			} else {
				if (hit.collider != null) {
					GameObject shootInstance = Instantiate (shoot, transform.position, Quaternion.identity) as GameObject;
					shootInstance.GetComponent<ShootEffect> ().startPos = transform.position;
					shootInstance.GetComponent<ShootEffect> ().endPos = hit.point;
					Destroy (shootInstance, 0.1f);
					if (hit.transform.tag == "Player") { //If collided with the player
						hit.transform.GetComponent<IndividualPlayer> ().life -= damage; //Hit the player 
						Vector3 dis = hit.transform.position - transform.position;
						hit.transform.GetComponent<Rigidbody2D> ().AddForce (dis * 110);
					} else if (hit.transform.tag == "Eviroment") {
						hit.transform.GetComponent<EviromentData> ().life -= 1;
						if (hit.transform.GetComponent<EviromentData> ().life <= 1 && hit.transform.GetComponent<EviromentData> ().life > 0) {
							hit.transform.GetComponent<SpriteRenderer> ().sprite = hit.transform.GetComponent<EviromentData> ().destructTexture;
						} else if (hit.transform.GetComponent<EviromentData> ().life <= 0) {
							hit.transform.GetComponent<EviromentData> ().DestructThis ();
						}
					} else if (hit.transform.tag == "Enemy") {
						Vector3 dis = hit.transform.position - transform.position;
						hit.transform.GetComponent<Rigidbody2D> ().AddForce (dis * 110);
						hit.transform.GetComponent<DuckManager> ().life -= damage; //Hit the player 
					}
				} else {
					GameObject shootInstance = Instantiate (shoot, transform.position, Quaternion.identity) as GameObject;
					shootInstance.GetComponent<ShootEffect> ().startPos = transform.position;
					shootInstance.GetComponent<ShootEffect> ().endPos = transform.right * 10;
					Destroy (shootInstance, 0.1f);
				}
			}
				
			shootPermission = false; //Desactive the shooting
		}
	}
}
