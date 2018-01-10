using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSpeedMov : MonoBehaviour {

	// Use this for initialization
	public float etar = 1;
	public Camera cam;

	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3 (cam.transform.position.x * etar, transform.position.y, transform.position.z);
		//transform.Translate (chars.speed * etar, 0, 0);
	}
}
