using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CarLapCounter : MonoBehaviour {

	// finishline trigger target
	public TrackLapTrigger first;

	// GUI location for lap counter
	public Text lapText;

	// next trigger
	TrackLapTrigger next;
	
	// current lap
	int _lap;

	void Start () {
		// initialise vars
		_lap = 1;
		SetNextTrigger(first);
		// update GUI
		UpdateText();
	}

	// update lap counter text
	void UpdateText() {
		if (lapText) {
			lapText.text = string.Format("{0}", _lap);		
		}
	}

	// lap trigger is entered
	public void OnLapTrigger(TrackLapTrigger trigger) {
		// only update if triggered in turn (prevent "back-over")
		if (trigger == next) {
			if (first == next) {
				_lap++;
				UpdateText();
			}
			SetNextTrigger(next);
		}
	}

	// action guidance trigger
	void SetNextTrigger(TrackLapTrigger trigger) {
		next = trigger.next;
		SendMessage("OnNextTrigger", next, SendMessageOptions.DontRequireReceiver);
	}

	// reset triggers on respawn
	public void Reset() {
		SetNextTrigger(first);
	}

	// return completed laps
	public int getLaps() {
		return _lap - 1; // don't count current incomplete lap
	}
}
