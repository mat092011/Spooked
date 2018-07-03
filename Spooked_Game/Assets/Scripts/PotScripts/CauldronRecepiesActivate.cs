using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronRecepiesActivate : MonoBehaviour {

	private GameObject Player;
	private GameObject Camera;
	public int type = 0;
	public int cauldronType = 0;

	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		Camera = GameObject.FindGameObjectWithTag("MainCamera");
	}


	void Update() {
		for (int i = 0; i < 10; i++) {
			if (Camera.GetComponent<InputDetection>().hitCol[i] == gameObject.GetComponent<Collider2D>()) {
				if (cauldronType == 1) {
					if (type == 1) {
						Player.GetComponent<ActionScript>().lightning += 10;
						Player.GetComponent<Movement>().worms -= 2;
						gameObject.SetActive(false);
					}
					if (type == 2) {
						Player.GetComponent<ActionScript>().fireball += 10;
						Player.GetComponent<Movement>().frogs -= 1;
						Player.GetComponent<Movement>().bees -= 1;
						gameObject.SetActive(false);
					}
					if (type == 3) {
						Player.GetComponent<ActionScript>().stun += 10;
						Player.GetComponent<Movement>().carterpilars -= 2;
						gameObject.SetActive(false);
					}
					if (type == 4) {
						Player.GetComponent<ActionScript>().fireball += 10;
						Player.GetComponent<Movement>().carterpilars -= 1;
						Player.GetComponent<Movement>().dragonflies -= 1;
						gameObject.SetActive(false);
					}
					if (type == 5) {
						Player.GetComponent<ActionScript>().changeling += 10;
						Player.GetComponent<Movement>().frogs -= 2;
						gameObject.SetActive(false);
					}
					if (type == 6) {
						Player.GetComponent<ActionScript>().revealSpell += 10;
						Player.GetComponent<Movement>().frogs -= 1;
						Player.GetComponent<Movement>().dragonflies -= 1;
						gameObject.SetActive(false);
					}
				}
			}
		}
	}
}
