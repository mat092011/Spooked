using UnityEngine;
using System.Collections;

public class Bugs_Fly2 : MonoBehaviour
{
    private float Speed;
    private Vector2 vectorOfMovement;
    private Rigidbody2D flyPhys;
    private float delayStop;
    private float maxDelayStop;
    private int look;
    private bool facingRight;
    private bool onCollision = false;

    void Start()
    {
        vectorOfMovement = Vector2.zero;
        flyPhys = gameObject.GetComponent<Rigidbody2D>();
        Speed = Random.Range(3, 9);
        delayStop = Random.Range(0.3f, 0.6f);
        maxDelayStop = Random.Range(-1.5f, -0.3f);
    }

    void Update()
    {
        delayStop -= Time.deltaTime;
        if (delayStop > 0)
        {
            RandomMove();
        }
        else
        {
            if (vectorOfMovement != Vector2.zero)
            {
                vectorOfMovement = Vector2.zero;
            }
            if (delayStop < maxDelayStop)
            {
                delayStop = Random.Range(0.3f, 0.8f);
            }

            flyPhys.velocity = vectorOfMovement * Speed;
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
			GameObject.Find("Player").GetComponent<Movement>().dragonflies++;
            SpawnSystemScript.bugExists--;
            Destroy(gameObject);
            onCollision = false;
        }
        if (col.gameObject.tag == "BugArea")
        {
            onCollision = true;
            delayStop = Random.Range(0.3f, 0.8f);
        }
        if (col.gameObject.tag == "PlayerBullet")
        {
            GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBulletScript>().destroy = true;
            SpawnSystemScript.bugExists--;
            Destroy(gameObject);
            onCollision = false;
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
            onCollision = false;
        }
    }

    void RandomMove()
    {
        if (vectorOfMovement == Vector2.zero)
        {
            maxDelayStop = Random.Range(-1.5f, -0.3f);
            Speed = Random.Range(3, 9);
            int rand;
            if (onCollision == true)
            {
                rand = Random.Range(0, 6);
            }
            else { rand = Random.Range(0, 4); }

            switch(rand)
            {
                case 0: vectorOfMovement = new Vector2(1, 1); break;
                case 1: vectorOfMovement = new Vector2(1, -1); break;
                case 2: vectorOfMovement = new Vector2(-1, 1); break;
                case 3: vectorOfMovement = new Vector2(-1, -1); break;
                case 4: vectorOfMovement = new Vector2(-1, 0); break;
                case 5: vectorOfMovement = new Vector2(1, 0); break;
            }
        }
        flyPhys.velocity = vectorOfMovement * Speed;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
