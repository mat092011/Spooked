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

	public GameObject pictogram;
	public GameObject Lcircle;
	public GameObject Rcircle;

	public Collider2D[] hitCol = new Collider2D[10];
	public Collider2D ButtonLeft;
	public Collider2D ButtonRight;
	public Collider2D JumpButton;
	public Collider2D Fire;
	public Collider2D LCircle;
	public Collider2D RCircle;
	public Vector2 LCVector;
	public Vector2 RCVector;

	public SpriteRenderer[] renderers = new SpriteRenderer[4];
	public Sprite spriteDefault;
	public Sprite spriteChanged;

	public GameObject Player;

	public Text[] txt1 = new Text[5];

	public int a = 0;

	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		fireP1 = Instantiate(FireParticle, firePoint1.transform.position, Quaternion.LookRotation(new Vector3(0, -90, 0)));
		fireP1.transform.parent = TopPanel.transform;
		fireP2 = Instantiate(FireParticle, firePoint2.transform.position, Quaternion.LookRotation(new Vector3(0, -90, 0)));
		fireP2.transform.parent = TopPanel.transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.I)) {
			Vector2 vect = new Vector2(Lcircle.transform.position.x + 5 * Mathf.Cos(0.349066f * a), Lcircle.transform.position.y + 5 * Mathf.Sin(0.349066f * a));
			Instantiate(pictogram, vect, Quaternion.identity);
			a++;
		}

		if (scared > 1.5f) {
			scared = 1.5f;
		}

		if (scared >= 0) {
			scared -= Time.deltaTime / 30;
		}
		scaredyness.fillAmount = scared;
		

		if (scaredyness.fillAmount == 1f && (fireP1 == null && fireP2 == null)) {
			fireP1 = Instantiate(FireParticle, firePoint1.transform.position, Quaternion.LookRotation(new Vector3(0, -90, 0)));
			fireP1.transform.parent = gameObject.transform;
			fireP2 = Instantiate(FireParticle, firePoint2.transform.position, Quaternion.LookRotation(new Vector3(0, -90, 0)));
			fireP2.transform.parent = gameObject.transform;
		}
		if (scaredyness.fillAmount < 1f && (fireP1 != null && fireP2 != null)) {
			Destroy(fireP1);
			Destroy(fireP2);
		}

		InputFinger();

		FingerCheck();
	}

	void InputFinger() {
		for (int i = 0; i < 4; i++) {
			if (hitCol[i] != null) {
				txt1[i].text = hitCol[i].name;
			} else {
				txt1[i].text = "null";
			}
		}
		txt1[4].text = Input.touchCount.ToString();
		if (Input.touchCount > 0) {
			for (int i = 0; i < Input.touchCount; i++) {

				TouchPosition[i] = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
				RaycastHit2D hit = Physics2D.Raycast(TouchPosition[i], new Vector2(TouchPosition[i].x + 0.01f, TouchPosition[i].y + 0.01f));
				hitCol[i] = hit.collider;

				if (Input.GetTouch(i).phase == TouchPhase.Began) {
					if (hitCol[i] == LCircle) {
						LCVector = Input.GetTouch(i).position;
					}
					if (hitCol[i] == RCircle) {
						RCVector = Input.GetTouch(i).position;
					}
				}

				if (Input.GetTouch(i).phase == TouchPhase.Moved) {
					if (hitCol[i] == LCircle) {
						Vector2 vec = Input.GetTouch(i).deltaPosition;
						Lcircle.transform.Rotate(new Vector3(0,0, vec.y/5));
					}
					if (hitCol[i] == RCircle) {
						Vector2 vec = Input.GetTouch(i).deltaPosition;
						Rcircle.transform.Rotate(new Vector3(0, 0, -vec.y/5));
					}
				}

				if (Input.GetTouch(i).phase == TouchPhase.Ended) {
					hitCol[i] = null;
				}
			}
		}
	}

	void FingerCheck() {
		int a = 0;
		for (int i = 0; i < hitCol.Length; i++) {
			if (hitCol[i] != null) {
				a++;
			}
		}
		if (a > Input.touchCount) {
			for (int i = hitCol.Length - Input.touchCount; i > 0; i--) {
				hitCol[i - 1] = null;
			}
		}
	}
}
