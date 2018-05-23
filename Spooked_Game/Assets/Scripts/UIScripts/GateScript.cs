using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour {

    public GameObject WordPlace;
    public GameObject WordText;
    public GameObject PotMenu;
    private GameObject Player;
    private float delay = 0;
    public GameObject connectedPortal;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (delay > 0)
        {
            delay -= Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject == Player)
        {
            if (Input.GetKey(KeyCode.DownArrow) && delay <= 0 && Player.GetComponent<ActionScript>().letterSpell >= 10)
            {
                PotMenu.SetActive(true);
                Time.timeScale = 0;
                
                delay = 2.0f;
            }
        }
    }
}
