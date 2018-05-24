using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpPart2 : MonoBehaviour
{
    // Use this for initialization
    public float scSpeed;
    public Transform insdObj;
    private float minX;
	void Start ()
    {
        scSpeed = Random.Range(0.01f, 0.02f);
        insdObj.localPosition = new Vector3(Random.RandomRange(-1.0f, 1.0f), Random.RandomRange(-1.0f, 1.0f), 0);
        transform.Rotate(Vector3.back * Random.Range(0, 360));
        minX = Random.RandomRange(0, 0.3f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.localScale -= new Vector3(scSpeed, scSpeed, 1);
        if (transform.localScale.x <= minX)
        {
            minX = Random.RandomRange(0, 0.3f);
            float sc = Random.RandomRange(0.8f, 1.5f);
            transform.localScale += new Vector3(sc, sc, 1);
            scSpeed = Random.Range(0.01f, 0.02f);
            insdObj.localPosition = new Vector3(Random.RandomRange(-1.0f, 1.0f), Random.RandomRange(-1.0f, 1.0f), 0);
            transform.Rotate(Vector3.back * Random.Range(0, 360));
           
        }
    }
}
