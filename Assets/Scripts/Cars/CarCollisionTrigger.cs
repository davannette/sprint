using UnityEngine;
using System.Collections;

public class CarCollisionTrigger : MonoBehaviour {

	// next trigger in the lap
	public int damageMinor = 2;
	public int damageMajor = 5;

	// when an object enters this trigger
	void OnTriggerEnter2D(Collider2D other) {

		var player = transform.parent;
		HealthAndLives health = player.GetComponent<HealthAndLives>();
		if (other.gameObject.CompareTag("enemy")) {

			// deduct health based on front (ram) or rear (rammed) collision
			if (gameObject.CompareTag("front")) {
				health.OnBumpTrigger (damageMinor);
				// do damage to enemy:
				if (other.gameObject.GetComponent<AICar> ().Bump ()) {
					health.incrementKills ();
				}
			} else {
				health.OnBumpTrigger (damageMajor);
			}
		}
		// collision with edge of track:
		if (other.gameObject.CompareTag("track")) {
			health.OnBumpTrigger(damageMajor);
		}
	}
}
