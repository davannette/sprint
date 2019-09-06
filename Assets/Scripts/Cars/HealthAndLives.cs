using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthAndLives : MonoBehaviour {

	// public health prameters
	public int StartingLives = 3;
	public int StartingHealth = 100;

	// GUI links for health info
	public Text HealthText, LivesText, KillsText;

	// health counters
	int _currentHealth, _currentLives;
	int _kills = 0;

	// Use this for initialization
	void Start () {
		_currentLives = StartingLives;
		_currentHealth = StartingHealth;
		UpdateText();
	}

	// update lap counter text
	void UpdateText() {
		if (LivesText) {
			if (_currentLives == 1)
				LivesText.text = string.Format("<b><color=red>{0}</color></b>", _currentLives);
			else
				LivesText.text = string.Format("{0}", _currentLives);
		}
		if (HealthText) {
			if (_currentHealth < 10)
				HealthText.text = string.Format ("<b><color=red>{0}</color></b>", _currentHealth);
			else
				HealthText.text = string.Format ("{0}", _currentHealth);
		}
		if (KillsText) {
			KillsText.text = string.Format ("{0}", _kills);
		}
	}

	// when collision is detected
	public void OnBumpTrigger(int amount) {
		_currentHealth -= amount;
		if (_currentHealth <= 0) {
			_currentLives--;
			// reset car pos...
			if (_currentLives <= 0) {
				// game over
				GameLogic.instance.GameOver();
				return;
			}
			_currentHealth = StartingHealth;
			GameLogic.instance.NextLife ("CRASH!");
		}
		UpdateText ();
	}

	// hit by enemy fire
	public void Die() {
		_currentHealth = 0;
		_currentLives--;
		UpdateText ();
		// reset car pos...
		if (_currentLives <= 0) {
			// game over
			GameLogic.instance.GameOver();
			return;
		}
		_currentHealth = StartingHealth;
		GameLogic.instance.NextLife ("BOOM!");
		UpdateText ();
	}

	// successful missile kill
	public void incrementKills() {
		_kills++;
		UpdateText ();
	}

	// reset health on powerup
	public void ResetHealth() {
		_currentHealth = StartingHealth;
		UpdateText ();
	}

	// return no of kills
	public int getKills() {
		return _kills;
	}
}
