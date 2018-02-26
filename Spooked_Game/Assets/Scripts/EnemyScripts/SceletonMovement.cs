using UnityEngine;
using System.Collections;

public class SceletonMovement : MonoBehaviour {

	public GameObject[] deathAnim = new GameObject[4];
	public GameObject BulletPos;
    public GameObject Bullet;
	public GameObject EffHit;
	public Rigidbody2D enemyPhysics;

	public float moveSpeed = 10f;
	public float jumpHeight = 1000f;
	public bool onCollisionWithPlayer = false;
	public bool bulletRight;
	public bool WallTriggered;
	public int colCount = 0;

	private GameObject Enemy;
    private GameObject Player;
    private Animator anim;
    Vector3 vector = new Vector3();

    private float airSpeed;
	private float delayToShoot;
    private bool grounded;
    private bool facingRight = true;
    private bool destroyEnemyUnit;
    private bool playerBulletScoreValue;
    private bool playerFireBallScoreValue;
    private bool runSpeed;

    public static bool bulletRelativePosition;
	public static bool sceletonBulletScoreValue;

	void Start() {
        onCollisionWithPlayer = false;
        sceletonBulletScoreValue = false;
        enemyPhysics = gameObject.GetComponent<Rigidbody2D>();
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
        delayToShoot = Random.Range(0.5f, 1.0f);
        FindPosition();
    }

    void Update() {
        if (delayToShoot > 0) {
            delayToShoot -= Time.deltaTime;
        }

        CheckPlayerBulletScoreValue();

        if (BulletPos.transform.position.x > Enemy.transform.position.x) {
            bulletRelativePosition = true;
        }
        else { bulletRelativePosition = false; }

        if (delayToShoot <= 0.0f && !onCollisionWithPlayer && !sceletonBulletScoreValue && (Enemy.transform.position.y < Player.transform.position.y + 1f && Enemy.transform.position.y > Player.transform.position.y - 1f)) { // if can shoot and bullet didn`t reach player and yPlayer == yEnemy 
			int randomize = Random.Range(1, 4);
            if (randomize == 2) {
                SpawnBullet();
            } else { delayToShoot = Random.Range(1.0f, 2.5f); }
        }

		RaycastHit2D hit = Physics2D.Raycast(BulletPos.transform.position, BulletPos.transform.position);
		if (hit.collider.tag == "Ground" || hit.collider.tag == "Untagged") {
			if (onCollisionWithPlayer) {
				onCollisionWithPlayer = false;
			}
			if (!WallTriggered) {
				WallTriggered = true;
			}
		}
	}

    void FixedUpdate() {
        anim.SetBool("Grounded", grounded);         //anim
        anim.SetBool("RunSpeed", runSpeed);

		if (colCount > 1) {
			colCount = 0;
			WallTriggered = false;
			vector = -vector;
		}
        if (WallTriggered && colCount >= 0) {
            WallTriggered = false;
			colCount++;
			FindPosition();
        } else {
			if (!runSpeed) {
				runSpeed = true;
			}
            enemyPhysics.AddForce(vector * moveSpeed * airSpeed);      //sceleton runs
        }
        if (((Enemy.transform.position.y + 0.25 < Player.transform.position.y)) && grounded && !onCollisionWithPlayer && enemyPhysics.velocity.y == 0) {
			enemyPhysics.AddForce(new Vector2(0, 50) * jumpHeight);
		}
		if (enemyPhysics.velocity.magnitude > 8.0f) {
			enemyPhysics.velocity = enemyPhysics.velocity.normalized * 8.0f;
		}


		if (vector == new Vector3(-1, 0) && !facingRight) {
            Flip();
        } else if (vector == new Vector3(1, 0) && facingRight) {
            Flip();
        }


		if (destroyEnemyUnit) {

			Instantiate (EffHit, gameObject.transform.position, Quaternion.identity);

			for (int i = 0; i < 4; i++) {
				GameObject Obj = Instantiate(deathAnim[i], gameObject.transform.position, Quaternion.identity);
				Rigidbody2D rig = Obj.GetComponent<Rigidbody2D>();
				if (bulletRight) {
					rig.AddForce(new Vector2(-40, 0));
				} else if (!bulletRight) {
					rig.AddForce(new Vector2(40, 0));
				}
			}

			Destroy(gameObject);            //destroy when out of range
			SpawnSystemScript.enemyExists--;
			PlayerBulletScript.destroyEnemyAfterColision = false;
			PlayerFireballScript.destroyEnemyAfterColision = false;
			WallTriggered = false;
			onCollisionWithPlayer = false;
			runSpeed = false;
		}
	}

    void FindPosition() {
        if (Enemy.transform.position.x > Player.transform.position.x) {
            vector = new Vector3(-1, 0);
        } else { vector = new Vector3(1, 0); }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Ground") {
            airSpeed = 1;
            grounded = true;
        }
        if (col.gameObject.tag == "Player") {
            if (Enemy.transform.position.y > Player.transform.position.y) {
				enemyPhysics.AddForce(new Vector2(0, 50) * jumpHeight);
			}
            FindPosition();
            vector = -vector;
            onCollisionWithPlayer = true;
        }
    }
	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.tag == "Ground") {
			airSpeed = 1;
			grounded = true;
		}
	}
	void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Area") {
            destroyEnemyUnit = false;
        }
    }
    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.tag == "Ground") {
            airSpeed = 0.1f;
            grounded = false;
        }
    }
	void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Area") {
            destroyEnemyUnit = true;
        }
    }

    void SpawnBullet() {
        Instantiate(Bullet, BulletPos.transform.position, Quaternion.identity);         //draw bullet
        delayToShoot = Random.Range(1.0f, 2.5f);
    }

    void CheckPlayerBulletScoreValue() {
        playerBulletScoreValue = PlayerBulletScript.destroyEnemyAfterColision;
        playerFireBallScoreValue = PlayerFireballScript.destroyEnemyAfterColision;
        if (playerBulletScoreValue == true) {
            destroyEnemyUnit = true;
        }
        if (playerFireBallScoreValue == true) {
            destroyEnemyUnit = true;
        }
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
