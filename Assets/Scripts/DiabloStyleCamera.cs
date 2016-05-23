using UnityEngine;
using System.Collections;

public class DiabloStyleCamera : MonoBehaviour {

	public GameObject player;
	public float offsetX = -5, offsetZ = 0;
	public float maximumDistance = 2f;
	public float playerVelocity = 10f;

	float movementX, movementZ;

	void Start() {
		
		transform.position = new Vector3(player.transform.position.x + offsetX, transform.position.y, player.transform.position.z + offsetZ);

	}

	// Update is called once per frame
	void FixedUpdate () {
	
		movementX = ( ( player.transform.position.x + offsetX - transform.position.x ) ) / maximumDistance;
		movementZ = ( ( player.transform.position.z + offsetZ - transform.position.z ) ) / maximumDistance;
		transform.position += new Vector3 (movementX * playerVelocity * Time.deltaTime, 0, movementZ * playerVelocity * Time.deltaTime);

	}
}
