using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    [SerializeField]
    private Transform[] backgrounds;
    [SerializeField]
    private float smoothing;

    private float[] parallaxingScales;
    private Vector3 previousCamPosition;

	// Use this for initialization
	void Start () {

        previousCamPosition = transform.position;

        parallaxingScales = new float[backgrounds.Length];

        for (int i = 0; i < parallaxingScales.Length; i++) {
            parallaxingScales[i] = backgrounds[i].position.z * -1;
        }
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

        for (int i = 0; i < backgrounds.Length; i++) {

            Vector3 parallax = (previousCamPosition - transform.position) * (parallaxingScales[i] / smoothing);

            backgrounds[i].position = new Vector3(backgrounds[i].position.x + parallax.x, backgrounds[i].position.y + parallax.y, backgrounds[i].position.z);

        }

        previousCamPosition = transform.position;

    }
}
