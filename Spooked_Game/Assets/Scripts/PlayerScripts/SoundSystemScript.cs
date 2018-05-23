using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystemScript : MonoBehaviour {

	private AudioSource audioSource;
	public GameObject Player;
	public AudioClip PauseMenuMusic;
	public AudioClip BugCatchSound;
	public AudioClip PotEnterSound;
	public static bool pauseMenuMusicStart = false;
	public static bool pauseMenuMusicStop = false;
	public static bool potEnterSound = false;
	public static bool playBugCatchSound = false;

	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	void Update () {
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
