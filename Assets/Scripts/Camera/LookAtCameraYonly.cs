using UnityEngine;
using System.Collections;

public class LookAtCameraYonly : MonoBehaviour
{
	public Camera cameraToLookAt;

	void Start() 
	{
		transform.Rotate(180,0,0);

		if (cameraToLookAt == null) {

			cameraToLookAt = Camera.main;

		}
	}

	void Update() 
	{
		Vector3 v = cameraToLookAt.transform.position - transform.position;
		v.y = v.z = 0.0f;
		transform.LookAt( cameraToLookAt.transform.position - v ); 
		transform.Rotate(-180,0,0);
	}
}