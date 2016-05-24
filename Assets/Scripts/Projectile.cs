﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class Projectile : MonoBehaviour {

	// Speed and Damage values.
	[SerializeField]
	float speed = 1f, damage, currentDamage, lifetime = 8f;
	[SerializeField]
	bool isPiercing, isBurning, isFreezing;

	public GameObject dmgIndicator;

	[SerializeField]
	float destroyingIn;

	int timesHit = 0, enemyID;

	public void SetSpeed(float _speed) {
		
		speed = _speed;

	}

	public void SetDamage(float _damage) {

		damage = _damage;

	}

	public void SetIsPiercing(bool _isPiercing) {

		isPiercing = _isPiercing;

	}

	public void SetIsBurning(bool _isBurning) {

		isBurning = _isBurning;

	}

	public void SetIsFreezing(bool _isFreezing) {

		isFreezing = _isFreezing;

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

		bool doDmg = false;

		if (timesHit == 0) {
			enemyID = other.transform.parent.gameObject.GetInstanceID ();
		}

		if (!(new []{ "Player", "Weapon", "Projectile" }.Contains (other.tag))) {

			timesHit++;

			/// We want to do damage only when the times hit is equal to 1 (which means this is the first enemy it has encountered),
			/// or if the times hit is more than 1 and that the enemy ID is no longer the same.
			if (timesHit == 1 || (timesHit > 1 && enemyID != other.transform.parent.gameObject.GetInstanceID ())) {

				enemyID = other.transform.parent.gameObject.GetInstanceID ();
				doDmg = true;

			}

			if (doDmg) {

				Debug.Log (gameObject.name + " hit " + other.transform.parent.gameObject.name + ", timesHit: " + timesHit + ", for damage: " + currentDamage);

				/// Temp code to instantiate a temp dmg indicator.
				//GameObject dmg = Instantiate (dmgIndicator, other.transform.position, other.transform.rotation) as GameObject;
				//dmg.GetComponentInChildren<TextMesh> ().text = currentDamage.ToString ();
				//dmg.transform.LookAt (Camera.main.transform);

				currentDamage -= (damage / 4);

				doDmg = false;

				/// Destroy the gameObject now as it's either not a piercing round or it is and it has hit more than 4 enemies.
				if (isPiercing == false || (isPiercing == true && timesHit == 4)) {

					Destroy (gameObject);

				}
			}
		}

	}

}