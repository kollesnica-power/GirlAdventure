using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenuMove : MonoBehaviour {

    [SerializeField]
    [Range(1.0f, 10.0f)]
    private float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.x >= 61.44f) {

            transform.position = new Vector3(0.0f, 0.0f, transform.position.z);

        } 

        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
