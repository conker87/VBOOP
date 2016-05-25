using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

	Vector3 velocity;
	Rigidbody myRigidbody;

	public Transform playerModel;

	void Start () {

		if (playerModel != null) {

			myRigidbody = GetComponent<Rigidbody> ();

		}

	}

	public void Move(Vector3 _velocity) {
		
		velocity = _velocity;

	}

	public void LookAt(Vector3 point) {

		Vector3 heightCorrection = new Vector3 (point.x, transform.position.y, point.z);

		playerModel.transform.LookAt (heightCorrection);
	}

	void FixedUpdate() {

		myRigidbody.MovePosition (myRigidbody.position + velocity * Time.fixedDeltaTime);

	}
}
