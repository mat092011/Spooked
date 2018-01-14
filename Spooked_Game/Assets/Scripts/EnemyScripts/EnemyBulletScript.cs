using UnityEngine;
using System.Collections;

public class EnemyBulletScript : MonoBehaviour {

    public int Speed = 10;
    private GameObject Bul;
    private Rigidbody2D rgb2;
    public static bool fail = false;

    void Start ()
    {
        fail = false;
        Bul = gameObject;
        rgb2 = gameObject.GetComponent<Rigidbody2D>();
        bool bulletGoesRight = SceletonMovement.bulletRelativePosition;
        if (bulletGoesRight == true)
        {
            rgb2.velocity = (Vector2.right * Speed);
        }                                                               //optional bullet flight
        else
        {
            Flip();
            rgb2.velocity = (Vector2.left * Speed);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Area")
        {
            Destroy(Bul);                   //destroy after out of range
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!GameObject.Find("Player").GetComponent<Movement>().protection)
            {
                fail = true;
				SceletonMovement.sceletonBulletScoreValue = true;
				//GameObject.Find("Clock").GetComponent<Clock>().time -= 100f;		MUST BE FIXED!!!
			}
            Destroy(Bul);                           //destroy after hitting player
        }
    }

    void Flip()
    {
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
