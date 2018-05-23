using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMovement : MonoBehaviour {

	private float timeCounter = 0;

	public float rad = 3.6f;
	public float speed;
	public float width;
	public float height;
	private Vector3 start;

	// Use this for initialization
	void Start () {
		start = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		timeCounter += Time.deltaTime * speed;

		float x = Mathf.Cos(timeCounter * width) * rad;
		float y = Mathf.Sin(timeCounter * height) * rad;

		gameObject.transform.position = start + new Vector3(x, y);
	}
}
