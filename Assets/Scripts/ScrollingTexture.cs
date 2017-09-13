using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTexture : MonoBehaviour {

	public float velocity;
	private Vector2 offset;

	void Update () {
		offset.x = Time.time * velocity;
		GetComponent<Renderer> ().sharedMaterial.mainTextureOffset = offset;
	}
}
