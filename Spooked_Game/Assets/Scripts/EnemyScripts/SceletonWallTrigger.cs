using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceletonWallTrigger : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Untagged")
        {
            EnemyMovement.sceletonBulletScoreValue = false;
            GameObject.Find("Skeleton(Clone)").GetComponent<EnemyMovement>().onCollisionWithPlayer = false;
            GameObject.Find("Skeleton(Clone)").GetComponent<EnemyMovement>().WallTriggered = true;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Untagged")
        {
            EnemyMovement.sceletonBulletScoreValue = false;
            GameObject.Find("Skeleton(Clone)").GetComponent<EnemyMovement>().onCollisionWithPlayer = false;
            GameObject.Find("Skeleton(Clone)").GetComponent<EnemyMovement>().WallTriggered = true;
        }
    }
}
