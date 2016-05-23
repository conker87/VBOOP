using UnityEngine;
using System.Collections;
using System.Linq;

public class Projectile : MonoBehaviour {

	// Speed and Damage values.
	float speed = 1f, damage, lifetime = 8f;

	[SerializeField]
	float destroyingIn;

	int timeHits = 0;

	public void SetSpeed(float _speed) {
		
		speed = _speed;

	}

	public void SetDamage(float _damage) {

		damage = _damage;

	}

	/// <summary>
	/// Sets the projectile's lifetime in seconds.
	/// </summary>
	/// <param name="_lifetime">Lifetime in seconds.</param>
	public void SetLifetime(float _lifetime) {

		lifetime = _lifetime;

	}

	void Start () {

		Destroy (gameObject, lifetime);

		destroyingIn = lifetime;

	}

	void Update () {

		transform.Translate (Vector3.forward * Time.deltaTime * speed);

		// More of a DEBUG option really.
		destroyingIn -= Time.deltaTime;

	}

	void OnTriggerEnter(Collider other) {

		if (!(new []{ "Player", "Weapon" }.Contains (other.tag))) {

			timeHits++;


			Debug.Log ("This gameObject: " + gameObject.name + ", other: " + other.gameObject.name + ", timesHit: " + timeHits);

			Debug.Log ("Damage taken from projectile: " + damage);
		}

		//Destroy(other.gameObject);

	}

}