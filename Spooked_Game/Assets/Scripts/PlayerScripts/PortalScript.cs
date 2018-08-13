using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour {

	public GameObject EffTeleportReverse;
	public GameObject Particles;
	public SpriteRenderer render;
	public GameObject Grass;
	public Sprite biom1;
	public Sprite biom2;
	private bool activated = false;
	private bool teleported = false;
	private GameObject Player;
	public GameObject TeleportAnim;
	public GameObject connectedPortal;
	public GameObject Button;
	public GameObject controller;
	public GameObject ActiveButton;
	public float durOfAnim = 0f;
	public int biomNum;

	void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update() {
		if (durOfAnim > 0) {
			durOfAnim -= Time.deltaTime;
		} else {
			if (activated) {
				if (biomNum == 1) {
					if (render.sprite != biom1) {
						Player.GetComponent<ActionScript>().CurrentBiom = 1;
						Grass.SetActive(false);
						Particles.SetActive(true);
						render.sprite = biom1;
					}
				}
				if (biomNum == 2) {
					if (render.sprite != biom2) {
						Player.GetComponent<ActionScript>().CurrentBiom = 2;
						Grass.SetActive(true);
						Particles.SetActive(false);
						render.sprite = biom2;
					}
				}
				if (biomNum == 3) {
					if (render.sprite != null) {
						Player.GetComponent<ActionScript>().CurrentBiom = 3;
						Grass.SetActive(false);
						Particles.SetActive(false);
						render.sprite = null;
					}
				}
				Player.transform.localPosition = connectedPortal.transform.position;
				GameObject temp = Instantiate(EffTeleportReverse, Player.transform.position, Quaternion.identity);
				if (Player.transform.localScale.x < 0) {
					temp.transform.localScale = new Vector3(temp.transform.localScale.x * -1, temp.transform.localScale.y, temp.transform.localScale.z);
				}
				durOfAnim = 1.66f;
				activated = false;
				teleported = true;
			}
			if (teleported && durOfAnim < 0) {
				Player.SetActive(true);
				controller.SetActive(true);
				teleported = false;
			}
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject == Player) {
			if (Button.activeSelf == false) {
				Button.SetActive(true);
			}

			if ((Input.GetKey(KeyCode.DownArrow) || Input.GetButton("Activate") || ActiveButton.GetComponent<ButtonScript>().activate) && !activated) {
				GameObject temp = Instantiate(TeleportAnim, Player.transform.position, Quaternion.identity);
				if (Player.transform.localScale.x < 0) {
					temp.transform.localScale = new Vector3(temp.transform.localScale.x * -1, temp.transform.localScale.y, temp.transform.localScale.z);
				}
				Player.SetActive(false);
				controller.SetActive(false);
				activated = true;
				ActiveButton.GetComponent<ButtonScript>().activate = false;
				durOfAnim = 1.4f;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject == Player) {
			if (Button.activeSelf == true) {
				Button.SetActive(false);
			}
		}
	}
}
