using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	float speed = 5f;

	public void SetSpeed(float newSpeed) {
		
		speed = newSpeed;

	}

	void Update () {

		transform.Translate (Vector3.forward * Time.deltaTime * speed);

	}
}
