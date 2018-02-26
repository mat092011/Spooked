using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cotel : MonoBehaviour {

    public bool isSleep = true;

	public SpriteRenderer body;
    public SpriteRenderer[] eye;
    public SpriteRenderer rot;
    public GameObject yazik;

    public Sprite[] rots;
    public Sprite[] bodys;
    public Sprite[] eyesWake;
    public Sprite[] eyesSleep;
	private Animator anim;
	public int type;
	public bool rainbow;


    private float[] partAlphaSpeed = new float[10];
    private float[] partSpeed = new float[10];
    private GameObject[] parts = new GameObject[10];
    public GameObject Light;
    public GameObject prefPart;
    private Color tmp;
    public float maxSpeed = 0.001f;

	void Start () {
		anim = gameObject.GetComponent<Animator>();
		anim.SetBool("Rainbow", rainbow);
		type--;
		Swap(type);
        initParts();
	}

	void Update () {
        if (Light.activeSelf) {
            for (int i = 0; i < 10; i++) {
                parts[i].transform.Translate(0, partSpeed[i],0 );
                parts[i].transform.Rotate( Vector3.right * 10 * Time.deltaTime );
                //parts[i].gameObject.GetComponent<SpriteRenderer>().color += new Color(255,255,255,-0.1f);
                tmp = parts[i].gameObject.GetComponent<SpriteRenderer>().color;
                tmp.a -= maxSpeed;
               
                //.a = 0.1f;
                if (parts[i].transform.localPosition.y> 1.1) {
                    parts[i].transform.localPosition = new Vector3(Random.Range(-0.707f, 0.01f), Random.Range(0.123f, 0.130f), 0);
                    partSpeed[i] = Random.Range(0.01f, 0.02f);
                    partAlphaSpeed[i] = partSpeed[i];
                    tmp.a = Random.Range(0.5f,1f);
                }
                parts[i].gameObject.GetComponent<SpriteRenderer>().color = tmp;
            }
        }
	}

	public void Swap(int type) {
		if (isSleep) {
			rot.sprite = rots[1];
			yazik.SetActive(false);
			for (int i = 0; i < 3; i++)
				eye[i].sprite = eyesSleep[type];

		} else {
			rot.sprite = rots[0];
			yazik.SetActive(true);
			for (int i = 0; i < 3; i++)
				eye[i].sprite = eyesWake[type];
		}
		body.sprite = bodys[type];
	}

	void initParts() {
        for(int i = 0; i < 10 ; i++) {
            GameObject part = Instantiate(prefPart);
            part.transform.SetParent(Light.transform);
            part.transform.localPosition = new Vector3(Random.Range(-0.707f,0.01f),Random.Range(0.123f,0.800f),0);
            parts[i] = part;
            partSpeed[i] = Random.Range(0.01f, 0.02f);
            partAlphaSpeed[i] = partSpeed[i];
        }
    }

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			isSleep = false;
			Swap(type);
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Player" && isSleep) {
			isSleep = false;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			isSleep = true;
			Swap(type);
		}
	}
}
