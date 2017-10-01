using UnityEngine;
using System.Collections;

public class Bugs_ButterFly : Bugs_Base 
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
        Speed = Random.Range(6, 9);
        delayStop = Random.Range(0.2f, 0.5f);
        maxDelayStop = Random.Range(-0.8f, -0.4f);
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
                delayStop = Random.Range(0.2f, 0.5f);
            }

            flyPhys.AddForce(vectorOfMovement * Speed);
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
			GameObject.Find("Player").GetComponent<Movement>().butterflies++;
            SpawnSystemScript.bugExists--;
            Destroy(gameObject);
            onCollision = false;
        }
        if (col.gameObject.tag == "BugArea")
        {
            onCollision = true;
            delayStop = Random.Range(0.2f, 0.5f);
        }
        if (col.gameObject.tag == "PlayerBullet")
        {
            GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBulletScript>().destroy = true;
            SpawnSystemScript.bugExists--;
            Destroy(gameObject);
            onCollision = false;
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
            maxDelayStop = Random.Range(-0.8f, -0.4f);
            Speed = Random.Range(6, 9);
            int rand;
            if (onCollision == true)
            {
                rand = Random.Range(0, 2);
            }
            else { rand = Random.Range(0, 2); }

            switch (rand)
            {
                case 0: vectorOfMovement = new Vector2(0.2f, 1); break;
                case 1: vectorOfMovement = new Vector2(-0.2f, 1); break;
            }
        }
        flyPhys.AddForce(vectorOfMovement * Speed);
    }
/*
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }*/
}
