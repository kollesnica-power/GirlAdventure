using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, minX, maxX),
            Mathf.Clamp(player.transform.position.y, minY, maxY), transform.position.z);
	}
}
