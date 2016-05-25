using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour {

	public Transform weaponLocation;

	public string nameofEnemy = "Test Enemy";

	public float health, mana;
	public WeaponBase startingWeapon;
	WeaponBase equippedWeapon;

	void Start ()
	{
		if (startingWeapon != null) {
			EquipWeapon (startingWeapon);
		}
	}

	public void SetHealth(float _health) {

		health = _health;

	}

	public void ChangeHealth(float _health) {

		health += _health;

	}

	public void ChangeHealth(float _health, bool showDebug) {

		float temp = health;

		health += _health;

		Debug.Log ("Previous/Current/Dmg: " + temp + "/" + health + "/" + _health);

	}

	public void SetMana(float _mana) {

		mana = _mana;

	}

	public void ChangeMana(float _mana) {

		mana += _mana;

	}

	void Update () {

		CheckForDeath ();

	}

	public void CheckForDeath() {

		if (health <= 0) {

			Destroy(gameObject);

		}

	}

	public void EquipWeapon(WeaponBase weaponToEquip) {

		if (equippedWeapon != null) {
			Destroy (equippedWeapon.gameObject);
		}

		equippedWeapon = Instantiate (weaponToEquip, weaponLocation.position, weaponLocation.rotation) as WeaponBase;
		equippedWeapon.transform.parent = weaponLocation;

	}

}

public enum EnemySpecies { UNDEAD, ABERRATION, BEAST, HUMAN, CRITTER, MISC };