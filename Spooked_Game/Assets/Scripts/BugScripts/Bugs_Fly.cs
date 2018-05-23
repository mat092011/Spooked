using UnityEngine;
using System.Collections;

public class Bugs_Fly : MonoBehaviour 
{
    private float Speed;
    private Vector2 vectorOfMovement;
    private Rigidbody2D flyPhys;
    private float durOfMove;
    private float delayStop;
    private int look;
    private bool facingRight;

	void Start ()
    {
        flyPhys = gameObject.GetComponent<Rigidbody2D>();
        vectorOfMovement = new Vector2(Random.Range(-1.1f, 1.1f), Random.Range(-1.1f, 1.1f));
        Speed = Random.Range(4, 9);
        durOfMove = Random.Range(0.4f, 1.7f);
        delayStop = 1.2f ;
	}
	
	void Update ()
    {
        delayStop -= Time.deltaTime;
        if (delayStop > 0)
        {
            RandomMove();
        }
        else
        {
            flyPhys.velocity = Vector2.zero * Speed;
            if (delayStop < -2)
            {
                delayStop = Random.Range(2.5f, 4f);
            }
        }

        if (vectorOfMovement.x > 0)
        {
            look = 1;
        }
        else
        {
            look = -1;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
			SoundSystemScript.playBugCatchSound = true;
			GameObject.Find("Player").GetComponent<Movement>().flies++;
            SpawnSystemScript.bugExists--;
            Destroy(gameObject);
        }
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
        if (col.gameObject.tag == "Ground")
        {
            vectorOfMovement = -vectorOfMovement;
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

    void RandomMove()
    {
        durOfMove -= Time.deltaTime;
        if (durOfMove > 0)
        {
            flyPhys.velocity = vectorOfMovement * Speed;
        }
        else
        {
            vectorOfMovement = new Vector2(Random.Range(-1.1f, 1.1f), Random.Range(-1.1f, 1.1f));
            durOfMove = Random.Range(0.4f, 1.7f);
            Speed = Random.Range(4, 9);
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
