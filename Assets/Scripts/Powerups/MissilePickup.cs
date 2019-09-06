using UnityEngine;
using System.Collections;

public class MissilePickup : MonoBehaviour {

	// when missile hits something...
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("enemy")) {
			Destroy (gameObject);
		} else if (other.gameObject.CompareTag ("Player")) {
			other.gameObject.GetComponent<HumanCar>().PowerUp();
			Destroy (gameObject);
		}
	}
}
