using UnityEngine;
using System.Collections;

public class GhostMovementScript : MonoBehaviour {

	public GameObject DieAnim;

	private Animator anim;
	private GameObject Player;
	private GameObject ghostZone;
	private Rigidbody2D ghostPhysics;
    public float Speed = 2.0f;
	//private float delayToFly = 1.5f;
	//private float delayToRandMove = 0.3f;
	private bool agro;
	private bool attack = false;
    private bool facingRight;
	private bool changeVector = false;
    private int directionFlipManager;
    public static bool onCollisionWithPlayer = false;
	//private bool flyAway;
	//   private bool changeVector = true;
	//   private int waitMoveTryCount = 0;
	private bool destroy = false;
	//private Vector2 tempVector;
	//private Vector2 ShortMoveVector;
	private int numberOfMoves = 0;
	private float durationOfFly;
	private Vector2 vectorOfMovement;


    void Start () {
		anim = gameObject.GetComponent<Animator>();
		Player = GameObject.FindGameObjectWithTag("Player");
		ghostZone = GameObject.FindGameObjectWithTag("EnemyAnimZone");
		vectorOfMovement = new Vector2(Player.transform.position.x, Player.transform.position.y);
		ghostPhysics = gameObject.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		anim.SetBool("Agro", agro);
		anim.SetBool("Attack", attack);         //anim

		if (Movement.runSpeed && !onCollisionWithPlayer) {
			if (!agro) {
				agro = true;
			}
			vectorOfMovement = new Vector2(Player.transform.position.x, Player.transform.position.y);
			ghostPhysics.velocity = new Vector2() * 0;
			gameObject.transform.position = Vector2.MoveTowards(transform.position, vectorOfMovement, Speed * Time.deltaTime); // moving to player
			changeVector = true;
		} else {
			if (agro) {
				agro = false;
			}
			if (onCollisionWithPlayer) {
				int y = 0;
				switch (Random.Range(0,2)) {
					case 0: y = -1; break;
					case 1: y = 1; break;
				}
				vectorOfMovement = new Vector2(Random.Range(-1, 2), y); // collision fly away
				onCollisionWithPlayer = false;
			} else {
				if (durationOfFly <= 0 && numberOfMoves < 9) {
					vectorOfMovement = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
					durationOfFly = Random.Range(0.5f, 0.8f); // random moves
					numberOfMoves++;
				} else {
					if (changeVector) {
						float x = 0; // vectorChanged
						float y = 0;
						switch(Random.Range(0,4)) {
							case 0: x = Random.Range(-1f, 0f); Random.Range(-1f, 0f); break;
							case 1: x = Random.Range(-1f, 0f); y = Random.Range(0f, 1f); break;
							case 2: x = Random.Range(0f, 1f); y = Random.Range(-1f, 0f); break;
							case 3: x = Random.Range(0f, 1f); y = Random.Range(0f, 1f); break;
						}
						vectorOfMovement = new Vector2(x, y);
						changeVector = false;
					}
					ghostPhysics.velocity = vectorOfMovement * Speed;
					durationOfFly -= Time.deltaTime;
				}
			}
		}
		//      vectorOfMovement = new Vector2(Player.transform.position.x, Player.transform.position.y);
		//      if (!onCollisionWithPlayer && !flyAway) {
		//          if (Movement.runSpeed == true) {
		//              if (delayToFly != 1.2f) {
		//                  delayToFly = 1.2f;
		//              }
		//              gameObject.transform.position = Vector2.MoveTowards(transform.position, vectorOfMovement, Speed * Time.deltaTime);
		//              ShortMoveVector = gameObject.transform.position;
		//          } else {
		//              if (delayToFly > 0 && waitMoveTryCount < 8) {
		//                  delayToFly -= Time.deltaTime;
		//                  if (delayToRandMove > 0) {
		//                      ShortRandomMovement();
		//                  } else { ShortMoveVector = new Vector2(); delayToRandMove = Random.Range(0.5f, 2.0f); }
		//              }
		//              else {
		//                  gameObject.transform.position = Vector2.MoveTowards(transform.position, tempVector, Speed * Time.deltaTime);
		//                  if (Movement.runSpeed == true) {
		//                      waitMoveTryCount = 6;
		//                  }
		//              }
		//              if (changeVector == true) {
		//                  int x = Random.Range(0, 2);
		//                  int y = Random.Range(0, 2);
		//                  switch(x) {
		//                      case 0: x = Random.Range(-100, -50); break;
		//                      case 1: x = Random.Range(50, 100); break;
		//                  }
		//                  switch(y) {
		//                      case 0: y = Random.Range(-100, -50); break;
		//                      case 1: y = Random.Range(50, 100); break;
		//                  }
		//                  tempVector = new Vector2(x, y) + new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
		//                  changeVector = false;
		//              }
		//          }
		//      }
		//      if (onCollisionWithPlayer) {
		//	waitMoveTryCount = 4;
		//	ShortMoveVector = new Vector2();
		//	delayToFly = 0;
		//	delayToRandMove = 2.0f;
		//          onCollisionWithPlayer = false;
		//      }
		//if (flyAway) {
		//	if (delayToRandMove > 0) {
		//		ShortRandomMovement();
		//	} else {
		//		flyAway = false;
		//	}
		//}

		if (PlayerBulletScript.destroyEnemyAfterColision == true || PlayerFireballScript.destroyEnemyAfterColision == true) {
			destroy = true;

			Instantiate (DieAnim, gameObject.transform.position, Quaternion.identity);
		}

		if (destroy) {
            Destroy(gameObject);
			numberOfMoves = 0;
            onCollisionWithPlayer = false;
            //waitMoveTryCount = 0;
            PlayerBulletScript.destroyEnemyAfterColision = false;
            PlayerFireballScript.destroyEnemyAfterColision = false;
            SpawnSystemScript.enemyExists--;
            destroy = false;
        }

        if (gameObject.transform.position.x < Player.transform.position.x) {
            directionFlipManager = 1;
        }
        else { directionFlipManager = -1; }

        if (directionFlipManager < 0 && !facingRight) {
            Flip();
        }
        else if (directionFlipManager > 0 && facingRight) {
            Flip();
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Area" && !attack) {
            destroy = true;
        }
		if (col.gameObject == ghostZone) {
			if (attack) {
				attack = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject == Player) {
			onCollisionWithPlayer = true;
			Player.GetComponent<ActionScript>().Clock.fillAmount -= 50f / 300f;
		}
		if (col.gameObject.tag == "PlayerBullet") {
			destroy = true;

			Instantiate(DieAnim, gameObject.transform.position, Quaternion.identity);
		}
		if (col.gameObject == ghostZone) {
			if (!attack) {
				attack = true;
			}
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject == ghostZone) {
			if (!attack) {
				attack = true;
			}
		}
	}

	//void ShortRandomMovement() {
	//    delayToRandMove -= Time.deltaTime;
	//    if (ShortMoveVector == new Vector2()) {
	//        ShortMoveVector = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y) + new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
	//        waitMoveTryCount++;
	//    }
	//    gameObject.transform.position = Vector2.MoveTowards(transform.position, ShortMoveVector, Speed * Time.deltaTime);
	//}

	void Flip() {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
