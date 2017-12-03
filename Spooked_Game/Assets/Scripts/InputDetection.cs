using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDetection : MonoBehaviour {

	private Vector2[] TouchPosition = new Vector2[10];

	public Collider2D ButtonLeft;
	public Collider2D ButtonRight;
	public Collider2D JumpButton;
	public Collider2D Fire;

	public GameObject Player;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			for (int i = 0; i < Input.touchCount; i++) {
				if (Input.GetTouch(i).phase == TouchPhase.Began) {
					TouchPosition[i] = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
					RaycastHit2D hit = Physics2D.Raycast(TouchPosition[i], new Vector2(TouchPosition[i].x + 0.01f, TouchPosition[i].y + 0.01f));
					if (hit.collider == ButtonLeft) {
						if (Player.GetComponent<Movement>().left != true) {
							Player.GetComponent<Movement>().left = true;
						}
					}
					if (hit.collider == ButtonRight) {
						if (Player.GetComponent<Movement>().right != true) {
							Player.GetComponent<Movement>().right = true;
						}
					}
					if (hit.collider == Fire) {
						if (Player.GetComponent<ActionScript>().fire != true) {
							Player.GetComponent<ActionScript>().fire = true;
						}
					}
					if (hit.collider == JumpButton) {
						if (Jump.jump != true) {
							Jump.jump = true;
						}
					}
				}
				if (Input.GetTouch(i).phase == TouchPhase.Ended) {
					if (Player.GetComponent<Movement>().left != false) {
						Player.GetComponent<Movement>().left = false;
					}
					if (Player.GetComponent<Movement>().right != false) {
						Player.GetComponent<Movement>().right = false;
					}
					if (Player.GetComponent<ActionScript>().fire != false) {
						Player.GetComponent<ActionScript>().fire = false;
					}
					if (Jump.jump != false) {
						Jump.jump = false;
					}
				}
			}
		}
	}
}
