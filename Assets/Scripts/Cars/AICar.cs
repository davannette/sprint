using UnityEngine;
using System.Collections;

public class AICar : MonoBehaviour {

	// public car parameters
	public float acceleration = 0.3f;
	public float braking = 0.3f;
	public float steering = 4.0f;
	public float topspeed = 5.0f;

	// health parameters
	public int maxhealth = 20;
	public int bumpdamage = 5;

	// location to respawn after kill
	public GameObject spawnPoint;

	// car health
	int _health;

	// direction to drive
	Vector3 target;

	void Start() {
		// set health to default
		_health = maxhealth;
	}

	// subtract damage from health
	public bool Bump() {
		_health -= 5;
		if (_health <= 0) {
			Respawn ();
			return true;
		}
		return false;
	}

	// kill/respawn AI car
	public void Kill() {
		Respawn();
	}

	// reset health after powerup
	public void ResetHealth() {
		_health = maxhealth;
	}

	// select next target
	public void OnNextTrigger(TrackLapTrigger next) {
		target = Vector3.Lerp(next.transform.position - next.transform.right, 
		                      next.transform.position + next.transform.right, Random.value);
	}

	// steer car towards target
	void SteerTowardsTarget ()
	{
		Vector2 towardNextTrigger = target - transform.position;
		float targetRot = Vector2.Angle (Vector2.right, towardNextTrigger);
		if (towardNextTrigger.y < 0.0f) {
			targetRot = -targetRot;
		}
		float rot = Mathf.MoveTowardsAngle (transform.localEulerAngles.z, targetRot, steering);
		transform.eulerAngles = new Vector3 (0.0f, 0.0f, rot);
	}

	void FixedUpdate() {

		SteerTowardsTarget();

		// always accelerate
		float velocity = GetComponent<Rigidbody2D>().velocity.magnitude;
		velocity += acceleration;

		// limit top speed
		if (velocity > topspeed)
			velocity = topspeed;

		// apply car movement
		GetComponent<Rigidbody2D>().velocity = transform.right * velocity;
		GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
	}

	// respawn car from start point, and reset parameters
	void Respawn() {
		_health = maxhealth;
		gameObject.SetActive (false);
		transform.position = spawnPoint.transform.position;
		transform.rotation = spawnPoint.transform.rotation;
		GetComponent<EnemyFire>().Reset();
		gameObject.SetActive (true);
		GetComponent<CarLapCounter> ().Reset ();
	}
}
