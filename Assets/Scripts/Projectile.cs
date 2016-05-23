using UnityEngine;
using System.Collections;
using System.Linq;

public class Projectile : MonoBehaviour {

	// Speed and Damage values.
	float speed = 1f, damage, currentDamage, lifetime = 8f;
	bool isPiercing = false, isFlaming = false, isFreezing = false;

	[SerializeField]
	float destroyingIn;

	int timesHit = 0;

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

		currentDamage = damage;

	}

	void Update () {

		transform.Translate (Vector3.forward * Time.deltaTime * speed);

		// More of a DEBUG option really.
		destroyingIn -= Time.deltaTime;

	}

	void OnTriggerEnter(Collider other) {

		if (!(new []{ "Player", "Weapon" }.Contains (other.tag))) {

			timesHit++;


			Debug.Log ("This gameObject: " + gameObject.name + ", other: " + other.gameObject.name + ", timesHit: " + timesHit + ", for damage: " + currentDamage);

			if (isPiercing && timesHit > 4) {

				Destroy (gameObject);

			}

			if (!isPiercing && timesHit > 1) {

				Destroy (gameObject);

			}

			currentDamage -= (damage / 4);
		}

		//Destroy(other.gameObject);

	}

}