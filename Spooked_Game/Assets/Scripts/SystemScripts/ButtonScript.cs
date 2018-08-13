using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour {

	public GameObject Player;
	public int num;
	public Sprite sprite;
	public Sprite nSprite;
	public SpriteRenderer rend;
	public bool temp = true;
	public GameObject Camera;
	public bool activate = false;

	void Update () {
		for (int i = 0; i < 10; i++) {
			temp = true;
			if (Camera.GetComponent<InputDetection>().hitCol[i] == gameObject.GetComponent<Collider2D>()) {
				if (rend.sprite != nSprite) {
					rend.sprite = nSprite;
				}
				if (num == 1) {
					if (Player.GetComponent<Movement>().left != true) {
						Player.GetComponent<Movement>().left = true;
					}
				}
				if (num == 2) {
					if (Player.GetComponent<Movement>().right != true) {
						Player.GetComponent<Movement>().right = true;
					}
				}
				if (num == 3) {
					if (Player.GetComponent<ActionScript>().fire != true) {
						Player.GetComponent<ActionScript>().fire = true;
					}
				}
				if (num == 4) {
					if (Jump.jump != true) {
						Jump.jump = true;
					}
				}
				if (num == 5) {
					activate = true;
				}
				temp = false;
				break;
			}
		}
		if (temp == true) {
			if (rend.sprite != sprite) {
				rend.sprite = sprite;
			}
			if (num == 1) {
				if (Player.GetComponent<Movement>().left != false) {
					Player.GetComponent<Movement>().left = false;
				}
			}
			if (num == 2) {
				if (Player.GetComponent<Movement>().right != false) {
					Player.GetComponent<Movement>().right = false;
				}
			}
			if (num == 3) {
				if (Player.GetComponent<ActionScript>().fire != false) {
					Player.GetComponent<ActionScript>().fire = false;
				}
			}
			if (num == 4) {
				if (Jump.jump != false) {
					Jump.jump = false;
				}
			}
		}
	}
}
