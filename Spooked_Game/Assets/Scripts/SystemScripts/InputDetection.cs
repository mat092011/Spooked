using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputDetection : MonoBehaviour {

	private Vector2[] TouchPosition = new Vector2[10];
	private GameObject fireP1;
	private GameObject fireP2;

	public GameObject TopPanel;
	public Image scaredyness;
	public static float scared = 1.0f;
	public GameObject firePoint1;
	public GameObject firePoint2;
	public GameObject FireParticle;

	public Collider2D ButtonLeft;
	public Collider2D ButtonRight;
	public Collider2D JumpButton;
	public Collider2D Fire;

	public SpriteRenderer[] renderers = new SpriteRenderer[4];
	public Sprite spriteDefault;
	public Sprite spriteChanged;

	public GameObject Player;

	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		fireP1 = Instantiate(FireParticle, firePoint1.transform.position, Quaternion.LookRotation(new Vector3(0, -90, 0)));
		fireP1.transform.parent = TopPanel.transform;
		fireP2 = Instantiate(FireParticle, firePoint2.transform.position, Quaternion.LookRotation(new Vector3(0, -90, 0)));
		fireP2.transform.parent = TopPanel.transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (scared > 1f) {
			scared = 1f;
		}

		if (scared >= 0) {
			scared -= Time.deltaTime / 30;
		}
		scaredyness.fillAmount = scared;
		

		if (scaredyness.fillAmount > 0.7f && (fireP1 == null && fireP2 == null)) {
			fireP1 = Instantiate(FireParticle, firePoint1.transform.position, Quaternion.LookRotation(new Vector3(0, -90, 0)));
			fireP1.transform.parent = gameObject.transform;
			fireP2 = Instantiate(FireParticle, firePoint2.transform.position, Quaternion.LookRotation(new Vector3(0, -90, 0)));
			fireP2.transform.parent = gameObject.transform;
		}
		if (scaredyness.fillAmount < 0.7f && (fireP1 != null && fireP2 != null)) {
			Destroy(fireP1);
			Destroy(fireP2);
		}

		InputFinger();

	}

	void InputFinger() {
		if (Input.touchCount > 0) {
			for (int i = 0; i < Input.touchCount; i++) {
				if (Input.GetTouch(i).phase == TouchPhase.Began) {
					TouchPosition[i] = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
					RaycastHit2D hit = Physics2D.Raycast(TouchPosition[i], new Vector2(TouchPosition[i].x + 0.01f, TouchPosition[i].y + 0.01f));
					if (hit.collider == ButtonLeft) {
						if (renderers[0].sprite != spriteChanged) {
							renderers[0].sprite = spriteChanged;
						}
						if (Player.GetComponent<Movement>().left != true) {
							Player.GetComponent<Movement>().left = true;
						}
					}
					if (hit.collider == ButtonRight) {
						if (renderers[1].sprite != spriteChanged) {
							renderers[1].sprite = spriteChanged;
						}
						if (Player.GetComponent<Movement>().right != true) {
							Player.GetComponent<Movement>().right = true;
						}
					}
					if (hit.collider == Fire) {
						if (renderers[3].sprite != spriteChanged) {
							renderers[3].sprite = spriteChanged;
						}
						if (Player.GetComponent<ActionScript>().fire != true) {
							Player.GetComponent<ActionScript>().fire = true;
						}
					}
					if (hit.collider == JumpButton) {
						if (renderers[2].sprite != spriteChanged) {
							renderers[2].sprite = spriteChanged;
						}
						if (Jump.jump != true) {
							Jump.jump = true;
						}
					}
				}
				if (Input.GetTouch(i).phase == TouchPhase.Ended) {
					for (int a = 0; a < 4; a++) {
						renderers[a].sprite = spriteDefault;
					}
					if (Player.GetComponent<Movement>().left != false) {
						Player.GetComponent<Movement>().left = false;
					}
					if (Player.GetComponent<Movement>().right != false) {
						Player.GetComponent<Movement>().right = false;
					}
					if (Player.GetComponent<ActionScript>().fire != false) {
						Player.GetComponent<ActionScript>().fire = false;
					}
					if (Jump.jump != false) {
						Jump.jump = false;
					}
				}
			}
		}
	}
}
