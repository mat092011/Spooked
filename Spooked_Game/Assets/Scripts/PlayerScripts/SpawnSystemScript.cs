using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystemScript : MonoBehaviour
{
	public static bool tutorial = true;
    public static int enemyExists;
    public static int bugExists;
    private GameObject enemyPosition;
    public GameObject EnemyPosition1;
    public GameObject EnemyPosition2;
    public GameObject EnemyPosition3;
    public GameObject EnemyPosition4;
    public GameObject Sceleton;
    public GameObject Ghost;
    public GameObject Worm;
    public GameObject Grasshopper;
    public GameObject Butterfly;
    public GameObject Bee;
    public GameObject Carterpilar;
    public GameObject Frog;
    public GameObject Dragonfly;
    public GameObject Fly;

	public bool canSpawn = true;

    void Update() {
		SpawnGameEnemy();
		if (tutorial) {
			SpawnWormTutorial();
		}
		if (!tutorial) {
			SpawnBug();
		}
        if (bugExists < 0) {
            bugExists = 0;
        }
    }

    void SpawnGameEnemy() {
        if (enemyExists == 0 && Movement.fail == false) {
			int SpawnPos = Random.Range(1, 5);
            switch (SpawnPos) {
                case 1: enemyPosition = EnemyPosition2; break;
                case 2: enemyPosition = EnemyPosition4; break;
                case 3: enemyPosition = EnemyPosition1; break;
                case 4: enemyPosition = EnemyPosition3; break;
            }
			Vector3 vector2 = new Vector3(enemyPosition.transform.position.x, enemyPosition.transform.position.y, enemyPosition.transform.position.z + 2);
			Vector3 vector = new Vector3(enemyPosition.transform.position.x, enemyPosition.transform.position.y, enemyPosition.transform.position.z);
			RaycastHit2D hit = Physics2D.Raycast(vector2, vector);
			if (Movement.runSpeed == false && (SpawnPos == 1 || SpawnPos == 2) && hit.collider == gameObject.GetComponent<Collider2D>()) {
				Instantiate(Sceleton, enemyPosition.transform.position, Quaternion.identity);
				enemyExists++;
			} else {
				Instantiate(Ghost, enemyPosition.transform.position, Quaternion.identity);
				enemyExists++;
			}
		}
    }

	void SpawnBug() {
		if (bugExists == 0) {
			switch (Random.Range(1, 5)) {
				case 1: enemyPosition = EnemyPosition1; break;
				case 2: enemyPosition = EnemyPosition2; break;
				case 3: enemyPosition = EnemyPosition3; break;
				case 4: enemyPosition = EnemyPosition4; break;
			}
			Vector3 vector2 = new Vector3(enemyPosition.transform.position.x, enemyPosition.transform.position.y, enemyPosition.transform.position.z + 2);
			Vector3 vector = new Vector3(enemyPosition.transform.position.x, enemyPosition.transform.position.y, enemyPosition.transform.position.z);
			RaycastHit2D hit = Physics2D.Raycast(vector2, vector);
			if (hit.collider == gameObject.GetComponent<Collider2D>()) {
				Instantiate(Worm, enemyPosition.transform.position, Quaternion.identity); bugExists++;
			}
			switch (Random.Range(0, 8)) {
				case 0: Instantiate(Worm, enemyPosition.transform.position, Quaternion.identity); bugExists++; break;
				case 1: Instantiate(Grasshopper, enemyPosition.transform.position, Quaternion.identity); bugExists++; break;
				case 2: Instantiate(Butterfly, enemyPosition.transform.position, Quaternion.identity); bugExists++; break;
				case 3: Instantiate(Bee, enemyPosition.transform.position, Quaternion.identity); bugExists++; break;
				case 4: Instantiate(Carterpilar, enemyPosition.transform.position, Quaternion.identity); bugExists++; break;
				case 5: Instantiate(Frog, enemyPosition.transform.position, Quaternion.identity); bugExists++; break;
				case 6: Instantiate(Dragonfly, enemyPosition.transform.position, Quaternion.identity); bugExists++; break;
				case 7: Instantiate(Fly, enemyPosition.transform.position, Quaternion.identity); bugExists++; break;
			}
		}
	}

	void SpawnWormTutorial() {
		if (bugExists == 0 && TutorialSystem.spawnWorm) {
			switch (Random.Range(1, 3)) {
				case 1: enemyPosition = EnemyPosition2; break;
				case 2: enemyPosition = EnemyPosition4; break;
			}
			Vector3 vector2 = new Vector3(enemyPosition.transform.position.x, enemyPosition.transform.position.y, enemyPosition.transform.position.z + 2);
			Vector3 vector = new Vector3(enemyPosition.transform.position.x, enemyPosition.transform.position.y, enemyPosition.transform.position.z);
			RaycastHit2D hit = Physics2D.Raycast(vector2, vector);
			if (hit.collider == gameObject.GetComponent<Collider2D>()) {
				switch (Random.Range(0, 3)) {
					case 0: Instantiate(Worm, enemyPosition.transform.position, Quaternion.identity); bugExists++; break;
					case 1: Instantiate(Grasshopper, enemyPosition.transform.position, Quaternion.identity); bugExists++; break;
					case 2: Instantiate(Bee, enemyPosition.transform.position, Quaternion.identity); bugExists++; break;
				}
			}
		}
	}
}
