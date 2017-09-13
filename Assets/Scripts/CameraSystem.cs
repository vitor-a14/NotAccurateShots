using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {

	public GameObject target;
	public float movementVelocity;

	void Update () {
		Vector3 targetPos = new Vector3(target.transform.position.x, target.transform.position.y, -10); //Set the distance correctly
		transform.position = Vector3.Lerp (transform.position, targetPos, movementVelocity * Time.deltaTime); //Set the camera in atual player
	}
}
