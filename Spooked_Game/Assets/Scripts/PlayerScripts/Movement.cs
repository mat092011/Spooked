using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float moveSpeed = 10f;
    private float delayAfterFail = 1.0f;
    private float delayAfterKnockBack = 0f;

	private AudioSource audioWalk;
    private Rigidbody2D playerPhysics;
    private Animator anim;
    private Vector3 tempEnemyPosition;

	private int move = -1;
	public static bool fail;
    public float protectionDur;
    public bool protection = false;
    public bool grounded;
	public bool jumpAllowed;
    private float airSpeed;
    public static bool runSpeed;
    private bool facingRight = false;
	public Vector2 vectorOfMove;
	public bool left = false;
	public bool right = false;
	public static int enemyExists;

    public int worms;
    public int grasshoppers;
    public int butterflies;
    public int bees;
    public int carterpilars;
    public int frogs;
    public int dragonflies;
    public int flies;

    void Start() {
		audioWalk = gameObject.GetComponent<AudioSource>();
        playerPhysics = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate() {
        anim.SetBool("Fail", fail);
        anim.SetBool("Grounded", grounded);         //anim
        anim.SetBool("RunSpeed", runSpeed);

		if (GetComponentInParent<MovingPlatform>() == null) {
			if (playerPhysics.velocity.y == 0) {
				if (jumpAllowed != true) {
					jumpAllowed = true;
				}
			} else {
				if (jumpAllowed != false) {
					jumpAllowed = false;
				}
			}
		} else {
			if (jumpAllowed != true) {
				jumpAllowed = true;
			}
		}

		if (runSpeed) {
			runSpeed = false;
		}

		if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || left || Input.GetButton("Left")) && !fail) {
			if (grounded && jumpAllowed) {
				airSpeed = 1;
			} else { airSpeed = 0.10f; }
			if (move > 0) {
				move = -1;
			}
			playerPhysics.AddForce(Vector2.left * moveSpeed * airSpeed);     //run left
			runSpeed = true;
		}

		if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || right || Input.GetButton("Right")) && !fail) {
			if (grounded && jumpAllowed) {
				airSpeed = 1;
			} else { airSpeed = 0.10f; }
			if (move < 0) {
				move = 1;
			}
			playerPhysics.AddForce(Vector2.right * moveSpeed * airSpeed);          //run right
			runSpeed = true;
		}

		if (runSpeed && grounded) {
			if (!audioWalk.loop) {
				audioWalk.loop = true;
				audioWalk.Play();
			}
		} else {
			audioWalk.loop = false;
		}

		if (playerPhysics.velocity.magnitude > 8.0f) {
            playerPhysics.velocity = playerPhysics.velocity.normalized * 8.0f;
        }

        if (move > 0 && !facingRight) {
            Flip();
        } else if (move < 0 && facingRight) {
            Flip();
        }
    }

    void Update() {
		if (protection) {
			Protection();
		}

        CheckApplyKnockBack();
    }

    void CheckApplyKnockBack() {
        if (protection == false) {
            if (GhostMovementScript.onCollisionWithPlayer == true && fail != true) {
                fail = true;
                GameObject tempEnemy = GameObject.FindGameObjectWithTag("Enemy");
                tempEnemyPosition = new Vector3(tempEnemy.transform.position.x, tempEnemy.transform.position.y);
                delayAfterKnockBack = Random.Range(0.1f, 0.3f);
            }

            if (EnemyBulletScript.fail == true && fail != true) {
                fail = true;

                if (SceletonMovement.bulletRelativePosition == true) {
                    tempEnemyPosition = playerPhysics.transform.position - new Vector3(3, 0);
                }
                if (SceletonMovement.bulletRelativePosition == false) {
                    tempEnemyPosition = playerPhysics.transform.position + new Vector3(3, 0);
                }
                delayAfterKnockBack = Random.Range(0.1f, 0.3f);
            }

            if (fail == true && delayAfterFail > 0) {
                delayAfterFail -= Time.deltaTime;
                delayAfterKnockBack -= Time.deltaTime;
                if (delayAfterKnockBack > 0) {
                    if (playerPhysics.transform.position.x < tempEnemyPosition.x) {
                        playerPhysics.AddForce(new Vector2(-1 * (moveSpeed + 40f), 1 * (moveSpeed + 500f)) * Time.deltaTime);
                    }
                    if (playerPhysics.transform.position.x > tempEnemyPosition.x) {
                        playerPhysics.AddForce(new Vector2(1 * (moveSpeed + 40f), 1 * (moveSpeed + 500f)) * Time.deltaTime);
                    }
                }
                if (delayAfterFail <= 0) {
                    fail = false;
                    delayAfterFail = 1.0f;
                    delayAfterKnockBack = 0;
                    EnemyBulletScript.fail = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Enemy" && fail != true) {
            if (protection == false) {
                fail = true;
                GameObject tempEnemy = GameObject.FindGameObjectWithTag("Enemy");
                tempEnemyPosition = new Vector3(tempEnemy.transform.position.x, tempEnemy.transform.position.y);
                delayAfterKnockBack = Random.Range(0.5f, 1.2f);
            } else {
                GameObject Enemy = GameObject.FindGameObjectWithTag("Enemy");
                Physics2D.IgnoreCollision(Enemy.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
            }
        }
    }

	void Protection() {
		if (protectionDur > 0) {
			protectionDur -= Time.deltaTime;
		}else {
			if (protection) {
				protection = false;                         /*must be uncommented!!!!!*/
			}
		}
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
		transform.localScale = Scale;
    }
}
