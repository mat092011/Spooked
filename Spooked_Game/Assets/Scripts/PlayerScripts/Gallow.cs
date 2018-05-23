using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gallow : MonoBehaviour {

	private GameObject Player;
	private GameObject Clone;
	private float delayToEnter = 0;
	private float delayOfAnim = 0;
	private int animCount = 0;
	private bool active = false;
	private bool delayExit = false;
	private bool win = false;
	private string str = null;
	private string[] word = new string[8];
    private string[] chosenWord = new string[5];
	private string[] word1 = new string[5]{"a", "n", "g", "e", "l"};
    private string[] word2 = new string[5]{"d", "e", "v", "i", "l"};
    private string[] word3 = new string[5]{"e", "a", "g", "l", "e"};
    private string[] word4 = new string[5]{"d", "e", "m", "o", "n"};
    private string[] word5 = new string[5]{"g", "r", "a", "v", "e"};
    private string[] word6 = new string[5]{"w", "i", "t", "c", "h"};
    private string[] word7 = new string[5]{"c", "r", "o", "w", "n"};
    private string[] word8 = new string[5]{"g", "h", "o", "s", "t"};
    private string[] word9 = new string[5]{"g", "h", "o", "u", "l"};
    private string[] word10 = new string[5]{"g", "i", "a", "n", "t"};
    public GameObject PotMenu;
	public GameObject Word;
	public Text text;
	public GameObject WordPlace;

	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
        switch(Random.Range(1, 11))
        {
            case 1: chosenWord = word1; break;
            case 2: chosenWord = word2; break;
            case 3: chosenWord = word3; break;
            case 4: chosenWord = word4; break;
            case 5: chosenWord = word5; break;
            case 6: chosenWord = word6; break;
            case 7: chosenWord = word7; break;
            case 8: chosenWord = word8; break;
            case 9: chosenWord = word9; break;
            case 10: chosenWord = word10; break;
        }
		for (int i = 0; i < word.Length; i++) {
			word[i] = "<color=red>_ </color>";
		}
	}
		
	void Update () {
		if (delayToEnter > 0) {
			delayToEnter -= Time.unscaledDeltaTime;
		}
		if (delayToEnter <= 0 && active)
		{
			if (delayExit) {
				if (text.text == "<color=red>" + chosenWord[0] + " </color><color=red" + chosenWord[1] + " </color><color=red>" + chosenWord[2] + " </color><color=red>" + chosenWord[3] + " </color><color=red>" + chosenWord[4] + " </color>") {
					delayToEnter = 0.5f;
					Destroy (Clone);
					text.text = "<color=red>You win!</color>";
					Clone = Instantiate (Word, WordPlace.transform.localPosition, Quaternion.identity);
					Clone.transform.SetParent (PotMenu.transform, false);
					delayToEnter = 4.0f;
					win = true;
					delayExit = true;
				} 
				else {
					if (win) {
						Application.Quit ();
					}
					Exit ();	
				}
			}
			if (Input.anyKeyDown) {
				str = Input.inputString;
				if (str != chosenWord[0] && str != chosenWord[1] && str != chosenWord[2] && str != chosenWord[3] && str != chosenWord[4]) {
					Destroy (Clone);
					text.text = "<color=red>No such letter!</color>";
					Clone = Instantiate (Word, WordPlace.transform.localPosition, Quaternion.identity);
					Clone.transform.SetParent (PotMenu.transform, false);
                    Player.GetComponent<ActionScript>().letterSpell -= 10;
                    delayToEnter = 0.5f;
					delayExit = true;
				} 
				else {
					Destroy (Clone);
					LetterCheck ();
					DrawWord ();
					delayToEnter = 0.5f;
					delayExit = true;
				}
			}
			if (Input.GetKey(KeyCode.Escape) && delayToEnter <= 0) {
				Exit ();
			}
		}
	}

	void Exit()
	{
		Destroy (Clone);
		delayExit = false;
		active = false;
		Time.timeScale = 1;
		PotMenu.SetActive (false);
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject == Player) {
			if (Input.GetKey(KeyCode.DownArrow) && Player.GetComponent<ActionScript>().letterSpell >= 10) {
				DrawWord ();
				delayToEnter = 0.5f;
				Time.timeScale = 0;
				active = true;
				PotMenu.SetActive (true);
			}
		}
	}

	void Animation(int objNum)
	{
		if (delayOfAnim > 0) {
			delayOfAnim -= Time.unscaledDeltaTime;
		}
		if (delayOfAnim <= 0 && animCount < 6) {
			Destroy (Clone);
			animCount++;
			switch (animCount) {
			case 1:
				word [objNum] = "<color=red>_ </color>";
				break;
			case 2:
				word [objNum] = "   ";
				break;
			case 3:
				word [objNum] = "<color=red>_ </color>";
				break;
			case 4:
				word [objNum] = "    ";
				break;
			case 5: 
				word [objNum] = "<color=red>_ </color>";
				break;
			}
			DrawWord ();
			delayOfAnim = 0.5f;
		}
	}

	void DrawWord()
	{
		text.text = word [0] + word [1] + word [2] + word [3] + word [4];
		Clone = Instantiate (Word, WordPlace.transform.localPosition, Quaternion.identity);
		Clone.transform.SetParent(PotMenu.transform, false);
	}

	void LetterCheck()
	{
		str = Input.inputString;
		for (int i = 0; i < word1.Length; i++) {
			if (str == chosenWord[i]) {
				word [i] = "<color=red>" + str + " </color>";
				delayToEnter = 0.5f;
			}
		}
	}
}
