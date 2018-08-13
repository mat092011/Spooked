using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public float durOfMove;
	private float durOfMoveT = 0;
	private bool isAbleToMove = false;
	public float speed;
	public Vector3 vectorOfMovement;

	void Update () {
		if (isAbleToMove) {
			if (durOfMoveT <= 0) {
				durOfMoveT = durOfMove;
				vectorOfMovement = -vectorOfMovement;
			} else {
				durOfMoveT -= Time.deltaTime;
				gameObject.transform.position = Vector3.MoveTowards(transform.position, transform.position + vectorOfMovement, speed * Time.deltaTime);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			GameObject.FindGameObjectWithTag("Player").transform.SetParent(gameObject.transform);
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			GameObject.FindGameObjectWithTag("Player").transform.SetParent(null);
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Area") {
			if (!isAbleToMove) {
				isAbleToMove = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.tag == "Area") {
			if (isAbleToMove) {
				isAbleToMove = false;
			}
		}
	}
}
