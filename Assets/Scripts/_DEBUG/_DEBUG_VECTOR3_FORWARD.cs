using UnityEngine;
using System.Collections;

public class _DEBUG_VECTOR3_FORWARD : MonoBehaviour {

	public float speed = 5f;
	
	void Update () {
	
		transform.Translate (Vector3.forward * Time.deltaTime * speed);

	}
}
