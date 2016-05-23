using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	float speed = 5f;
	public float damage = 5f;

	public void SetSpeed(float _speed) {
		
		speed = _speed;

	}

	public void SetDamage(float _damage) {

		damage = _damage;

	}

	void Start () {

		SetDamage (0);

	}

	void Update () {

		transform.Translate (Vector3.forward * Time.deltaTime * speed);

	}
}
