using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour {

	public Transform weaponLocation;

	public string enemyName = "", enemyNameSub = "";

	public EnemySpecies enemySpecies;

	public float health, mana;
	[SerializeField]
	float currentHealth, currentMana;
	public WeaponBase startingWeapon;
	WeaponBase equippedWeapon;

	void Start ()
	{
		if (startingWeapon != null) {
			EquipWeapon (startingWeapon);
		}

		currentHealth = health;
		currentMana = mana;

	}

	void Update () {

		CheckForDeath ();
		ClampHealthAndMana ();

	}

	public void SetHealth(float _health) {

		currentHealth = _health;

	}

	public void ChangeHealth(float _health) {

		currentHealth += _health;

	}

	public void ChangeHealth(float _health, bool showDebug) {

		float temp = currentHealth;

		currentHealth += _health;

		Debug.Log ("Previous/Current/Dmg: " + temp + "/" + currentHealth + "/" + Mathf.Abs(_health));

	}

	public void SetMana(float _mana) {

		currentMana = _mana;

	}

	public void ChangeMana(float _mana) {

		currentMana += _mana;

	}

	public void CheckForDeath() {

		if (currentHealth <= 0) {

			Destroy(gameObject);

		}

	}

	public void ClampHealthAndMana() {

		if (currentMana <= 0) {

			currentMana = 0;

		}

		if (currentMana > mana) {

			currentMana = mana;

		}

		if (currentHealth > health) {

			currentHealth = health;

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