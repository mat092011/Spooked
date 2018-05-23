using UnityEngine;
using System.Collections;

public class Bugs_Base : MonoBehaviour {

	protected bool facingRight;

	protected void Flip()
	{
		facingRight = !facingRight;
		Vector3 Scale = transform.localScale;
		Scale.x *= -1;
		transform.localScale = Scale;
	}
}

