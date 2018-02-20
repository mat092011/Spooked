using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronTriggerEnter : MonoBehaviour {

	private bool spawned = false;
	private GameObject temp;
	private float checkDur = 0.5f;
	private bool dur;
	private Vector3 Scale = new Vector3(0.6097561f, 0.6097561f, 1);

	public GameObject Circle;
	public GameObject SpawnPoint;

	public GameObject Player;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (dur && checkDur > 0) {
			checkDur -= Time.deltaTime;
		}
		if (checkDur <= 0 && !spawned) {
			spawned = true;
		}

		if (temp != null) {
			if (spawned) {
				temp.transform.localScale = Vector3.Lerp(temp.transform.localScale, Scale, 2 * Time.deltaTime);
				temp.transform.Find("Star").gameObject.SetActive(true);
				temp.transform.Find("CauldronLeaves").gameObject.SetActive(true);
			}
			if (!spawned) {
				temp.transform.localScale = Vector3.Lerp(temp.transform.localScale, new Vector3(0, 0, 1), 2 * Time.deltaTime);
				temp.transform.Find("Star").gameObject.SetActive(false);
				temp.transform.Find("CauldronLeaves").gameObject.SetActive(false);
				if (temp.transform.localScale == new Vector3(0, 0, 1) && !dur) {
					Destroy(temp);
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject == GameObject.Find("Player") && !spawned) {
			if (temp == null) {
				temp = Instantiate(Circle, SpawnPoint.transform.position, Quaternion.identity);
				temp.transform.SetParent(gameObject.transform);
			}
			dur = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision) {
		if (collision.gameObject == GameObject.Find("Player") && spawned) {
			checkDur = 0.5f;
			dur = false;
			spawned = false;
		}
	}
}
