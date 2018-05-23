using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundOpacity : MonoBehaviour {

	public GameObject X;
	public GameObject Y;
	private float alpha;
	
	void Update () {
		alpha = ((gameObject.transform.position.y + 41.3f) * 255) / ((Y.transform.position.y + 41.3f) - (X.transform.position.y + 41.3f));
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha/255f);
	}
}
