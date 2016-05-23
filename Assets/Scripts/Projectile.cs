using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	float speed;
	public float damage;

	public void SetSpeed(float _speed) {
		
		speed = _speed;

	}

	public void SetDamage(float _damage) {

		damage = _damage;

	}

	void Update () {

		transform.Translate (Vector3.forward * Time.deltaTime * speed);

	}
}
