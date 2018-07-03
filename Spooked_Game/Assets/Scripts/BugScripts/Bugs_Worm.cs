using UnityEngine;
using System.Collections;

public class Bugs_Worm : MonoBehaviour {

    public float Speed = 6;
	private bool facingRight;
	private bool direction;
	private bool stay;
	private float airSpeed;
    private float delayToChangeVector = 1.0f;
    private float delayStop = 4.0f;
    private Vector2 vectorOfMovement;
	private Animator anim;
	Rigidbody2D wormPhysics;

    void Start() {
        RandomVector();
        wormPhysics = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator>();
	}

	void Update () {
		anim.SetBool("Stay", stay);
		delayStop -= Time.deltaTime;
        if (delayStop > 0) {
			if (stay) {
				stay = false;
			}
            delayToChangeVector -= Time.deltaTime;
            if (delayToChangeVector <= 0) {
                delayToChangeVector = Random.Range(0.2f, 1.4f);
                RandomVector();
            }
            wormPhysics.velocity = vectorOfMovement * Speed * airSpeed;
        } else {
			if (!stay) {
				stay = true;
			}
            wormPhysics.velocity = Vector2.zero * Speed;
            if (delayStop < -2) {
                delayStop = Random.Range(2.5f, 4f);
            }
        }

		if (vectorOfMovement == Vector2.right && direction) {
			direction = false;
		}
		if (vectorOfMovement == Vector2.left && !direction) {
			direction = true;
		}

		if (direction && !facingRight) {
			Flip();
		} else if (!direction && facingRight) {
			Flip();
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Ground") {
			airSpeed = 1;
		}
		if (col.gameObject.tag == "BugArea") {
            delayStop = Random.Range(2.5f, 4f);
            vectorOfMovement = -vectorOfMovement;
        }
		if (col.gameObject.tag == "Player") {
			SoundSystemScript.playBugCatchSound = true;
			GameObject.Find("Player").GetComponent<Movement>().worms++;
			SpawnSystemScript.bugExists--;
			Destroy(gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Ground") {
			if (airSpeed != 1) {
				airSpeed = 1;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Area") {
            SpawnSystemScript.bugExists--;
            Destroy(gameObject);
        }
		if (col.gameObject.tag == "Ground") {
			airSpeed = 0;
		}
	}

    void RandomVector() {
        int vectorSide;
        vectorSide = Random.Range(1, 3);
        switch(vectorSide) {
            case 1: vectorOfMovement = Vector2.right; break;
            case 2: vectorOfMovement = Vector2.left; break;
        }
    }

    void Flip() {
		facingRight = !facingRight;
		Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
