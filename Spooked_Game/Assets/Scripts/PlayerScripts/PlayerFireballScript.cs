using UnityEngine;
using System.Collections;

public class PlayerFireballScript : MonoBehaviour
{
    public int Speed = 10;
    private GameObject PlayerBul;
    private Rigidbody2D rgb2;
    private GameObject Enemy;
    //private int tempVector = 0;
    public static bool destroyEnemyAfterColision;
    //private bool vectorDirection;
    public bool destroy = false;

    void Start()
    {
        //vectorDirection = true;
        destroyEnemyAfterColision = false;
        PlayerBul = gameObject;
        rgb2 = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (Enemy != null)
        {
			gameObject.transform.position = Vector2.MoveTowards(transform.position, Enemy.transform.position, Speed * Time.deltaTime);
			//if (vectorDirection == true)
			//{
			//    if (tempVector == 0)
			//    {
			//        if (rgb2.transform.position.x > Enemy.transform.position.x)
			//        {
			//            tempVector = 1;
			//        }
			//        if (rgb2.transform.position.x < Enemy.transform.position.x)
			//        {
			//            tempVector = -1;
			//        }
			//    }
			//    if (tempVector == 1)
			//    {
			//        rgb2.velocity = Vector2.left * Speed;
			//        if (rgb2.transform.position.x < Enemy.transform.position.x)
			//        {
			//            vectorDirection = false;
			//            tempVector = 0;
			//        }
			//    }
			//    if (tempVector == -1)
			//    {
			//        rgb2.velocity = Vector2.right * Speed;
			//        if (rgb2.transform.position.x > Enemy.transform.position.x)
			//        {
			//            vectorDirection = false;
			//            tempVector = 0;
			//        }
			//    }
			//}
			//if (vectorDirection == false)
			//{
			//    if (tempVector == 0)
			//    {
			//        if (rgb2.transform.position.y > Enemy.transform.position.y)
			//        {
			//            tempVector = 1;
			//        }
			//        if (rgb2.transform.position.y < Enemy.transform.position.y)
			//        {
			//            tempVector = -1;
			//        }
			//    }
			//    if (tempVector == 1)
			//    {
			//        rgb2.velocity = Vector2.down * Speed;
			//        if (rgb2.transform.position.y < Enemy.transform.position.y)
			//        {
			//            vectorDirection = true;
			//            tempVector = 0;
			//        }
			//    }
			//    if (tempVector == -1)
			//    {
			//        rgb2.velocity = Vector2.up * Speed;
			//        if (rgb2.transform.position.y > Enemy.transform.position.y)
			//        {
			//            vectorDirection = true;
			//            tempVector = 0;
			//        }
			//    }
			//}
		} else {
            bool bulletGoesRight = ActionScript.playerBulletRelativePosition;
            if (bulletGoesRight == true) {
                rgb2.velocity = (Vector2.right * Speed);
            } else {                                                             //optional bullet flight
                rgb2.velocity = (Vector2.left * Speed);
            }
        }
        if (destroy) {
            Destroy(gameObject);
            destroy = false;
            //vectorDirection = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Enemy") {
			InputDetection.scared += 0.3f;
			if (GameObject.Find("Skeleton(Clone)") != null) {
				if (GameObject.FindGameObjectWithTag("Enemy").transform.position.x > gameObject.transform.position.x) {
					GameObject.Find("Skeleton(Clone)").GetComponent<SceletonMovement>().bulletRight = false;
				} else {
					GameObject.Find("Skeleton(Clone)").GetComponent<SceletonMovement>().bulletRight = true;
				}
			}
            destroyEnemyAfterColision = true;
            Destroy(PlayerBul);                           //destroy after hitting player
        }
    }
    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.tag == "Enemy") {
            destroyEnemyAfterColision = true;
            Destroy(PlayerBul);                           //destroy after hitting player
        }
    }
    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Area") {
            Destroy(PlayerBul);                   //destroy after out of range
        }
    }
}
