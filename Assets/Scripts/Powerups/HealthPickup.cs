using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

	// Health powerup
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("enemy")) {
			// enemy stole it - restore health
			other.gameObject.GetComponent<AICar>().ResetHealth();
			Destroy (gameObject);
		} else if (other.gameObject.CompareTag ("Player")) {
			// player got it - restore health
			other.gameObject.GetComponent<HealthAndLives> ().ResetHealth ();
			Destroy (gameObject);
		}
	}
}
