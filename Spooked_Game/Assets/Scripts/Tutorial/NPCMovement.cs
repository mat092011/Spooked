using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour {

	private float durOfMove = 0;
	private float speed = 1;
	private Vector3 vector = new Vector2(0, 0.5f);

	void Start () {
		
	}
	
	void Update () {
		if (durOfMove <= 0) {
			durOfMove = 0.5f;
			vector = -vector;
		} else {
			durOfMove -= Time.deltaTime;
			gameObject.transform.position = Vector2.MoveTowards(transform.position, transform.position + vector, speed * Time.deltaTime);
		}
	}
}
