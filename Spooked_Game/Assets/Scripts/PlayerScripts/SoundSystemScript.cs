using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystemScript : MonoBehaviour {

	private AudioSource audioSource;
	public GameObject Player;
	public AudioClip PauseMenuMusic;
	public AudioClip BugCatchSound;
	public AudioClip PotEnterSound;
	public AudioSource backGround;
	public AudioClip cave;
	public AudioClip grass;
	public static bool pauseMenuMusicStart = false;
	public static bool pauseMenuMusicStop = false;
	public static bool potEnterSound = false;
	public static bool playBugCatchSound = false;

	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	void Update () {
		if (Player.GetComponent<ActionScript>().CurrentBiom == 1) {
			if (backGround.clip != cave) {
				backGround.Stop();
				backGround.clip = cave;
				backGround.Play();
			}
		}
		if (Player.GetComponent<ActionScript>().CurrentBiom == 2) {
			if (backGround.clip != grass) {
				backGround.Stop();
				backGround.clip = grass;
				backGround.Play();
			}
		}
		if (playBugCatchSound) {
			audioSource.clip = BugCatchSound;
			audioSource.Play();
			playBugCatchSound = false;
		}
		if (potEnterSound) {
			Player.GetComponent<AudioSource>().loop = false;
			audioSource.clip = PotEnterSound;
			audioSource.Play();
			potEnterSound = false;
		}
		if (pauseMenuMusicStart) {
			Player.GetComponent<AudioSource>().loop = false;
			audioSource.clip = PauseMenuMusic;
			audioSource.Play();
			pauseMenuMusicStart = false;
		}
		if (pauseMenuMusicStop) {
			audioSource.Stop();
			pauseMenuMusicStop = false;
		}
	}
}
