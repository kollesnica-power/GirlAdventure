﻿using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    [SerializeField]
    private EnemyController enemy;

	void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Player") {
            enemy.target = other.gameObject;
        }

    }

    void OnTriggerExit2D(Collider2D other) {

        if (other.tag == "Player") {
            enemy.target = null;
        }

    }

}
