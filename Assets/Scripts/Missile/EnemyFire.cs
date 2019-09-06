using UnityEngine;
using System.Collections;

public class EnemyFire : MonoBehaviour {

	// missile prefab
	public GameObject missile;

	// poll rate to check for viable target - configurable
	public int pollRate = 5;
	int _pollTime;

	void Start () {
		// no firing until cars get off the line
		_pollTime = 5;
	}
	
	void Update () {
		// check for other cars in front
		if ((int)Time.time == _pollTime) {
			_pollTime += pollRate;
			int layerMask = 1 << 8;
			RaycastHit2D hit = Physics2D.Raycast (transform.position + transform.right * 1.0f, transform.right, Mathf.Infinity, layerMask);

			if (hit != null && hit.transform != null) {
				Instantiate (missile, transform.position + transform.right * 0.5f, transform.rotation);
			}
		}
	}

	public void Reset() {
		// prevent infinite loop on start line
		_pollTime = (int)Time.time + 5;
	}
}
