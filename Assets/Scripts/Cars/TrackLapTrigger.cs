using UnityEngine;
using System.Collections;

public class TrackLapTrigger : MonoBehaviour {

	// next trigger in the lap
	public TrackLapTrigger next;

	// trigger the lap-counter of the associated car 
	void OnTriggerEnter2D(Collider2D other) {
		CarLapCounter carLapCounter = other.gameObject.GetComponent<CarLapCounter>();
		if (carLapCounter) {
			carLapCounter.OnLapTrigger(this);
		}
	}
}
