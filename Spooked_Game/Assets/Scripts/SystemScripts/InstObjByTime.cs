using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstObjByTime : MonoBehaviour {

	public GameObject obj;
	public float dur;

	private GameObject temp;

	void Update () {
		if (dur > 0) {
			dur -= Time.deltaTime;
		} else {
			dur = Random.Range(0.0f, 30.0f);
			if (temp == null) {
				temp = Instantiate(obj, gameObject.transform.position, Quaternion.identity);
				temp.transform.SetParent(gameObject.transform);
			}
		}
	}
}
