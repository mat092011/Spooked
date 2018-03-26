using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

	public static bool jump = false;
    public float jumpHeight = 1000f;
    private Rigidbody2D playerPhysics;
	public Vector2 vector;
	private bool alJump;

    void Start () {
        playerPhysics = gameObject.GetComponent<Rigidbody2D>();
    }

	void Update () {
        if (GameObject.Find("Player").GetComponent<Movement>().grounded)
        {
            if (((Input.GetButton("Jump") || jump) && GameObject.Find("Player").GetComponent<Movement>().jumpAllowed && !Movement.fail))
            {
				alJump = true;
				//StartCoroutine("JumpUp");            //jump???
			}
        }
    }

	void FixedUpdate() {
		if (alJump) {
			playerPhysics.AddForce(vector * jumpHeight, ForceMode2D.Impulse);
			alJump = false;
		}
	}
}
