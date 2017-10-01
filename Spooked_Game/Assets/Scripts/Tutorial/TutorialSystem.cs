using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSystem : MonoBehaviour {

	public static bool tutorial;
	public GameObject Worm;
	public GameObject Skeleton;
	public GameObject Ghost;
	public GameObject tutorialNPC;
	public GameObject cauldronSpawnPoint;
	public GameObject Cauldron;
	public static GameObject wormSpawnPoint;
	public static bool spawnWorm = true;
	public static bool spawnSkeleton;
	public static bool spawnGhost;
	public static bool Trigger1 = false;
	public GameObject NPCSpawnPosition1;

	void Start () {
		
	}
	
	void Update () {
		//if (tutorial) {
		//	if (Trigger1) {
		//		if (GameObject.Find("Player").GetComponent<Movement>().worms < 2) {
		//			Instantiate(tutorialNPC, NPCSpawnPosition1.transform.position, Quaternion.identity);
		//		}
		//	}
		//}
	}
}
