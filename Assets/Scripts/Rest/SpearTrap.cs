using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTrap : MonoBehaviour {

    [SerializeField]
    private GameObject spearPrefab;
    [SerializeField]
    private Transform spearPosition;

    private Vector2 direction;

    // Use this for initialization
    void Start () {

        if ((transform.position.x - spearPosition.position.x) > 0) {
            direction = Vector2.right;
        } else {
            direction = Vector2.left;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Player")) {
            (Instantiate(spearPrefab, spearPosition.position, Quaternion.identity)).GetComponent<Missle>().Init(direction);
        }

    }
}
