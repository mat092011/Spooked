using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRenewScript : MonoBehaviour {

	private GameObject Player;
	//public GameObject Clock;
	private bool activated = false;
	private bool animat;
	private Animator anim;
	private float dur = 0f;

	void Start () {
		anim = gameObject.GetComponent<Animator>();
		Player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update() {
		anim.SetBool("Activated", animat);
		if (dur > 0) {
			dur -= Time.deltaTime;
		} else {
			if (animat) {
				animat = false;
			}
		}
		if (activated) {
			if (Player.GetComponent<ActionScript>().Clock.fillAmount < 0.995f) {
				Player.GetComponent<ActionScript>().Clock.fillAmount = Mathf.Lerp(Player.GetComponent<ActionScript>().Clock.fillAmount, 1.2f, 2 * Time.deltaTime);
			} else {
				activated = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject == Player) {
			gameObject.GetComponent<AudioSource>().Play();
			activated = true;
			animat = true;
			dur = 1f;
		}
	}
}
