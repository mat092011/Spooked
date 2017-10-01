using UnityEngine;
using System.Collections;


/*public class Bugs_Base : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			SoundSystemScript.playBugCatchSound = true;
			//GameObject.Find("Player").GetComponent<Movement>().bees++;
			RegisterBugKill();
			SpawnSystemScript.bugExists--;
			Destroy(gameObject);
		}
		if (col.gameObject.tag == "BugArea")
		{
			delayStop = Random.Range(0.3f, 0.8f);
		}
		if (col.gameObject.tag == "PlayerBullet")
		{
			GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBulletScript>().destroy = true;
			Destroy(gameObject);
			SpawnSystemScript.bugExists--;
		}
		if (col.gameObject.tag == "Ground")
		{
			vectorOfMovement = -vectorOfMovement;
		}
	}

	protected virtual void RegisterBugKill()
	{
	}
}*/

public class Bugs_Bee : Bugs_Base
{
    private Vector2 vectorOfMovement;
    private Vector2 tempVector;
    private Vector2 tempVectorX;
    private Vector2 tempVectorY;
    private Rigidbody2D flyPhys;
    private float Speed;
    private float delayStop;
    private float maxDelayStop;
    private float delayToShortMove;
    private int look;
    private int moveVar;
    private int moveCount;

    void Start()
    {
        delayToShortMove = 0;
        moveVar = Random.Range(0, 2);
        vectorOfMovement = Vector2.zero;
        flyPhys = gameObject.GetComponent<Rigidbody2D>();
        Speed = Random.Range(3, 9);
        delayStop = Random.Range(0.3f, 0.6f);
        maxDelayStop = Random.Range(-0.5f, -0.2f);
    }

	/*protected override void RegisterBugKill()
	{
		GameObject.Find("Player").GetComponent<Movement>().bees++;
	}*/

    void Update()
    {
        delayStop -= Time.deltaTime;
        if (delayStop > 0)
        {
            if (moveVar == 0)
            {
                RandomMoveVar1();
            }
            if (moveVar == 1 && moveCount < 10)
            {
                RandomMoveVar2();
            }
        }
        else
        {
            if (vectorOfMovement != Vector2.zero)
            {
                vectorOfMovement = Vector2.zero;
            }
            if (delayStop < maxDelayStop)
            {
                moveVar = Random.Range(0, 2);
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
            GameObject.Find("Player").GetComponent<Movement>().bees++;
            SpawnSystemScript.bugExists--;
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "BugArea")
        {
            delayStop = Random.Range(0.3f, 0.8f);
        }
        if (col.gameObject.tag == "PlayerBullet")
        {
			GameObject.FindGameObjectWithTag("PlayerBullet").GetComponent<PlayerBulletScript>().destroy = true;
            Destroy(gameObject);
            SpawnSystemScript.bugExists--;
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

    void RandomMoveVar1()
    {
        if (vectorOfMovement == Vector2.zero)
        {
            maxDelayStop = Random.Range(-0.5f, -0.2f);
            Speed = Random.Range(3, 9);
            int rand;
            rand = Random.Range(0, 6);

            switch (rand)
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

    void RandomMoveVar2()
    {
        if (delayToShortMove == 0)
        {
            int randX = Random.Range(0, 2);
            int randY = Random.Range(0, 2);
            switch (randX)
            {
                case 0: tempVectorX = Vector2.left; break;
                case 1: tempVectorX = Vector2.right; break;
            }
            switch (randY)
            {
                case 0: tempVectorY = Vector2.up; break;
                case 1: tempVectorY = Vector2.down; break;
            }
            tempVector = tempVectorX;
            delayToShortMove = 0.4f;
            delayStop = 2.0f;
        }
        delayToShortMove -= Time.deltaTime;
        if (delayToShortMove > 0)
        {
            vectorOfMovement = tempVector;
        }
        else
        {
            moveCount++;
            delayToShortMove = Random.Range(0.1f, 0.5f);
            delayStop = 0.7f;
            if (tempVector == tempVectorX)
            {
                tempVector = tempVectorY;
            }
            else { tempVector = tempVectorX; }
        }

        flyPhys.velocity = vectorOfMovement * Speed;
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
