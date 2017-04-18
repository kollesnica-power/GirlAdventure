using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour {

    [SerializeField]
    private Rigidbody2D myRigidbody;
    [SerializeField]
    private BoxCollider2D boxCollider;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float shakeTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Player")) {

            StartCoroutine(ShakeAndDrop());

        }

    }

    private IEnumerator ShakeAndDrop() {

        float timer = 0.0f;

        while (timer < shakeTime) {

            timer += 0.05f;
            transform.position = new Vector2(transform.position.x + 0.02f, transform.position.y);
            yield return new WaitForSeconds(0.05f);

            timer += 0.05f;
            transform.position = new Vector2(transform.position.x - 0.02f, transform.position.y);
            yield return new WaitForSeconds(0.05f);

            timer += 0.05f;
            transform.position = new Vector2(transform.position.x - 0.02f, transform.position.y);
            yield return new WaitForSeconds(0.05f);

            timer += 0.05f;
            transform.position = new Vector2(transform.position.x + 0.02f, transform.position.y);
            yield return new WaitForSeconds(0.05f);


        }

        myRigidbody.bodyType = RigidbodyType2D.Dynamic;
        boxCollider.enabled = false;

    }

}
