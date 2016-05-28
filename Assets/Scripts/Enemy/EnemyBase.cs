using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour {

	public Transform weaponLocation;

	public string nameof = "", title = "";

	public EnemySpecies species;
	public EnemyQuality quality;

	public float health, mana;
	[SerializeField]
	float currentHealth, currentMana;

	public WeaponBase startingWeapon;
	WeaponBase equippedWeapon;

	// enemyXPForCurrentLevel should be calculated depending on the Player current level, if the Player is more than [3] levels above the enemy should not give XP. 1/3 of the total XP should be removed per level above enemyCurrentLevel.
	public int currentLevel = 1, XPForCurrentLevel = 10;

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

	// Get methods
	public float GetCurrentHealth () {

		return currentHealth;

	}

	public float GetMaximumHealth() {

		return health;

	}

	public float GetCurrentMana() {

		return currentMana;

	}

	public float GetMaximumMana() {

		return mana;

	}

	// Set and Amend methods
	public void SetHealth(float _health) {

		currentHealth = _health;

	}

	/// <summary>
	/// Changes the health based upon the amount give.
	/// </summary>
	/// <param name="_health">The health to change by, can be negative or positive.</param>
	public void ChangeHealth(float _health) {

		currentHealth += _health;

	}

	public void SetMana(float _mana) {

		currentMana = _mana;

	}

	public void ChangeMana(float _mana) {

		currentMana += _mana;

	}

	public void CheckForDeath() {

		if (currentHealth <= 0) {

			Player.current.AmendCurrentXP (XPForCurrentLevel);

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
public enum EnemyQuality { WEAK, STARDARD, ELITE, BOSS };