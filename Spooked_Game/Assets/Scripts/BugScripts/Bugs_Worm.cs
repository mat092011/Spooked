using UnityEngine;
using System.Collections;

public class Bugs_Worm : MonoBehaviour {

    public float Speed = 6;
	private float airSpeed;
    private float delayToChangeVector = 1.0f;
    private float delayStop = 4.0f;
    private Vector2 vectorOfMovement;
    Rigidbody2D wormPhysics;
    private GameObject Enemy;

    void Start() {
        RandomVector();
        wormPhysics = gameObject.GetComponent<Rigidbody2D>();
    }

	void Update () {
        delayStop -= Time.deltaTime;
        if (delayStop > 0) {
            delayToChangeVector -= Time.deltaTime;
            if (delayToChangeVector <= 0) {
                delayToChangeVector = Random.Range(0.2f, 1.4f);
                RandomVector();
            }
            wormPhysics.velocity = vectorOfMovement * Speed * airSpeed;
        } else {
            wormPhysics.velocity = Vector2.zero * Speed;
            if (delayStop < -2) {
                delayStop = Random.Range(2.5f, 4f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player") {
			SoundSystemScript.playBugCatchSound = true;
			GameObject.Find("Player").GetComponent<Movement>().worms++;
            SpawnSystemScript.bugExists--;
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "PlayerBullet") {
            SpawnSystemScript.bugExists--;
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy") {
            Enemy = GameObject.FindGameObjectWithTag("Enemy");
            Physics2D.IgnoreCollision(Enemy.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
        }
		if (col.gameObject.tag == "Ground") {
			airSpeed = 1;
		}
    }

	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.tag == "Ground") {
			airSpeed = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "BugArea") {
            delayStop = Random.Range(2.5f, 4f);
            vectorOfMovement = -vectorOfMovement;
            Flip();
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Area") {
            SpawnSystemScript.bugExists--;
            Destroy(gameObject);
        }
    }

    void RandomVector() {
        int vectorSide;
        vectorSide = Random.Range(1, 3);
        switch(vectorSide) {
            case 1: vectorOfMovement = Vector2.right; break;
            case 2: vectorOfMovement = Vector2.left; break;
        }
        Flip();
    }

    void Flip(){
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
