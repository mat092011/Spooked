using UnityEngine;
using System.Collections;

public class Cart_WallCollisionTrigger : MonoBehaviour {

    public GameObject Cart;

    void Start() {
        Cart = GameObject.Find("Carteppilar(Clone)");
    }

    void Update() {

    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Untagged") {
            Cart.GetComponent<Bugs_Cart>().collision = true;
        }
    }
}
