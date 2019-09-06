using UnityEngine;
using System.Collections;

public class MissileImpact : MonoBehaviour {

	// when missile hits something...
	void OnTriggerEnter2D(Collider2D other) {
		var player = GameObject.Find ("PlayerCar");
		HealthAndLives health = player.GetComponent<HealthAndLives> ();
		if (other.gameObject.CompareTag ("enemy")) {
			other.gameObject.GetComponent<AICar> ().Kill();
			if (GetComponent<FireBullet> ().playerShot)
				// got him!
				health.incrementKills ();
			Destroy (gameObject);
		} else if (other.gameObject.CompareTag("Player")) {
			// got me!
			health.Die();
			Destroy (gameObject);
		} else if (other.gameObject.CompareTag ("track")) {
			// missed...
			Destroy (gameObject);
		}
	}
}
