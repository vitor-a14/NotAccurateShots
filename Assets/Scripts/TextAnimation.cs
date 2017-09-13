using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour {

	private bool sizeValue = true;
	public float minValue = 1;
	public float maxValue = 1.6f;
	public float speed = 3;

	void Update () {
		if (transform.localScale.x >= maxValue - 0.2f) {
			sizeValue = false;
		} else if (transform.localScale.x <= minValue + 0.2f) {
			sizeValue = true;
		}

		if (sizeValue) {
			transform.localScale = Vector2.Lerp (transform.localScale, new Vector2 (maxValue, maxValue), speed * Time.deltaTime);
		} else {
			transform.localScale = Vector2.Lerp (transform.localScale, new Vector2 (minValue, minValue), speed * Time.deltaTime);
		}
	}
}
