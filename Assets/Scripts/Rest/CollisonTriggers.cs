using UnityEngine;
using System.Collections;

public class CollisonTriggers : MonoBehaviour {

    public BoxCollider2D platformCollider;
    public BoxCollider2D platformTrigger;

    

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
            Physics2D.IgnoreCollision(platformCollider, other, true);
    }

    void OnTriggerExit2D(Collider2D other) {
            Physics2D.IgnoreCollision(platformCollider, other, false);
    }
}
