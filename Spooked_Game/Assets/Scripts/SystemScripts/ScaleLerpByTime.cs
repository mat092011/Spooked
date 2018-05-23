using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleLerpByTime : MonoBehaviour {

	public float SpeedScaler;
	public GameObject particle;

	public bool startLerp = false;
	public bool inst = false;
	private float dur = 0;

	void Update () {
		if (startLerp) {
			gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, new Vector3(0, 0, 0), SpeedScaler * Time.deltaTime);
			if (inst) {
				dur = 1.0f;
				Instantiate(particle, gameObject.transform.position, Quaternion.identity);
				inst = false;
			}
			if (dur > 0) {
				dur -= Time.deltaTime;
			} else {
				startLerp = false;
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			startLerp = true;
			inst = true;
		}
	}
}
