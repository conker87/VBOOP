using UnityEngine;
using System.Collections;
using System.Linq;

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

	void OnTriggerEnter(Collider other) {

		if (!(new []{ "Player", "Weapon" }.Contains (other.tag))) {
			Debug.Log ("This gameObject: " + gameObject.name + ", other: " + other.gameObject.name);


			Debug.Log ("Damage taken from projectile: " + damage);
		}

		//Destroy(other.gameObject);

	}

}