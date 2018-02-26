using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToGame : MonoBehaviour {

	private GameObject Player;
	public GameObject Clock;
	public GameObject ToGamePoint;

	void Start () {
		Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject == Player) {
			Player.GetComponent<Movement>().worms = 0;
			Player.GetComponent<ActionScript>().lightning = 0;
			Player.transform.localPosition = ToGamePoint.transform.position;
			SpawnSystemScript.tutorial = false;
		}
	}
}
