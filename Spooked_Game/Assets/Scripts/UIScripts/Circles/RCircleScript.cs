using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCircleScript : MonoBehaviour {

	public bool[] spells = new bool[10];
	public GameObject[] Pictos = new GameObject[10];
	public GameObject Player;

	public int a = 0;
	private int[] checkArray = new int[10];
	private int[] tempSpells = new int[10];
	private int[] formatedSpells = new int[18];
	private GameObject[] tempObjects = new GameObject[18];

	void Start () {
		for (int i = 0; i < 10; i++) {
			spells[i] = false;
		}
	}
	
	void Update () {

		CheckSpell();

		if (CheckArray() > 0) {
			for (int i = 0; i < tempObjects.Length; i++) {
				Destroy(tempObjects[i]);
			}
			for (int i = 0; i < formatedSpells.Length; i++) {
				Vector3 vect = new Vector3(gameObject.transform.position.x + 5 * Mathf.Cos(0.349066f * i), gameObject.transform.position.y + 5 * Mathf.Sin(0.349066f * i));
				tempObjects[i] = Instantiate(Pictos[formatedSpells[i]], vect, Quaternion.identity);
				tempObjects[i].transform.SetParent(gameObject.transform);
				tempObjects[i].transform.position = new Vector3(tempObjects[i].transform.position.x, tempObjects[i].transform.position.y, -7);
			}
			a = 0;
		}

		FormatSpells();
	}

	int CheckArray() {
		for (int i = 0; i < tempSpells.Length; i++) {
			if (tempSpells[i] != checkArray[i]) {
				a++;
			}
		}
		for (int i = 0; i < tempSpells.Length; i++) {
			checkArray[i] = tempSpells[i];
		}
		return a;
	}

	void CheckSpell() {
		if (Player.GetComponent<ActionScript>().lightning > 0) {
			if (spells[0] != true) {
				spells[0] = true;
			}
		} else {
			if (spells[0] != false) {
				spells[0] = false;
			}
		}
		if (Player.GetComponent<ActionScript>().fireball > 0) {
			if (spells[1] != true) {
				spells[1] = true;
			}
		} else {
			if (spells[1] != false) {
				spells[1] = false;
			}
		}
	}

	void FormatSpells() {
		int a = 0;
		int count = 0;
		int left = 0;
		for (int i = 0; i < spells.Length; i++) {
			if (spells[i] != false) {
				tempSpells[i - a] = i;
			} else {
				a++;
			}
		}
		int[] temp = new int[spells.Length - a];
		for (int i = 0; i < temp.Length; i++) {
			temp[i] = tempSpells[i];
		}
		if (temp.Length > 0) {
			count = formatedSpells.Length / temp.Length;
			left = formatedSpells.Length % temp.Length;
		}
		for (int i = 0; i < count; i++) {
			for (int s = 0; s < temp.Length; s++) {
				formatedSpells[s + (temp.Length * i)] = temp[s];
			}
		}
		for (int i = 0; i < left; i++) {
			formatedSpells[formatedSpells.Length - 1 - left + i] = temp[i];
		}
	}
}
