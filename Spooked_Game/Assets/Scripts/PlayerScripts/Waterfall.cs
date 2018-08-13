using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour {

	public GameObject Drop;
	public GameObject LPos;
	public GameObject RPos;
	public GameObject YPos;
	public int biom;
	private float durBetDrop;
	private GameObject Player;

	void Start() {
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () {
		if (Player.GetComponent<ActionScript>().CurrentBiom == 1) {
			if (durBetDrop > 0) {
				durBetDrop -= Time.deltaTime;
			} else {
				durBetDrop = Random.Range(0.0f, 1.4f);
				GameObject DropPlace = new GameObject();
				DropPlace.transform.position = new Vector2(Random.Range(LPos.transform.position.x, RPos.transform.position.x), LPos.transform.position.y);
				GameObject DropTemp = Instantiate(Drop, DropPlace.transform.position, Quaternion.identity);
				DropTemp.transform.position = new Vector3(DropTemp.transform.position.x, DropTemp.transform.position.y, Random.Range(5f, 20f));
				Destroy(DropPlace);
			}
		}
	}
}
