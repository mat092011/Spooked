using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRenewScript : MonoBehaviour {

	private GameObject Player;
	public GameObject Clock;

	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject == Player) {
			gameObject.GetComponent<AudioSource>().Play();
			Clock.GetComponent<Clock> ().time = 300f;
		}
	}
}
