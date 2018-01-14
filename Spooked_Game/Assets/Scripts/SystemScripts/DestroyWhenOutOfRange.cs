using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenOutOfRange : MonoBehaviour {

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Area") {
			Destroy(gameObject);                   //destroy after out of range
		}
	}
}
