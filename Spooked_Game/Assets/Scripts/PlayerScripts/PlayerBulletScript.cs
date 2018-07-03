using UnityEngine;
using System.Collections;

public class PlayerBulletScript : MonoBehaviour {

    public int Speed = 10;
    private GameObject PlayerBul;
    private Rigidbody2D rgb2;
	bool bulletGoesRight;
	public static bool destroyEnemyAfterColision;
    public bool destroy = false;

    void Start () {
        destroyEnemyAfterColision = false;
        PlayerBul = gameObject;
        rgb2 = gameObject.GetComponent<Rigidbody2D>();
        bulletGoesRight = ActionScript.playerBulletRelativePosition;
        if (bulletGoesRight == true) {
            rgb2.velocity = (Vector2.right * Speed);
        } else {                                                             //optional bullet flight
			Flip();
            rgb2.velocity = (Vector2.left * Speed);
        }
    }

    void Update() {
		RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, gameObject.transform.position);
		if (hit.collider == gameObject.GetComponent<Collider2D>() && hit.collider.isTrigger == false) {
			if (bulletGoesRight == true) {
				rgb2.velocity = (Vector2.left * Speed);
			} else {                                                             //optional bullet flight
				Flip();
				rgb2.velocity = (Vector2.right * Speed);
			}
		}
		if (destroy) {
            Destroy(gameObject);
            destroy = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Enemy") {
            destroyEnemyAfterColision = true;
            Destroy(PlayerBul);                           //destroy after hitting player
        }
		if (col.isTrigger == false && rgb2 != null) {
			if (bulletGoesRight == true) {
				rgb2.velocity = (Vector2.left * Speed);
			} else {                                                             //optional bullet flight
				Flip();
				rgb2.velocity = (Vector2.right * Speed);
			}
		}
    }

    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.tag == "Enemy") {
			InputDetection.scared += 0.3f;
			destroyEnemyAfterColision = true;
            Destroy(PlayerBul);                           //destroy after hitting player
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Area") {
            Destroy(PlayerBul);                   //destroy after out of range
        }
    }

    void Flip() {
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
