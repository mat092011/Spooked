using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpPart1 : MonoBehaviour
{
    // Use this for initialization
    public static int minSpeed = 1; 
    public int speed ;
    public float scSpeed;
	void Start ()
    {
        speed = Random.Range(120, 250);
        scSpeed = Random.Range (0.003f, 0.006f);
        speed *= -1;
        if (scSpeed < 0) scSpeed = 0.1f;
        transform.Rotate(Vector3.back * Random.Range(0,360));
        float sc = Random.Range(0.1f, 1.5f);
        transform.localScale = new Vector3(sc, sc, 1);
    }
	
	// Update is called once per frame
	void Update ()
    {
         transform.Rotate(Vector3.back * speed* Time.deltaTime);
         transform.localScale -= new Vector3(scSpeed, scSpeed, 1);
        if(transform.localScale.x <= 0)
        {
            speed = Random.Range(120, 250);
            scSpeed = Random.Range(0.003f, 0.006f);
            speed *= -1;
            transform.Rotate(Vector3.back * Random.Range(0, 360));
            float sc = Random.Range(0.8f, 1.5f);
            transform.localScale = new Vector3(sc, sc, 1);
        }
    }
}
