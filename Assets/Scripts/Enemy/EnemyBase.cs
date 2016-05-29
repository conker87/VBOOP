using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour {

	SharedFunctions sf = new SharedFunctions();

	public Transform weaponLocation;

	public string nameof = "", title = "";

	public EnemySpecies species;
	public EnemyQuality quality;

	float baseHealth = 20, baseMana = 10;
	public float maxHealth, maxMana;
	[SerializeField]
	float currentHealth, currentMana;

	public WeaponBase startingWeapon;
	WeaponBase equippedWeapon;

	const float levelMultiplierConstant = 10f;
	// enemyXPForCurrentLevel should be calculated depending on the Player current level, if the Player is more than [3] levels above the enemy should not give XP. 1/3 of the total XP should be removed per level above enemyCurrentLevel.
	public int XPForCurrentLevel = 10;
	// currentLevel should be acquired from the Level singleton, as the player can choose what level they want to fight.
	public int currentLevel = 1 ;

	void Start ()
	{
		
		quality = sf.RandomEnumValue<EnemyQuality> ();

		if (startingWeapon != null) {
			EquipWeapon (startingWeapon);
		}

		currentHealth = maxHealth = ScaleEnemyResourceToPlayer(baseHealth);
		currentMana = maxMana = ScaleEnemyResourceToPlayer(baseMana);

	}

	void Update () {

		CheckForDeath ();
		ClampHealthAndMana ();

	}

	// Scaling Methods
	float ScaleEnemyResourceToPlayer(float baseResource) { 

		float qualityMultiplier = GetQualityMultiplier();
		float levelMultiplier = GetLevelMultiplier();

		return baseResource * qualityMultiplier * levelMultiplier;

	}

	float GetQualityMultiplier() {

		float qualityMultiplier;

		switch (quality) {

			case EnemyQuality.WEAK:
				qualityMultiplier = 0.7f;
				break;
			default:
			case EnemyQuality.STANDARD:
				qualityMultiplier = 1f;
				break;
			case EnemyQuality.ELITE:
				qualityMultiplier = 1.3f;
				break;
			case EnemyQuality.BOSS:
				qualityMultiplier = 1.75f;
				break;

		}

		return qualityMultiplier;

	}

	float GetLevelMultiplier() {

		float levelMultiplier;

		levelMultiplier = currentLevel * levelMultiplierConstant;

		return levelMultiplier;

	}

	// Get methods
	public float GetCurrentHealth () {

		return currentHealth;

	}

	public float GetMaximumHealth() {

		return maxHealth;

	}

	public float GetCurrentMana() {

		return currentMana;

	}

	public float GetMaximumMana() {

		return maxMana;

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

		if (currentMana > maxMana) {

			currentMana = maxMana;

		}

		if (currentHealth > maxHealth) {

			currentHealth = maxHealth;

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
public enum EnemyQuality { WEAK, STANDARD, ELITE, BOSS };