using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour {

	private GameObject Player;
	public GameObject connectedPortal;

	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update()
	{

	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject == Player) {
			if (Input.GetKey(KeyCode.DownArrow)) {
				Player.transform.localPosition = connectedPortal.transform.position;
			}
		}
	}
}
