using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCircleScript : MonoBehaviour {

	public GameObject Camera;
	public bool[] spells = new bool[10];
	public GameObject[] Pictos = new GameObject[10];
	public GameObject Player;
	public GameObject ActiveHover;

	private Vector3 activePos = new Vector3(-0.5f, -6.38f);
	public int a = 0;
	private bool[] checkArray = new bool[10];
	private int[] tempSpells = new int[10];
	private int[] formatedSpells = new int[18];
	private GameObject[] tempObjects = new GameObject[18];
	private bool tween = true;
	public GameObject Hover;
	private int activePast;

	void Start() {
		for (int i = 0; i < 10; i++) {
			spells[i] = false;
		}
	}

	void Update() {

		ApplyTween();

		if (Check() > 0) {
			gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
			for (int i = 0; i < tempObjects.Length; i++) {
				Destroy(tempObjects[i]);
			}
			for (int i = 0; i < formatedSpells.Length; i++) {
				Vector3 vect = new Vector3(gameObject.transform.position.x - 5 * Mathf.Cos(0.35f * i), gameObject.transform.position.y - 5 * Mathf.Sin(0.35f * i));
				tempObjects[i] = Instantiate(Pictos[formatedSpells[i]], vect, Quaternion.identity);
				tempObjects[i].transform.SetParent(gameObject.transform);
				tempObjects[i].transform.position = new Vector3(tempObjects[i].transform.position.x, tempObjects[i].transform.position.y, -7);
			}
			Hover = Instantiate(ActiveHover, tempObjects[0].transform.position, Quaternion.identity);
			Hover.transform.SetParent(tempObjects[0].transform);
			a = 0;
		}
		if (tempObjects[0] != null && activePast != CheckActive()) {
			activePast = CheckActive();
			Destroy(Hover);
			InstantiateCheckActive();
			SendActive();
		}

		CheckSpell();

		ConditionDestroy();

		FormatSpells();

	}

	void SendActive() {
		if (tempObjects[CheckActive()].name == Pictos[0].name + "(Clone)") {
			if (!Player.GetComponent<ActionScript>().active[0]) {
				Player.GetComponent<ActionScript>().active[0] = true;
			} 
		} else {
			if (Player.GetComponent<ActionScript>().active[0]) {
				Player.GetComponent<ActionScript>().active[0] = false;
			}
		}
		if (tempObjects[CheckActive()].name == Pictos[1].name + "(Clone)") {
			if (!Player.GetComponent<ActionScript>().active[1]) {
				Player.GetComponent<ActionScript>().active[1] = true;
			} 
		} else {
			if (Player.GetComponent<ActionScript>().active[1]) {
				Player.GetComponent<ActionScript>().active[1] = false;
			}
		}
	}

	void ApplyTween() {
		for (int i = 0; i < 10; i++) {
			if (Camera.GetComponent<InputDetection>().hitCol[i] == Camera.GetComponent<InputDetection>().RCircle) {
				tween = false; break;
			} else {
				if (!tween) {
					tween = true;
				}
			}
		}

		if (tween) {
			Tween();
		}
	}

	void Tween() {
		float z = gameObject.transform.eulerAngles.z;
		if (z % 20 != 0) {
			float delta = z % 20;
			if (delta > 10) {
				gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, Mathf.LerpAngle(gameObject.transform.eulerAngles.z, gameObject.transform.eulerAngles.z + (20 - delta),6 * Time.deltaTime));
			} else {
				gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, Mathf.LerpAngle(gameObject.transform.eulerAngles.z, gameObject.transform.eulerAngles.z - delta,6 * Time.deltaTime));
			}
		}
	}

	void ConditionDestroy() {
		int b = 0;

		for (int i = 0; i < spells.Length; i++) {
			if (spells[i]) {
				b++;
			}
		}
		if (b == 0 && tempObjects[0] != null) {
			for (int i = 0; i < tempObjects.Length; i++) {
				Destroy(tempObjects[i]);
			}
		}
	}

	int Check() {
		for (int i = 0; i < spells.Length; i++) {
			if (spells[i] != checkArray[i]) {
				a++;
			}
		}
		for (int i = 0; i < tempSpells.Length; i++) {
			checkArray[i] = spells[i];
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

	void InstantiateCheckActive() {
		Hover.SetActive(true);
		Hover = Instantiate(ActiveHover, new Vector3(tempObjects[CheckActive()].transform.position.x, tempObjects[CheckActive()].transform.position.y, tempObjects[CheckActive()].transform.position.z + 0.5f), Quaternion.identity);
		Hover.transform.SetParent(tempObjects[CheckActive()].transform);
	}

	int CheckActive() {
		Vector3 distance = new Vector3();
		Vector3 minDistance = new Vector3(100, 100, 100);
		int closest = 0;
		for (int i = 0; i < tempObjects.Length; i++) {
			distance = tempObjects[i].transform.position - activePos;
			if (distance.x < minDistance.x) {
				minDistance = distance;
				closest = i;
			}
		}
		return closest;
	}
}
