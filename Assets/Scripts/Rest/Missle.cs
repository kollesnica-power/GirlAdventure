using UnityEngine;
using System.Collections;


public class Missle : MonoBehaviour {

    public float speed;
    // time to live actually
    public float ttl;

    private Vector2 direction;
    private Rigidbody2D rigidbody2d;

    public void Init(Vector2 direction) {
        this.direction = direction;
    }

    // Use this for initialization
    void Start () {
        rigidbody2d = GetComponentInChildren<Rigidbody2D>();

        if (direction == Vector2.left) {

            transform.localScale = new Vector3(-1, 1, 1);

        }

    }
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject, ttl);
	}

    void FixedUpdate() {
        rigidbody2d.velocity = direction * speed;
    }
}
