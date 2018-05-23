using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BPotScript : MonoBehaviour
{
    private bool rec1 = false;
    private bool rec2 = false;
    private bool rec3 = false;
    private bool rec4 = false;
    private bool rec5 = false;
    private bool rec6 = false;
    private int emptySpace = 0;
    private bool menuActive;
    private bool[] recepiesValues = new bool[6];
    private GameObject[] objectDestroy = new GameObject[7];
    private int[] activeRec = new int[7];
	private GameObject Player;
    public GameObject PotMenu;
    public GameObject ReceptPlace1;
    public GameObject ReceptPlace2;
    public GameObject ReceptPlace3;
    public GameObject ReceptPlace4;
    public GameObject ReceptPlace5;
    public GameObject ReceptPlace6;
    public GameObject ReceptPlace7;
    public GameObject Recept1;
    public GameObject Recept2;
    public GameObject Recept3;
    public GameObject Recept4;
    public GameObject Recept5;
    public GameObject Recept6;
    public GameObject Recept7;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PotMenu.SetActive(false);
    }

    void Update()
    {
        if (menuActive == true)
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                KeyEvent(activeRec[0]);
                Exit();
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                KeyEvent(activeRec[1]);
                Exit();
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                KeyEvent(activeRec[2]);
                Exit();
            }
            if (Input.GetKey(KeyCode.Alpha4))
            {
                KeyEvent(activeRec[3]);
                Exit();
            }
            if (Input.GetKey(KeyCode.Alpha5))
            {
                KeyEvent(activeRec[4]);
                Exit();
            }
            if (Input.GetKey(KeyCode.Alpha6))
            {
                KeyEvent(activeRec[5]);
                Exit();
            }
            if (Input.GetKey(KeyCode.Alpha7) || Input.GetKey(KeyCode.Escape))
            {
                Exit();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == Player)
        {
			SoundSystemScript.potEnterSound = true;
            RecipeDraw();
            menuActive = true;
            PotMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void KeyEvent(int num)
    {
        switch (num)
        {
            case 1:
                if (recepiesValues[0] == true)
                {
                    Player.GetComponent<ActionScript>().freezeSpell += 10;
                    Player.GetComponent<Movement>().carterpilars -= 3;
                }
                else
                {
                    KeyEvent(2);
                }
                break;
            case 2:
                if (recepiesValues[1] == true)
                {
                    Player.GetComponent<ActionScript>().fireball += 10;
                    Player.GetComponent<Movement>().bees -= 3;
                }
                else
                {
                    KeyEvent(3);
                }
                break;
            case 3:
                if (recepiesValues[2] == true)
                {
                    Player.GetComponent<ActionScript>().letterSpell += 10;
                    Player.GetComponent<Movement>().flies -= 3;
                }
                else
                {
                    KeyEvent(4);
                }
                break;
            case 4:
                if (recepiesValues[3] == true)
                {
                    Player.GetComponent<ActionScript>().protectionSpell += 10;
                    Player.GetComponent<Movement>().worms -= 1;
                    Player.GetComponent<Movement>().frogs -= 1;
                    Player.GetComponent<Movement>().flies -= 1;
                }
                else
                {
                    KeyEvent(5);
                }
                break;
            case 5:
                if (recepiesValues[4] == true)
                {
                    Player.GetComponent<ActionScript>().transportSpell += 10;
                    Player.GetComponent<Movement>().dragonflies -= 3;
                }
                else
                {
                    KeyEvent(6);
                }
                break;
            case 6:
                if (recepiesValues[5] == true)
                {
                    Player.GetComponent<ActionScript>().hallucinateSpell += 10;
                    Player.GetComponent<Movement>().butterflies -= 3;
                }
                else
                {
                    KeyEvent(7);
                }
                break;
        }
    }

    void Exit()
    {
        for (int i = 0; i < objectDestroy.Length; i++)
        {
            Destroy(objectDestroy[i]);
        }
        PotMenu.SetActive(false);
        Time.timeScale = 1;
        menuActive = false;
    }

    void RecipeDraw()
    {
        if (Player.GetComponent<Movement>().carterpilars >= 3)
        {
            rec1 = true;
        }
        else { rec1 = false; }
        if (Player.GetComponent<Movement>().bees >= 3)
        {
            rec2 = true;
        }
        else { rec2 = false; }
        if (Player.GetComponent<Movement>().flies >= 3)
        {
            rec3 = true;
        }
        else { rec3 = false; }
        if (Player.GetComponent<Movement>().worms >= 1 && Player.GetComponent<Movement>().flies >= 1 && Player.GetComponent<Movement>().frogs >= 1)
        {
            rec4 = true;
        }
        else { rec4 = false; }
        if (Player.GetComponent<Movement>().dragonflies >= 3)
        {
            rec5 = true;
        }
        else { rec5 = false; }
        if (Player.GetComponent<Movement>().butterflies >= 3)
        {
            rec6 = true;
        }
        else { rec6 = false; }

        emptySpace = 0;
        recepiesValues = new bool[6] { rec1, rec2, rec3, rec4, rec5, rec6 };
        GameObject[] recepts = new GameObject[7] { Recept1, Recept2, Recept3, Recept4, Recept5, Recept6, Recept7 };
        GameObject[] receptsPlaces = new GameObject[7] { ReceptPlace1, ReceptPlace2, ReceptPlace3, ReceptPlace4, ReceptPlace5, ReceptPlace6, ReceptPlace7 };
        for (int i = 0; i < recepiesValues.Length; i++)
        {
            if (recepiesValues[i] == true)
            {
                GameObject clone;
                clone = Instantiate(recepts[i], receptsPlaces[i - emptySpace].transform.localPosition, Quaternion.identity);
                clone.transform.SetParent(PotMenu.transform, false);
                objectDestroy[i - emptySpace] = clone;
                activeRec[i - emptySpace] = i + 1;
            }
            else
            {
                emptySpace++;
            }
        }
        switch (Random.Range(0, 6))
        {
            case 0:
                Recept7.GetComponentInChildren<Text>().text = "<color=blue>Do nothing, what so ever</color>";
                break;
            case 1:
                Recept7.GetComponentInChildren<Text>().text = "<color=yellow>Do nothing, what so ever</color>";
                break;
            case 2:
                Recept7.GetComponentInChildren<Text>().text = "<color=red>Do nothing, what so ever</color>";
                break;
            case 3:
                Recept7.GetComponentInChildren<Text>().text = "<color=green>Do nothing, what so ever</color>";
                break;
            case 4:
                Recept7.GetComponentInChildren<Text>().text = "<color=magenta>Do nothing, what so ever</color>";
                break;
            case 5:
                Recept7.GetComponentInChildren<Text>().text = "<color=gray>Do nothing, what so ever</color>";
                break;
        }
        GameObject exit;
        exit = Instantiate(recepts[6], receptsPlaces[6 - emptySpace].transform.localPosition, Quaternion.identity);
        exit.transform.SetParent(PotMenu.transform, false);

        objectDestroy[6 - emptySpace] = exit;
    }
}
