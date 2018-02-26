using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public float durOfMove;
	private float durOfMoveT = 0;
	public float speed;
	public Vector3 vectorOfMovement;

	void Update () {
		if (durOfMoveT <= 0) {
			durOfMoveT = durOfMove;
			vectorOfMovement = -vectorOfMovement;
		} else {
			durOfMoveT -= Time.deltaTime;
			gameObject.transform.position = Vector3.MoveTowards(transform.position, transform.position + vectorOfMovement, speed * Time.deltaTime);
		}
	}
}
