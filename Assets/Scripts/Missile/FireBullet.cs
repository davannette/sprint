using UnityEngine;
using System.Collections;

public class FireBullet : MonoBehaviour {

	// public parameters
	public float speed = 12.0f;
	public bool playerShot = false;

	void Start () {
		// fire missile in facing direction
		GetComponent<Rigidbody2D> ().velocity = transform.right * speed;
	}
}
