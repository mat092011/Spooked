using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour {

	public GameObject star;
	public SpriteRenderer[] stars = new SpriteRenderer[4];	
	
	void Update () {
		if (Movement.fail) {
			if (!star.activeSelf) {
				star.SetActive(true);
			}
		} else {
			if (star.activeSelf) {
				star.SetActive(false);
			}
		}

		if (star.activeSelf) {
			if (InputDetection.scared > 1f) {
				for (int i = 0; i < stars.Length; i++) {
					stars[i].color = Color.red;
				}
			} else {
				for (int i = 0; i < stars.Length; i++) {
					stars[i].color = Color.white;
				}
			}
		}
	}
}
