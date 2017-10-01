using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGameUISpellDisplay : MonoBehaviour {

    private GameObject Player;
    public Text text;
    public static bool[] selectedSpell = new bool[10];
    private float delayToChange;
    private int selectedSpellUnit;
    private bool lightning = false;
    private bool fireball = false;
    private bool stun = false;
    private bool changeling = false;
    private bool letterSpell = false;
    private bool protectionSpell = false;
    private bool transportSpell = false;
    private bool revealSpell = false;
    private bool freezeSpell = false;
    private bool hallucinateSpell = false;
    private string lightningS;
    private string fireballS;
    private string stunS;
    private string changelingS;
    private string letterSpellS;
    private string protectionSpellS;
    private string transportSpellS;
    private string revealSpellS;
    private string freezeSpellS;
    private string hallucinateSpellS;

    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void FixedUpdate()
    {
        if (delayToChange > 0)
        {
            delayToChange -= Time.deltaTime;
        }
        ValueTake();
        UIText();
        if (delayToChange <= 0)
        {
            StringMake();
        }
        text.text = lightningS + fireballS + stunS + changelingS + letterSpellS + protectionSpellS + transportSpellS + revealSpellS + freezeSpellS + hallucinateSpellS;
    }

    void ValueTake()
    {
        if (Player.GetComponent<ActionScript>().lightning > 0)
        {
            lightning = true;
        } else { lightning = false; }
        if (Player.GetComponent<ActionScript>().fireball > 0)
        {
            fireball = true;
        } else { fireball = false; }
        if (Player.GetComponent<ActionScript>().stun > 0)
        {
            stun = true;
        } else { stun = false; }
        if (Player.GetComponent<ActionScript>().changeling > 0)
        {
            changeling = true;
        } else { changeling = false; }
        if (Player.GetComponent<ActionScript>().letterSpell > 0)
        {
            letterSpell = true;
        } else { letterSpell = false; }
        if (Player.GetComponent<ActionScript>().protectionSpell > 0)
        {
            protectionSpell = true;
        } else { protectionSpell = false; }
        if (Player.GetComponent<ActionScript>().transportSpell > 0)
        {
            transportSpell = true;
        } else { transportSpell = false; }
        if (Player.GetComponent<ActionScript>().revealSpell > 0)
        {
            revealSpell = true;
        } else { revealSpell = false; }
        if (Player.GetComponent<ActionScript>().freezeSpell > 0)
        {
            freezeSpell = true;
        } else { freezeSpell = false; }
        if (Player.GetComponent<ActionScript>().hallucinateSpell > 0)
        {
            hallucinateSpell = true;
        } else { hallucinateSpell = false; }
    }

    void UIText()
    {
        if (lightning == true)
        {
            lightningS = "<color=yellow>L</color>";
            if (selectedSpell[1] == true)
            {
                lightningS = "<b><color=yellow>L</color></b>";
            }
        }else { lightningS = null; }
        if (fireball == true)
        {
            fireballS = "<color=red>F</color>";
            if (selectedSpell[2] == true)
            {
                fireballS = "<b><color=red>F</color></b>";
            }
        }else { fireballS = null; }
        if (stun == true)
        {
            stunS = "<color=green>S</color>";
            if (selectedSpell[3] == true)
            {
                stunS = "<b><color=green>S</color></b>";
            }
        }else { stunS = null; }
        if (changeling == true)
        {
            changelingS = "<color=magenta>C</color>";
            if (selectedSpell[4] == true)
            {
                changelingS = "<b><color=magenta>C</color></b>";
            }
        }
        else { changelingS = null; }
        if (letterSpell == true)
        {
            letterSpellS = "<color=gray>Ls</color>";
            if (selectedSpell[5] == true)
            {
                letterSpellS = "<b><color=gray>Ls</color></b>";
            }
        }
        else { letterSpellS = null; }
        if (protectionSpell == true)
        {
            protectionSpellS = "<color=green>Ps</color>";
            if (selectedSpell[6] == true)
            {
                protectionSpellS = "<b><color=green>Ps</color></b>";
            }
        }
        else { protectionSpellS = null; }
        if (transportSpell == true)
        {
            transportSpellS = "<color=red>Ts</color>";
            if (selectedSpell[7] == true)
            {
                transportSpellS = "<b><color=red>Ts</color></b>";
            }
        }
        else { transportSpellS = null; }
        if (revealSpell == true)
        {
            revealSpellS = "<color=yellow>Rs</color>";
            if (selectedSpell[8] == true)
            {
                revealSpellS = "<b><color=yellow>Rs</color></b>";
            }
        }
        else { revealSpellS = null; }
        if (freezeSpell == true)
        {
            freezeSpellS = "<color=blue>Fs</color>";
            if (selectedSpell[9] == true)
            {
                freezeSpellS = "<b><color=blue>Fs</color></b>";
            }
        }
        else { freezeSpellS = null; }
        if (hallucinateSpell == true)
        {
            hallucinateSpellS = "<color=magenta>Hs</color>";
            if (selectedSpell[10] == true)
            {
                hallucinateSpellS = "<b><color=magenta>Hs</color></b>";
            }
        }
        else { hallucinateSpellS = null; }
    }

    void StringMake()
    {
        bool selectedSpellOpportunity = false;
        bool[] str = new bool[11];
        for (int i = 0; i < str.Length; i++)
        {
            switch(i)
            {
                case 0: str[i] = lightning; break;
                case 1: str[i] = fireball; break;
                case 2: str[i] = stun; break;
                case 3: str[i] = changeling; break;
                case 4: str[i] = letterSpell; break;
                case 5: str[i] = protectionSpell; break;
                case 6: str[i] = transportSpell; break;
                case 7: str[i] = revealSpell; break;
                case 8: str[i] = freezeSpell; break;
                case 9: str[i] = hallucinateSpell; break;
            }
        }
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == true)
            {
                selectedSpellOpportunity = true;
                break;
            }
        }
        if (selectedSpellOpportunity)
        {
            if (Input.GetKey(KeyCode.C))
            {
                delayToChange = 0.5f;
                selectedSpell[selectedSpellUnit] = false;
                if (selectedSpellUnit == 11)
                {
                    selectedSpellUnit = 0;
                }
                selectedSpellUnit++;
                while (str[selectedSpellUnit - 1] == false)
                {
                    if (selectedSpellUnit == 11)
                    {
                        selectedSpellUnit = 0;
                    }
                    selectedSpellUnit++;
                }
                selectedSpell[selectedSpellUnit] = true;
            }
        }
    }
}
