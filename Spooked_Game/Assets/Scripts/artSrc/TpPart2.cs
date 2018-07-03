using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpPart2 : MonoBehaviour {

    public float scSpeed;
    public Transform insdObj;
    private float minX;

	void Start () {
        scSpeed = Random.Range(0.01f, 0.02f);
        insdObj.localPosition = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
        transform.Rotate(Vector3.back * Random.Range(0, 360));
        minX = Random.Range(0, 0.3f);
    }
	
	void Update () {
        transform.localScale -= new Vector3(scSpeed, scSpeed, 1);
        if (transform.localScale.x <= minX) {
            minX = Random.Range(0, 0.3f);
            float sc = Random.Range(0.8f, 1.5f);
            transform.localScale += new Vector3(sc, sc, 1);
            scSpeed = Random.Range(0.01f, 0.02f);
            insdObj.localPosition = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
            transform.Rotate(Vector3.back * Random.Range(0, 360)); 
        }
    }
}
