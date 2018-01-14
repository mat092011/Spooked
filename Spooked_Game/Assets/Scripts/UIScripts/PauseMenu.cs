using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    private GameObject Player;
    public GameObject PauseUI;
	public GameObject PotMenu;
    public Text Bee;
    public Text ButterFly;
    public Text Cart;
    public Text DragonFLy;
    public Text Fly;
    public Text Frog;
    public Text GrassHop;
    public Text Worm;
	private float delayToExit;
    private bool paused = false;

	void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PauseUI.SetActive(false);
	}
	
	
	void Update ()
    {
		//if(Input.GetKey(KeyCode.Escape) && delayToExit <= 0 && !PotMenu.activeSelf)
  //      {
		//	if (!paused) {
		//		SoundSystemScript.pauseMenuMusicStart = true;
		//	}
		//	paused = !paused;

		//	delayToExit = 0.5f;
  //      }

        if (paused)
        {
			Bee.text = "<color=white>" + Player.GetComponent<Movement>().bees.ToString() + "</color>";
            ButterFly.text = "<color=white>" + Player.GetComponent<Movement>().butterflies.ToString() + "</color>";
            Cart.text = "<color=white>" + Player.GetComponent<Movement>().carterpilars.ToString() + "</color>";
            DragonFLy.text = "<color=white>" + Player.GetComponent<Movement>().dragonflies.ToString() + "</color>";
            Fly.text = "<color=white>" + Player.GetComponent<Movement>().flies.ToString() + "</color>";
            Frog.text = "<color=white>" + Player.GetComponent<Movement>().frogs.ToString() + "</color>";
            GrassHop.text = "<color=white>" + Player.GetComponent<Movement>().grasshoppers.ToString() + "</color>";
            Worm.text = "<color=white>" + Player.GetComponent<Movement>().worms.ToString() + "</color>";
            PauseUI.SetActive(true);
            Time.timeScale = 0;
		}

		if (delayToExit > 0) {
			delayToExit -= Time.unscaledDeltaTime;
		}

		if (!paused && delayToExit <= 0 && !PotMenu.activeSelf && PauseUI.activeSelf)
        {
			SoundSystemScript.pauseMenuMusicStop = true;
            PauseUI.SetActive(false);
            Time.timeScale = 1;
        }
	}

    public void Resume()
    {
        paused = !paused;
    }
}
