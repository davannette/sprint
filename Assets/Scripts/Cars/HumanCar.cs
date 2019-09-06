using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HumanCar : MonoBehaviour {

	// public car parameters
	public float acceleration = 0.3f;
	public float braking = 0.3f;
	public float steering = 4.0f;
	public float topspeed = 1.0f;
	public int missiles = 3;
	public float fireRate = 0.5f;

	// GUI label for missile count
	public Text MissileLabel;

	// missile prefab
	public GameObject missile;

	// local params
	bool _inactive = false;
	int _missileCount;
	Vector2 touchOrigin = -Vector2.one;

	void Start() {
		// set missile count and refresh label
		_missileCount = missiles;
		MissileLabel.text = string.Format ("{0}", _missileCount);
	}

	void FixedUpdate() {
		if (_inactive) {
			// make sure car is not moving
			GetComponent<Rigidbody2D> ().velocity = new Vector3 (0.0f, 0.0f, 0.0f);
			GetComponent<Rigidbody2D> ().angularVelocity = 0;
			return;
		}

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		
		// steering
		float rot = transform.localEulerAngles.z - horizontal * steering;
		transform.localEulerAngles = new Vector3(0.0f, 0.0f, rot);

		// acceleration/braking
		float velocity = GetComponent<Rigidbody2D>().velocity.magnitude;
		// float y = Input.GetAxis ("Vertical");
		if (vertical > 0.01f) {
			velocity += vertical * acceleration;
			if (velocity > topspeed)
				velocity = topspeed;
		} else if (vertical < -0.01f) {
			velocity += vertical * braking;
		}

		// apply car movement
		GetComponent<Rigidbody2D>().velocity = transform.right * velocity;
		GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
	}

	float _nextFire = 0.0f;
	void Update() {
		// fire missile on ctrl key
		if (_missileCount > 0 && Time.time > _nextFire && Input.GetButton ("Fire1")) {
			_missileCount--;
			MissileLabel.text = string.Format ("{0}", _missileCount);
			_nextFire = Time.time + fireRate;
			GameObject msl = (GameObject)Instantiate (missile, transform.position + transform.right * 0.444f, transform.rotation);
			msl.GetComponent<FireBullet>().playerShot = true;
		}
	}

	// move car back to start
	public void ResetCar(GameObject spawnPosition) {
		transform.position = spawnPosition.transform.position;
		transform.rotation = spawnPosition.transform.rotation;
		SendMessage ("Reset");
	}

	// missile powerup
	public void PowerUp() {
		_missileCount += 3;
		MissileLabel.text = string.Format ("{0}", _missileCount);
	}

	// stop moving after crash
	public void deactivate() {
		_inactive = true;
		_missileCount = missiles;
		MissileLabel.text = string.Format ("{0}", _missileCount);
	}

	// restart car
	public void activate() {
		_inactive = false;
	}

}
