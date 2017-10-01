﻿using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public Rigidbody2D enemyPhysics;
    public GameObject BulletPos;
    public GameObject Bullet;
    private GameObject Enemy;
    private GameObject Player;
    private Animator anim;
    Vector3 vector = new Vector3();
    public float moveSpeed = 10f;
    public float jumpHeight = 1000f;
    private float airSpeed;
    public bool WallTriggered;
    private bool grounded;
    private bool facingRight = true;
    private bool destroyEnemyUnit;
    public static bool sceletonBulletScoreValue;
    private bool playerBulletScoreValue;
    private bool playerFireBallScoreValue;
    private bool runSpeed;
    private float delayToShoot;
    public static bool bulletRelativePosition;
    public bool onCollisionWithPlayer = false;

    void Start()
    {
        onCollisionWithPlayer = false;
        sceletonBulletScoreValue = false;
        enemyPhysics = gameObject.GetComponent<Rigidbody2D>();
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
        delayToShoot = Random.Range(0.5f, 1.0f);
        FindPosition();
    }

    void Update()
    {
        if (delayToShoot > 0)
        {
            delayToShoot -= Time.deltaTime;
        }

        CheckPlayerBulletScoreValue();

        if (BulletPos.transform.position.x > Enemy.transform.position.x)
        {
            bulletRelativePosition = true;
        }
        else { bulletRelativePosition = false; }

        if (delayToShoot <= 0.0f && !onCollisionWithPlayer && !sceletonBulletScoreValue && (Enemy.transform.position.y < Player.transform.position.y + 0.25f && Enemy.transform.position.y > Player.transform.position.y - 0.25f)) // if can shoot and bullet didn`t reach player and yPlayer == yEnemy
        {
            int randomize = Random.Range(1, 4);
            if (randomize == 2)
            {
                SpawnBullet();
            }
            else
            {
                delayToShoot = Random.Range(1.0f, 2.5f);
            }
        }
    }

    void FixedUpdate()
    {
        anim.SetBool("Grounded", grounded);         //anim
        anim.SetBool("RunSpeed", runSpeed);
        runSpeed = false;

        if (WallTriggered)
        {
            WallTriggered = false;
            FindPosition();
        }
        else
        {
            runSpeed = true;
            enemyPhysics.AddForce(vector * moveSpeed * airSpeed);      //sceleton runs
        }

            //if (ySceletonIsPlayer)
            //{
            //    delayToTryRun -= Time.deltaTime;
            //    if (Enemy.transform.position.x > Player.transform.position.x - 0.10f && Enemy.transform.position.x < Player.transform.position.x + 0.10f)
            //    {
            //        delayOfCheckY -= Time.deltaTime;
            //    }
            //    if (randomSideToGo == 0)
            //    {
            //        randomSideToGo = Random.Range(1, 3);
            //    }
            //    if (onCollisionWithPlayer)
            //    {
            //        ySceletonIsPlayer = false;
            //    }
            //}

            //if (delayToTryRun < 0)
            //{
            //    ySceletonIsPlayer = false;
            //    delayOfReturnRun -= Time.deltaTime;
            //    if (delayOfReturnRun < 0)
            //    {
            //        randomSideToGo = 0;
            //    }
            //}

            //if (!ySceletonIsPlayer)
            //{
            //    if (delayOfReturnRun > 0)
            //    {
            //        if (randomSideToGo == 1)
            //        {
            //            runSpeed = true;
            //            directionFlipManager = 1;
            //            enemyPhysics.AddForce(-vector * moveSpeed);
            //        }
            //        if (randomSideToGo == 2)
            //        {
            //            runSpeed = true;
            //            directionFlipManager = -1;
            //            enemyPhysics.AddForce(vector * moveSpeed);
            //        }
            //    }
            //    else
            //    {
            //        if ((((Enemy.transform.position.x < Player.transform.position.x) && !sceletonBulletScoreValue) || ((Enemy.transform.position.x > Player.transform.position.x) && ((sceletonBulletScoreValue && delayToRunBack <= 0) || onCollisionWithPlayer))))  //if xEnemy < xEnem and bullet didn`t reach player, or xEnemy > xEnem and bullet reached and "Wait"anim done or collided with player
            //        {
            //            runSpeed = true;
            //            directionFlipManager = 1;
            //            enemyPhysics.AddForce(-vector * moveSpeed);      //sceleton runs right
            //        }
            //        else if ((((Enemy.transform.position.x > Player.transform.position.x) && !sceletonBulletScoreValue) || ((Enemy.transform.position.x < Player.transform.position.x) && ((sceletonBulletScoreValue && delayToRunBack <= 0) || onCollisionWithPlayer))))  // opposite to above
            //        {
            //            runSpeed = true;
            //            directionFlipManager = -1;
            //            enemyPhysics.AddForce(vector * moveSpeed);            //sceleton runs left
            //        }
            //    }
            //}

        if (((Enemy.transform.position.y + 0.25 < Player.transform.position.y)) && grounded && !onCollisionWithPlayer)
        {
            StartCoroutine("JumpUp");         //jump try
        }

        if (destroyEnemyUnit)
        {
            Destroy(gameObject);            //destroy when out of range
            SpawnSystemScript.enemyExists--;
            PlayerBulletScript.destroyEnemyAfterColision = false;
            PlayerFireballScript.destroyEnemyAfterColision = false;
            WallTriggered = false;
            onCollisionWithPlayer = false;
        }   

        if (vector == new Vector3(-1, 0) && !facingRight)
        {
            Flip();
        }
        else if (vector == new Vector3(1, 0) && facingRight)
        {
            Flip();
        }

        if (enemyPhysics.velocity.magnitude > 8.0f)
        {
            enemyPhysics.velocity = enemyPhysics.velocity.normalized * 8.0f;
        }
    }

    void FindPosition()
    {
        if (Enemy.transform.position.x > Player.transform.position.x)
        {
            vector = new Vector3(-1, 0);
        }
        else
        {
            vector = new Vector3(1, 0);
        }
    }

    IEnumerator JumpUp()
    {
        for (int i = 100; i > 0; i--)
        {
            enemyPhysics.AddForce(Vector2.up * jumpHeight * (i / 100));
        }
        yield return null;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            airSpeed = 1;
            grounded = true;
        }
        if (col.gameObject.tag == "Player")
        {
            if (Enemy.transform.position.y > Player.transform.position.y)
            {
                StartCoroutine("JumpUp");
            }
            FindPosition();
            vector = -vector;
            onCollisionWithPlayer = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Area")
        {
            destroyEnemyUnit = false;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            airSpeed = 0.1f;
            grounded = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Area")
        {
            destroyEnemyUnit = true;
        }
    }

    void SpawnBullet()
    {
        Instantiate(Bullet, BulletPos.transform.position, Quaternion.identity);         //draw bullet
        delayToShoot = Random.Range(1.0f, 2.5f);
    }

    void CheckPlayerBulletScoreValue()
    {
        playerBulletScoreValue = PlayerBulletScript.destroyEnemyAfterColision;
        playerFireBallScoreValue = PlayerFireballScript.destroyEnemyAfterColision;
        if (playerBulletScoreValue == true)
        {
            destroyEnemyUnit = true;
        }
        if (playerFireBallScoreValue == true)
        {
            destroyEnemyUnit = true;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
