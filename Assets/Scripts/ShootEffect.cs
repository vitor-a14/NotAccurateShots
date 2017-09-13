using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEffect : MonoBehaviour {

	public Vector3 startPos, endPos;

	void Update () {
		GetComponent<LineRenderer> ().SetPosition (0, startPos);
		GetComponent<LineRenderer> ().SetPosition (1, endPos);
	}
}
