using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronRecepiesActivate : MonoBehaviour {

	private GameObject Player;
	private GameObject Camera;
	public int type = 0;

	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
		Camera = GameObject.FindGameObjectWithTag("MainCamera");
	}


	void Update() {
		for (int i = 0; i < 10; i++) {
			if (Camera.GetComponent<InputDetection>().hitCol[i] == gameObject.GetComponent<Collider2D>()) {
				if (type == 1) {
					Debug.Log("works");
					Player.GetComponent<ActionScript>().lightning += 10;
					Player.GetComponent<Movement>().worms -= 1;
					gameObject.SetActive(false);
				}
				if (type == 2) {
					Player.GetComponent<ActionScript>().fireball += 10;
					Player.GetComponent<Movement>().grasshoppers -= 1;
					gameObject.SetActive(false);
				}
			}
		}
	}
}
