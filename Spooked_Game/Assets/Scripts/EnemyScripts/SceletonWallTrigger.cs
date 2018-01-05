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
			SceletonMovement.sceletonBulletScoreValue = false;
            GameObject.Find("Skeleton(Clone)").GetComponent<SceletonMovement>().onCollisionWithPlayer = false;
            GameObject.Find("Skeleton(Clone)").GetComponent<SceletonMovement>().WallTriggered = true;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Untagged")
        {
			SceletonMovement.sceletonBulletScoreValue = false;
            GameObject.Find("Skeleton(Clone)").GetComponent<SceletonMovement>().onCollisionWithPlayer = false;
            GameObject.Find("Skeleton(Clone)").GetComponent<SceletonMovement>().WallTriggered = true;
        }
    }
}
