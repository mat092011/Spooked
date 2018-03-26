using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTriggerScript : MonoBehaviour {

	private GameObject Player;

	void Start() {
		Player = GameObject.Find("Player");
	}

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Ground") {
			if (Player.activeSelf) {
				Player.GetComponent<Movement>().grounded = true;
			}
        }
    }

    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.tag == "Ground") {
			if (Player.activeSelf) {
				Player.GetComponent<Movement>().grounded = true;
			}
		}
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Ground") {
			if (Player.activeSelf) {
				Player.GetComponent<Movement>().grounded = false;
			}
		}
    }
}
