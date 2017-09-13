using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationsManager : MonoBehaviour {

	public GameObject[] stations;
	public GameObject[] stationsInstance;

	private float timeToNext;

	void Start () {
		stations = GameObject.FindGameObjectsWithTag ("Station");
	}

	void Update () {
		timeToNext += Time.deltaTime;
		if (timeToNext >= 8) {
			Instantiate (stationsInstance [Random.Range (0, stationsInstance.Length)], stations [Random.Range (0, stations.Length)].transform.position, Quaternion.identity);
			timeToNext = 0;
		}
	}
}
