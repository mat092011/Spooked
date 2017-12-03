using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

	public static bool jump = false;
    public float jumpHeight = 1000f;
    private Rigidbody2D playerPhysics;

    void Start () {
        playerPhysics = gameObject.GetComponent<Rigidbody2D>();
    }

	void Update () {
        if (GameObject.Find("Player").GetComponent<Movement>().grounded)
        {
            if (((Input.GetButtonDown("Jump") || jump) && !Movement.fail))
            {
                StartCoroutine("JumpUp");            //jump???
            }
        }
    }

    IEnumerator JumpUp()
    {
        for (int i = 100; i > 0; i--)
        {
            playerPhysics.AddForce(Vector2.up * jumpHeight * (i / 100));
        }
        yield return null;
    }
}
