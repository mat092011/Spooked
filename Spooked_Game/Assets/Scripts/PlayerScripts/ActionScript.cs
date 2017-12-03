using UnityEngine;
using System.Collections;

public class ActionScript : MonoBehaviour {

    public GameObject PlayerBulletPosition;
    public GameObject PlayerBullet;
    public GameObject PlayerFireball;
    private GameObject Player;
    private Vector3 PlayerTransportPosition = new Vector3(0, 0);
    private bool[] selectedSpell = new bool[10];
    private float delayToShoot = 0f;
	public bool fire = false;
    public static bool playerBulletRelativePosition;
    public int lightning;
    public int fireball;
    public int stun;
    public int changeling;
    public int letterSpell;
    public int protectionSpell;
    public int transportSpell;
    public int revealSpell;
    public int freezeSpell;
    public int hallucinateSpell;

    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void FixedUpdate ()
    {
        if (PlayerBulletPosition.transform.position.x > Player.transform.position.x)
        {
            playerBulletRelativePosition = true;
        }
        else { playerBulletRelativePosition = false; }
        if ((Input.GetKey(KeyCode.F) || fire) && !Movement.fail)
        {
            selectedSpell = InGameUISpellDisplay.selectedSpell;
            if (delayToShoot <= 0.0f && lightning > 0 && selectedSpell[1] == true)
            {
                Instantiate(PlayerBullet, PlayerBulletPosition.transform.position, Quaternion.identity);
                delayToShoot = 0.5f;
            }
            if (delayToShoot <= 0.0f && fireball > 0 && selectedSpell[2] == true)
            {
                Instantiate(PlayerFireball, PlayerBulletPosition.transform.position, Quaternion.identity);
				fireball--;
                delayToShoot = 0.5f;
            }
            if (delayToShoot <= 0.0f && stun > 0 && selectedSpell[3] == true)
            {

            }
            if (delayToShoot <= 0.0f && changeling > 0 && selectedSpell[4] == true)
            {

            }
            if (delayToShoot <= 0.0f && letterSpell > 0 && selectedSpell[5] == true)
            {

            }
            if (delayToShoot <= 0.0f && protectionSpell > 0 && selectedSpell[6] == true)
            {
                Player.GetComponent<Movement>().protection = true;
                Player.GetComponent<Movement>().protectionDur = 35.0f;
                delayToShoot = 0.5f;
            }
            if (delayToShoot <= 0.0f && transportSpell > 0 && selectedSpell[7] == true)
            {
                if (PlayerTransportPosition == new Vector3(0,0))
                {
					transportSpell -= 5;
                    PlayerTransportPosition = Player.transform.localPosition;
                    delayToShoot = 0.5f;
                }
                else
                {
					transportSpell -= 5;
					Player.transform.localPosition = PlayerTransportPosition;
                    PlayerTransportPosition = new Vector3(0, 0);
                    delayToShoot = 0.5f;
                }
            }
            if (delayToShoot <= 0.0f && revealSpell > 0 && selectedSpell[8] == true)
            {

            }
            if (delayToShoot <= 0.0f && freezeSpell > 0 && selectedSpell[9] == true)
            {

            }
            if (delayToShoot <= 0.0f && hallucinateSpell > 0 && selectedSpell[10] == true)
            {

            }
        }
    }

    void Update()
    {
        delayToShoot -= Time.deltaTime;
    }
}
