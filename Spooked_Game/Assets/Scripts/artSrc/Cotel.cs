using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cotel : MonoBehaviour {

    public bool isSleep = false;

    public SpriteRenderer body;
    public SpriteRenderer[] eye;
    public SpriteRenderer rot;
    public GameObject yazik;

    public Sprite[] rots;
    public Sprite[] bodys;
    public Sprite[] eyesWake;
    public Sprite[] eyesSleep;
    public int type = 0;

    private int count = 10;
    public int countColor = 0;


    private float[] partAlphaSpeed = new float[10];
    private float[] partSpeed = new float[10];
    private GameObject[] parts = new GameObject[10];
    public GameObject Light;
    public GameObject prefPart;
    private Color tmp;
    public float maxSpeed = 0.001f;
	void Start ()
    {
        if (type >= 4)
            Swap(type - 1);
        else
            Swap(type);
        initParts();
	}
	public void Swap(int color)
    {
        if(isSleep)
        {
            rot.sprite = rots[1];
            yazik.SetActive(false);
            for (int i = 0; i < 3; i++)
                eye[i].sprite = eyesSleep[color];

        }
        else
        {
            rot.sprite = rots[0];
            yazik.SetActive(true);
            for (int i = 0; i < 3; i++)
                eye[i].sprite = eyesWake[color];
        }
        body.sprite = bodys[color];
    }
	void Update ()
    {
		if( type == 4 )
        {
            if(count-- < 0)
            {
                count = 15;

                countColor++;
                if (countColor > 3)
                    countColor = 0;
                
                Swap(countColor);
                
            }
        }
        if (Light.active)
        {
            for (int i = 0; i < 10; i++)
            {
                parts[i].transform.Translate(0, partSpeed[i],0 );
                // parts[i].gameObject.GetComponent<SpriteRenderer>().color += new Color(255,255,255,-0.1f);
                tmp = parts[i].gameObject.GetComponent<SpriteRenderer>().color;
                tmp.a -= maxSpeed;
               
                //.a = 0.1f;
                if (parts[i].transform.localPosition.y> 1.1)
                {
                    parts[i].transform.localPosition = new Vector3(Random.Range(-0.707f, 0.01f), Random.Range(0.123f, 0.130f), 0);
                    partSpeed[i] = Random.Range(0.01f, 0.02f);
                    partAlphaSpeed[i] = partSpeed[i];
                    tmp.a = Random.Range(0.5f,1f);
                }
                parts[i].gameObject.GetComponent<SpriteRenderer>().color = tmp;
            }
        }
	}
    void initParts()
    {
        for(int i = 0; i < 10 ; i++)
        {
            GameObject part = Instantiate(prefPart);
            part.transform.SetParent(Light.transform);
            part.transform.localPosition = new Vector3(Random.Range(-0.707f,0.01f),Random.Range(0.123f,0.800f),0);
            parts[i] = part;
            partSpeed[i] = Random.Range(0.01f, 0.02f);
            partAlphaSpeed[i] = partSpeed[i];
        }
    }
}
