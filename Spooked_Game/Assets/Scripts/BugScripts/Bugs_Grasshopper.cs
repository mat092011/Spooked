using UnityEngine;
using System.Collections;

public class Bugs_Grasshopper : MonoBehaviour
{
    public float moveForce = 7;
    private float jumpTime;
    private float delayToJump = 0.7f;
    private float delayToWalk = 1.4f;
    private float delayStop = 4.0f;
    public bool grounded;
    private int jumpRight;
    public GameObject Enemy;
    private GameObject Player;
    private Vector2 vectorOfMovement;
    Rigidbody2D grasshopPhysics;
    private bool facingRight = true;
    private int look;

	void Start() {
		grasshopPhysics = gameObject.GetComponent<Rigidbody2D>();
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update() {
		delayStop -= Time.deltaTime;
		if (delayStop > 0) {
			delayToJump -= Time.deltaTime;
			if (delayToJump < 0) {
				if ((Player.transform.position.x < gameObject.transform.position.x) && grounded && jumpRight == 0) {
					look = 1;
					jumpRight = 1;
					jumpTime = Random.Range(0.2f, 0.4f);
					vectorOfMovement = new Vector2(1, 1);
				}
				if ((Player.transform.position.x > gameObject.transform.position.x) && grounded && jumpRight == 0) {
					look = -1;
					jumpRight = -1;
					jumpTime = Random.Range(0.2f, 0.4f);
					vectorOfMovement = new Vector2(-1, 1);
				}
			} else {
				delayToWalk -= Time.deltaTime;
				if (delayToWalk > 0) {
					if ((Player.transform.position.x < gameObject.transform.position.x) && grounded) {
						look = 1;
						grasshopPhysics.velocity = Vector2.right * moveForce;
					}
					if ((Player.transform.position.x > gameObject.transform.position.x) && grounded) {
						look = -1;
						grasshopPhysics.velocity = Vector2.left * moveForce;
					}
				} else {
					if ((Player.transform.position.x > gameObject.transform.position.x) && grounded) {
						look = 1;
						grasshopPhysics.velocity = Vector2.right * moveForce;
					}
					if ((Player.transform.position.x < gameObject.transform.position.x) && grounded) {
						look = -1;
						grasshopPhysics.velocity = Vector2.left * moveForce;
					}
				}
			}
			if (jumpRight == 1) {
				jumpTime -= Time.deltaTime;
				Jump(vectorOfMovement);
			}
			if (jumpRight == -1) {
				jumpTime -= Time.deltaTime;
				Jump(vectorOfMovement);
			}
		} else {
			if (grounded) {
				grasshopPhysics.velocity = Vector2.zero * moveForce;
			}
			if (delayStop < -2) {
				delayStop = Random.Range(2.5f, 4f);
			}
		}

		if (look < 0 && facingRight) {
			Flip();
		} else if (look > 0 && !facingRight) {
			Flip();
		}
	}

	void Jump(Vector2 vectorOfMovement) {
		if (jumpTime > 0) {
			grasshopPhysics.velocity = vectorOfMovement * moveForce;
		} else { jumpRight = 0; jumpTime = Random.Range(0.2f, 0.4f); ; }
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "BugArea") {
			delayStop = Random.Range(2.5f, 4f);
		}
		if (col.gameObject.tag == "Player") {
			SoundSystemScript.playBugCatchSound = true;
			GameObject.Find("Player").GetComponent<Movement>().grasshoppers++;
			SpawnSystemScript.bugExists--;
			Destroy(gameObject);
		}
		if (col.gameObject.tag == "Ground") {
			grounded = true;
			delayToJump = Random.Range(1.0f, 3.0f);
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Ground") {
			if (!grounded) {
				grounded = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Area") {
			SpawnSystemScript.bugExists--;
			Destroy(gameObject);
		}
		if (col.gameObject.tag == "Ground") {
			grounded = false;
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 Scale = transform.localScale;
		Scale.x *= -1;
		transform.localScale = Scale;
	}
}
