using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRenewScript : MonoBehaviour {

	private GameObject Player;
	//public GameObject Clock;
	private bool activated = false; 

	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update() {
		if (activated) {
			if (Player.GetComponent<ActionScript>().Clock.fillAmount < 0.995f) {
				Player.GetComponent<ActionScript>().Clock.fillAmount = Mathf.Lerp(Player.GetComponent<ActionScript>().Clock.fillAmount, 1.2f, 2 * Time.deltaTime);
			} else {
				activated = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject == Player) {
			gameObject.GetComponent<AudioSource>().Play();
			activated = true;
			//Clock.GetComponent<Clock> ().time = 300f;
		}
	}
}
