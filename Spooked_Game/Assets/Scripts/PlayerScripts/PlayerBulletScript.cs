using UnityEngine;
using System.Collections;

public class PlayerBulletScript : MonoBehaviour {

    public int Speed = 10;
    private GameObject PlayerBul;
    private Rigidbody2D rgb2;
    public static bool destroyEnemyAfterColision;
    public bool destroy = false;

    void Start () {
        destroyEnemyAfterColision = false;
        PlayerBul = gameObject;
        rgb2 = gameObject.GetComponent<Rigidbody2D>();
        bool bulletGoesRight = ActionScript.playerBulletRelativePosition;
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

    void Update()
    {
        if (destroy)
        {
            Destroy(gameObject);
            destroy = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            destroyEnemyAfterColision = true;
            Destroy(PlayerBul);                           //destroy after hitting player
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            destroyEnemyAfterColision = true;
            Destroy(PlayerBul);                           //destroy after hitting player
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Area")
        {
            Destroy(PlayerBul);                   //destroy after out of range
        }
    }

    void Flip()
    {
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
