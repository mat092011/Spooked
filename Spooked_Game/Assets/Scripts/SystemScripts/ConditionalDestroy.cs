using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalDestroy : MonoBehaviour {

	public float estimatedLifeTime;
	public bool onlyWhenOutOfRange;

	void Update(){
		if (estimatedLifeTime > 0) {
			estimatedLifeTime -= Time.deltaTime;
		}
		if (estimatedLifeTime <= 0 && !onlyWhenOutOfRange) {
			Destroy (gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Area") {
			Destroy(gameObject);                   //destroy after out of range
		}
	}
}
