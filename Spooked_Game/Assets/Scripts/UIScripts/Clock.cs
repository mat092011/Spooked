using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour {

    public Texture2D fase1;
    public Texture2D fase2;
    public Texture2D fase3;
    public Texture2D fase4;
    public Texture2D fase5;
    public Texture2D fase6;
    public Texture2D fase7;
    public Texture2D fase8;
    public Texture2D fase9;
    public Texture2D fase10;
    public Texture2D fase11;
    public Texture2D fase12;
    public GameObject Hang;
    public GameObject imghang;
    public RawImage img;
    public Text txt;
    public Texture2D life2;
    public Texture2D life3;
    public Texture2D life4;
    public Texture2D life5;
    public Texture2D life6;
	public float time = 300.0f;
    public float delayToExit = 0;
    public int lifes = 6;
	
	void Update () {
        time -= Time.deltaTime;
        if (delayToExit > 0)
        {
            delayToExit -= Time.unscaledDeltaTime;
        }
        if (delayToExit <= 0 && Hang.activeSelf == true)
        {
            Hang.SetActive(false);
            Time.timeScale = 1;
        }
		if (delayToExit <= 0 && Hang.activeSelf == true && lifes == 0)
		{
			Application.Quit ();
		} 
        if (time > 275)
        {
            gameObject.GetComponent<RawImage>().texture = fase1;
        }
        if (time < 275 && time > 250)
        {
            gameObject.GetComponent<RawImage>().texture = fase2;
        }
        if (time < 250 && time > 225)
        {
            gameObject.GetComponent<RawImage>().texture = fase3;
        }
        if (time < 225 && time > 200)
        {
            gameObject.GetComponent<RawImage>().texture = fase4;
        }
        if (time < 200 && time > 175)
        {
            gameObject.GetComponent<RawImage>().texture = fase5;
        }
        if (time < 175 && time > 150)
        {
            gameObject.GetComponent<RawImage>().texture = fase6;
        }
        if (time < 150 && time > 125)
        {
            gameObject.GetComponent<RawImage>().texture = fase7;
        }
        if (time < 125 && time > 100)
        {
            gameObject.GetComponent<RawImage>().texture = fase8;
        }
        if (time < 100 && time > 75)
        {
            gameObject.GetComponent<RawImage>().texture = fase9;
        }
        if (time < 75 && time > 50)
        {
            gameObject.GetComponent<RawImage>().texture = fase10;
        }
        if (time < 50 && time > 25)
        {
            gameObject.GetComponent<RawImage>().texture = fase11;
        }
        if (time < 25 && time > 0)
        {
            gameObject.GetComponent<RawImage>().texture = fase12;
        }
        if (time < 0)
        {
            lifes--;
            if (lifes == 5)
            {
                imghang.SetActive(false);
            }
            if (lifes == 4)
            {
                imghang.SetActive(true);
                img.texture = life2;
                txt.text = "";
            }
            if (lifes == 3)
            {
                imghang.SetActive(true);
                img.texture = life3;
                txt.text = "";
            }
            if (lifes == 2)
            {
                imghang.SetActive(true);
                img.texture = life4;
                txt.text = "";
            }
            if (lifes == 1)
            {
                imghang.SetActive(true);
                img.texture = life5;
                txt.text = "";
            }
            if (lifes == 0)
            {
                imghang.SetActive(true);
                img.texture = life6;
                txt.text = "<color=red>H A N G E D !</color>";
            }
            Hang.SetActive(true);
            Time.timeScale = 0;
            delayToExit = 2.0f;
            time = 300;
        }
    }
}
