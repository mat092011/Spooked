using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronTriggerEnter : MonoBehaviour {

	private bool spawned = false;
	private GameObject temp;
	private Vector3 Scale = new Vector3(0.6097561f, 0.6097561f, 1);

	public GameObject Circle;
	public GameObject SpawnPoint;
	public GameObject deathZone;

	public GameObject[] Pictos = new GameObject[6];
	private bool[] canSpawn = new bool[6];
	public GameObject[] instantiated = new GameObject[6];

	public GameObject Player;

	void Start () {
		deathZone.SetActive(false);
		for (int i = 0; i < canSpawn.Length; i++) {
			canSpawn[i] = false;
		}
		for (int i = 0; i < Pictos.Length; i++) {
			instantiated[i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {

		CheckSpawn();

		if (temp != null) {
			if (spawned) {
				temp.transform.localScale = Vector3.Lerp(temp.transform.localScale, Scale, 2 * Time.deltaTime);
				for (int i = 0; i < instantiated.Length; i++) {
					if (canSpawn[i]) {
						instantiated[i].SetActive(true);
					}
				}
				deathZone.SetActive(true);
				temp.transform.Find("Star").gameObject.SetActive(true);
				temp.transform.Find("CauldronLeaves").gameObject.SetActive(true);
			}
			if (!spawned) {
				temp.transform.localScale = Vector3.Lerp(temp.transform.localScale, new Vector3(0, 0, 1), 6 * Time.deltaTime);
				for (int i = 0; i < instantiated.Length; i++) {
					instantiated[i].SetActive(false);
				}
				deathZone.SetActive(false);
				temp.transform.Find("Star").gameObject.SetActive(false);
				temp.transform.Find("CauldronLeaves").gameObject.SetActive(false);
				if (temp.transform.localScale == new Vector3(0, 0, 1)) {
					Destroy(temp);
				}
			}
		}
	}

	void CheckSpawn() {
		if (Player.GetComponent<Movement>().worms > 0) {
			if (!canSpawn[0]) {
				canSpawn[0] = true;
			}
		} else {
			if (canSpawn[0]) {
				canSpawn[0] = false;
			}
		}
		if (Player.GetComponent<Movement>().grasshoppers > 0) {
			if (!canSpawn[1]) {
				canSpawn[1] = true;
			}
		} else {
			if (canSpawn[1]) {
				canSpawn[1] = false;
			}
		}
	}

	void OnCollisionStay2D(Collision2D collision) {
		GameObject Player = GameObject.FindGameObjectWithTag("Player");
		if (collision.gameObject == Player && !spawned && Player.GetComponent<Movement>().jumpAllowed) {
			if (temp == null) {
				temp = Instantiate(Circle, SpawnPoint.transform.position, Quaternion.identity);
				temp.transform.SetParent(gameObject.transform);
				spawned = true;
				Cotel.triggered = true;
			}
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject == GameObject.FindGameObjectWithTag("Player") && spawned) {
			spawned = false;
			Cotel.triggered = false;
		}
	}
}
