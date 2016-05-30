using UnityEngine;
using System.Collections;

public class Enemy : Entity {

	SharedFunctions sf = new SharedFunctions();

	public Transform weaponLocation;

	public EnemySpecies species;
	public EnemyQuality quality;

	public Weapon startingWeapon;
	Weapon equippedWeapon;

	const float levelMultiplierConstant = 10f;
	// currentLevel should be acquired from the Level singleton, as the player can choose what level they want to fight.
	public int currentLevel = 1 ;

	void Start ()
	{
		
		quality = sf.RandomEnumValue<EnemyQuality> ();

		if (startingWeapon != null) {
			EquipWeapon (startingWeapon);
		}

		currentHealth = maximumHealth = ScaleEnemyResourceToPlayer(baseHealth);
		currentMana = maximumMana = ScaleEnemyResourceToPlayer(baseMana);

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

	public void EquipWeapon(Weapon weaponToEquip) {

		if (equippedWeapon != null) {
			Destroy (equippedWeapon.gameObject);
		}

		equippedWeapon = Instantiate (weaponToEquip, weaponLocation.position, weaponLocation.rotation) as Weapon;
		equippedWeapon.transform.parent = weaponLocation;

	}

}

public enum EnemySpecies { UNDEAD, ABERRATION, BEAST, HUMAN, CRITTER, MISC };
public enum EnemyQuality { WEAK, STANDARD, ELITE, BOSS };