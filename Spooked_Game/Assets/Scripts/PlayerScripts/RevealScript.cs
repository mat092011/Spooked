using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RevealScript : MonoBehaviour {

    public GameObject LetterPlace;
    public GameObject PotMenu;
    public Text text;
    private float delay = 0;
    private GameObject Player;

    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update () {
        if (delay > 0)
        {
            delay -= Time.unscaledDeltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == Player)
        {
            if (Player.GetComponent<ActionScript>().revealSpell >= 10)
            {
                PotMenu.SetActive(true);
                Time.timeScale = 0;

                delay = 2.0f;
            }
        }
    }
}
