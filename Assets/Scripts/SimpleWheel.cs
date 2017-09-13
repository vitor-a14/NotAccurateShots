using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWheel : MonoBehaviour {

	void Update () {
		transform.Rotate (0, 0, -700 * Time.deltaTime);
	}
}
