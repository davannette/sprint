using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameLogic : MonoBehaviour {

	public static GameLogic instance;

	// public GameObject parameters
	public GameObject spawnPosition;
	public GameObject positionObjects;
	public GameObject healthPU;
	public GameObject missilePU;

	// powerup placement timing
	public int powerupTiming = 30;

	// GUI elements
	public Text TimerText;
	public Text Status;

	// Player components
	HumanCar playerCar;
	HealthAndLives carHealth;
	CarLapCounter carLaps;

	void Awake() {
		// set class instance var
		instance = this;
	}

	// powerup timing vars
	int _nextHealthPU;
	int _nextMissilePU;

	void Start () {
		// set Player vars
		var car = GameObject.Find ("PlayerCar");
		playerCar = car.GetComponent<HumanCar>();
		carHealth = car.GetComponent<HealthAndLives>();
		carLaps = car.GetComponent<CarLapCounter>();
		// set powerup timing vars
		_nextHealthPU = powerupTiming / 2;
		_nextMissilePU = powerupTiming;
		// freeze game until started from start menu
		Time.timeScale = 0;
		GetComponent<StartMenu> ().Open ();
	}

	// display "Ready, set, go" then start game
	IEnumerator intro(float time) {
		Time.timeScale = 0;
		Status.text = "Ready ...";
		yield return new WaitForSecondsRealtime(time);
		Status.text = "... Set ...";
		yield return new WaitForSecondsRealtime(time);
		Status.text = "GO!!!";
		Time.timeScale = 1.0f;
		yield return new WaitForSecondsRealtime(time);
		Status.text = "";
	}

	void Update() {
		// display elapsed time
		int minutes = (int)Time.fixedTime / 60;
		int seconds = (int)Time.fixedTime % 60;
		TimerText.text = string.Format("{0}:{1:D2}", minutes, seconds);

		// spawn a health powerup every 60 seconds:
		if ((int)Time.time == _nextHealthPU) {
			_nextHealthPU += powerupTiming;
			// randomise spawn location
			Transform[] ts = positionObjects.GetComponentsInChildren<Transform> ();
			int randIndex = (int)Random.Range (0, ts.Length - 1);
			Vector3 randVect = new Vector3 (Random.Range (0f, 1f), Random.Range (0f, 1f), 0);
			Instantiate (healthPU, ts [randIndex].position + randVect, Quaternion.identity);
		}

		// spawn a missile powerup every 60 seconds (offset by 30 seconds):
		if ((int)Time.time == _nextMissilePU) {
			_nextMissilePU += powerupTiming;
			// randomise spawn location
			Transform[] ts = positionObjects.GetComponentsInChildren<Transform> ();
			int randIndex = (int)Random.Range (0, ts.Length - 1);
			Vector3 randVect = new Vector3 (Random.Range (0f, 1f), Random.Range (0f, 1f), 0);
			Instantiate (missilePU, ts [randIndex].position + randVect, Quaternion.identity);
		}

	}

	public void NextLife(string msg) {
		StartCoroutine (restartIntro (1, msg));
	}

	// start new game
	public void Go() {
		StartCoroutine(intro (2));
	}

	// display "Ready, set, go" then continue game
	IEnumerator restartIntro(float time, string message) {
		Time.timeScale = 0;
		Status.text = message;
		yield return new WaitForSecondsRealtime(time);
		Time.timeScale = 1.0f;
		playerCar.deactivate ();
		playerCar.ResetCar (spawnPosition);
		Status.text = "Ready ...";
		yield return new WaitForSecondsRealtime(time);
		Status.text = "... Set ...";
		yield return new WaitForSecondsRealtime(time);
		Status.text = "GO!!!";
		playerCar.activate ();
		yield return new WaitForSecondsRealtime(time);
		Status.text = "";
	}

	// game over, display Game Menu popup with score
	public void GameOver() {
		Time.timeScale = 0;
		// lap bonus
		int score = carLaps.getLaps() * 100;
		// kill bonus
		score += carHealth.getKills() * 50;
		// time bonus
		score += (carLaps.getLaps () * 14 - (int)Time.time) * 10;
		// display popup
		GetComponent<GameMenu> ().Open(score);
	}
}
