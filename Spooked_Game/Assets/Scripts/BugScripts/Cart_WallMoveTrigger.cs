using UnityEngine;
using System.Collections;

public class Cart_WallMoveTrigger : MonoBehaviour {

    public GameObject Cart;

    void Start() {
        Cart = GameObject.Find("Carteppilar(Clone)");
    }

    void Update() {

    }

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Untagged") {
			Cart.GetComponent<Bugs_Cart>().notFly = true;
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Untagged") {
			Cart.GetComponent<Bugs_Cart>().notFly = true;
		}
    }

    void OnTriggerExit2D(Collider2D col) {
        if (Cart.GetComponent<Bugs_Cart>().collision == true && (col.gameObject.tag == "Ground" || col.gameObject.tag == "Untagged")) {
            Cart.GetComponent<Bugs_Cart>().move = true;
        }
		if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Untagged") {
			Cart.GetComponent<Bugs_Cart>().notFly = false;
		}
    }
}
