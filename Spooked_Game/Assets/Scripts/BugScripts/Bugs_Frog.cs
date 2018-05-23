using UnityEngine;
using System.Collections;

public class Bugs_Frog : MonoBehaviour {

    public float jumpForce = 7;
    private float jumpTime;
    private float delayToJump = 0.3f;
    private float delayStop = 4.0f;
    public bool grounded;
    private int jumpRight;
    public GameObject Enemy;
    private GameObject Player;
    private Vector2 vectorOfMovement;
    Rigidbody2D frogPhysics;
    private Animator anim;
    private bool facingRight = true;
    private int look;

    void Start()
    {
        frogPhysics = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        anim.SetBool("Grounded", grounded);
        delayStop -= Time.deltaTime;
        if (delayStop > 0)
        {
            delayToJump -= Time.deltaTime;
            if ((Player.transform.position.x < gameObject.transform.position.x) && grounded && jumpRight == 0 && delayToJump < 0)
            {
                look = 1;
                jumpRight = 1;
                jumpTime = Random.Range(0.2f, 0.4f);
                vectorOfMovement = new Vector2(1, 1);
            }
            if ((Player.transform.position.x > gameObject.transform.position.x) && grounded && jumpRight == 0 && delayToJump < 0)
            {
                look = -1;
                jumpRight = -1;
                jumpTime = Random.Range(0.2f, 0.4f);
                vectorOfMovement = new Vector2(-1, 1);
            }
            if (jumpRight == 1)
            {
                jumpTime -= Time.deltaTime;
                Jump(vectorOfMovement);
            }
            if (jumpRight == -1)
            {
                jumpTime -= Time.deltaTime;
                Jump(vectorOfMovement);
            }
        }
        else
        {
            if (grounded)
            {
                frogPhysics.velocity = Vector2.zero * jumpForce;
            }
            if (delayStop < -2)
            {
                delayStop = Random.Range(2.5f, 4f);
            }
        }

        if (look < 0 && facingRight)
        {
            Flip();
        }
        else if (look > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Jump(Vector2 vectorOfMovement)
    {
        if (jumpTime > 0)
        {
            frogPhysics.velocity = vectorOfMovement * jumpForce;
        }
        else { jumpRight = 0; jumpTime = Random.Range(0.2f, 0.4f); }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
			SoundSystemScript.playBugCatchSound = true;
			GameObject.Find("Player").GetComponent<Movement>().frogs++;
            SpawnSystemScript.bugExists--;
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            Enemy = GameObject.FindGameObjectWithTag("Enemy");
            Physics2D.IgnoreCollision(Enemy.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
        }
        if (col.gameObject.tag == "Ground")
        {
            grounded = true;
            delayToJump = Random.Range(0.3f, 1.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BugArea")
        {
            delayStop = Random.Range(2.5f, 4f);
        }
        if (col.gameObject.tag == "PlayerBullet")
        {
            GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBulletScript>().destroy = true;
            SpawnSystemScript.bugExists--;
            Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Area")
        {
            SpawnSystemScript.bugExists--;
            Destroy(gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            grounded = false;
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
