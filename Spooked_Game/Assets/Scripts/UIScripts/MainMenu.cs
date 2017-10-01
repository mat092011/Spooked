using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	void Update () {
		if (Input.GetKey(KeyCode.Alpha1)) {
			SceneManager.LoadScene ("Spooked", LoadSceneMode.Single);
		}
		if (Input.GetKey(KeyCode.Alpha2)) {
			Application.Quit ();
		}
	}
}
