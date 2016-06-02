using UnityEngine;
using System.Collections;

public class Enemy : Entity {

	SharedFunctions sf = new SharedFunctions();

	public EnemySpecies species;
	public EnemyQuality quality;

	const float levelMultiplierConstant = 10f;

	protected override void Start ()
	{

		base.Start();

		quality = sf.RandomEnumValue<EnemyQuality> ();

		CurrentHealth = MaximumHealth;// = ScaleEnemyResourceToPlayer(BaseHealth);
		CurrentMana = MaximumMana;// = ScaleEnemyResourceToPlayer(BaseMana);

		CurrentLevel = 1;

	}

	protected override void Update () {

		base.Update();

		CheckForDeath();

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

		levelMultiplier = CurrentLevel * levelMultiplierConstant;

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

public enum EnemySpecies { ABERRATION, BEAST, CRITTER, DEITY, DEMON, ELEMENTAL, HUMANOID, UNDEAD };
public enum EnemyQuality { WEAK, STANDARD, ELITE, BOSS };