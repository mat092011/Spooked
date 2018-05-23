using UnityEngine;
using System.Collections;

public class Bugs_Cart : MonoBehaviour
{
	public float speed = 6;
    public GameObject WallCollisionTrigger;
    private Vector2 vectorOfMovement;
    private Vector2 tempVector;
    private Rigidbody2D cartPhysics;
    private GameObject Enemy;
    private GameObject Player;
    public bool grounded = false;
    public bool collision = false;
	public bool notFly = false;
    public bool move = false;

    void Start()
    {
		cartPhysics = gameObject.GetComponent<Rigidbody2D>();
        PositionVector();
    }

    void Update()
    {
		if (notFly) {
			cartPhysics.velocity = vectorOfMovement * speed;
		}
        if (collision)
        {
            if (vectorOfMovement != Vector2.up)
            {
                if (vectorOfMovement == Vector2.left)
                {
                    transform.eulerAngles = new Vector3(0, 0, -90);
                }
                else { transform.eulerAngles = new Vector3(0, 0, 90); }
                cartPhysics.gravityScale = 0;
                tempVector = vectorOfMovement;
                vectorOfMovement = Vector2.up;
                move = false;
            }
        }
        if (move)
        {
            collision = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
            cartPhysics.gravityScale = 1;
            vectorOfMovement = tempVector;
            move = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
			SoundSystemScript.playBugCatchSound = true;
			GameObject.Find("Player").GetComponent<Movement>().carterpilars++;
            SpawnSystemScript.bugExists--;
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            Enemy = GameObject.FindGameObjectWithTag("Enemy");
            Physics2D.IgnoreCollision(Enemy.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
            SpawnSystemScript.bugExists--;
        }
        if (col.gameObject.tag == "BugArea")
        {
            vectorOfMovement = -vectorOfMovement;
            Flip();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Area")
        {
            Destroy(gameObject);
            SpawnSystemScript.bugExists--;
        }
    }

    void PositionVector()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player.transform.position.x > cartPhysics.position.x)
        {
            vectorOfMovement = Vector2.right;
            Flip();
        }
        else { vectorOfMovement = Vector2.left; }
    }

    void Flip()
    {
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
