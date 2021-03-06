﻿using UnityEngine;
using System.Collections;
using EckTechGames.FloatingCombatText;

public abstract class Entity : MonoBehaviour {

	// Entity requirements
	EffectSlots entityEffectSlots;
	Item[] equipmentSlots = new Item[10];	// Helm, Chest, Hands, Legs, Feet, Weapon1, Weapon2, Relic1, Relic2, [AMMO?]

	// Temp Stats
	float[] tempFloat = new float[] { -1f, -1f, -1f };
	int[] tempInt = new int[] { -1, -1, -1 };

	// CONST
	int healthPerStamina = 10, manaPerIntellect = 10;
	public int HealthPerStamina	{ get {	return this.healthPerStamina; } }
	public int ManaPerIntellect	{ get {	return this.manaPerIntellect; } }

	// ********
	// * NAME *
	// ********
	// Name and Title
	//[SerializeField]
	public string entityName, entityTitle;
	public string EntityName	{ get {	return this.entityName; }		set {	this.entityName = value; } }
	public string EntityTitle	{ get {	return this.entityTitle; }		set {	this.entityTitle = value; } }

	// *****************
	// * HEALTH & MANA *
	// *****************
	// Base Health & Mana
	// Used to calculate health base upon level. These values should probably be taken from a class that stores these values.
	protected float baseHealth = 20, baseMana = 10;
	public float	BaseHealth		{ get {	return this.baseHealth; }		set {	this.baseHealth = value; } }
	public float 	BaseMana		{ get {	return this.baseMana; }			set {	this.baseMana = value; } }

	// Maximum Health & Mana
	// The maximum resources.
	// Can be changed via Equipment and Effects.
	[SerializeField]
	protected float maximumHealth, maximumMana;
	public float 	MaximumHealth		{ get {	return this.maximumHealth; }	set {	this.maximumHealth = value; } }
	public float 	MaximumMana		{ get {	return this.maximumMana; }		set {	this.maximumMana = value; } }

	// Current Health & Mana
	// These values are the Entity's realtime health and mana resources.
	[SerializeField]
	protected float currentHealth, currentMana;
	public float 	CurrentHealth		{ get {	return this.currentHealth; }	set {	this.currentHealth = value; } }
	public float 	CurrentMana		{ get {	return this.currentMana; }		set {	this.currentMana = value; } }

	// *********
	// * STATS *
	// *********
	// Stamina, Intellect & Armor Rating (yes I'm spelling it the American way because it looks less weird)
	// This is the amount of total health and the amount of total mana.
	// This value reduces physical damage (all damage is physical, bar those caused by BURNING, FREEZING, LEECHING and POISON).
	//		These *Ratings are going to be percentages, while they scale with level the values on the rest of the equipment does not.
	//		Except the ArmorRating, which requires it to be a value that increase with level, 
	[SerializeField]
	protected int stamina = 1, intellect = 1, armorRating = 0;
	protected float critRating = 5f, hasteRating = 0f;
	public int 		Stamina			{ get { return this.stamina;		} set { this.stamina = value;		} }
	public int 		Intellect		{ get { return this.intellect;		} set { this.intellect = value;		} }
	public int		ArmorRating		{ get { return this.armorRating;	} set { this.armorRating = value;	} }
	public float 	CritRating		{ get { return this.critRating; 	} set { this.critRating = value;	} }
	public float 	HasteRating		{ get { return this.hasteRating;	} set { this.hasteRating = value;	} }

	[SerializeField]
	protected float healthRegenerationPerSecond = 5f, manaRegenerationPerSecond = 5f, goldFind = 1f, experienceGain = 1f;
	public float 	HealthRegenerationPerSecond	{ get {	return this.healthRegenerationPerSecond; }	set { this.healthRegenerationPerSecond = value; } }
	public float 	ManaRegenerationPerSecond	{ get {	return this.manaRegenerationPerSecond; }	set { this.manaRegenerationPerSecond = value; } }
	public float 	IncreasedGoldFind			{ get {	return this.goldFind; }						set { this.goldFind = value; } }
	public float	IncreasedExperienceGain		{ get {	return this.experienceGain; }				set { this.experienceGain = value; } }

	[SerializeField]
	protected float increasedCritDamage = 1f, increasedAberrationDamage = 1f, increasedBeastDamage = 1f, increasedCritterDamage = 1f, increasedDemonDamage = 1f,
						increasedDeityDamage = 1f, increasedElementalDamage = 1f, increasedHumanoidDamage = 1, increasedUndeadDamage = 1f;
	public float 	IncreasedCritDamage			{ get {	return this.increasedCritDamage; }			set { this.increasedCritDamage = value; } }
	public float 	IncreasedAberrationDamage	{ get {	return this.increasedAberrationDamage; }	set { this.increasedAberrationDamage = value; } }	// Otherworldy
	public float	IncreasedBeastDamage		{ get {	return this.increasedBeastDamage; }			set { this.increasedBeastDamage = value; } }		// Animal
	public float	IncreasedCritterDamage		{ get {	return this.increasedCritterDamage; }		set { this.increasedCritterDamage = value; } }		// Animal
	public float 	IncreasedDemonDamage		{ get {	return this.increasedDemonDamage; }			set { this.increasedDemonDamage = value; } }		// Otherworldy
	public float 	IncreasedDeityDamage		{ get {	return this.increasedDeityDamage; }			set { this.increasedDeityDamage = value; } }		// Otherworldy
	public float 	IncreasedElementalDamage	{ get {	return this.increasedElementalDamage; }		set { this.increasedElementalDamage = value; } }	// Otherworldy
	public float 	IncreasedHumanoidDamage		{ get {	return this.increasedHumanoidDamage; }		set { this.increasedHumanoidDamage = value; } }		// Humanlike
	public float 	IncreasedUndeadDamage		{ get {	return this.increasedUndeadDamage; }		set { this.increasedUndeadDamage = value; } }		// Humanlike

	// ******************
	// * PARAGON POINTS *
	// ******************
	// These are added to once the player hits max level, they will not be called Paragon Points.
	// They will be used to purchase mats and better weapons.
	protected int 	paragonPoints, totalParagonPoints;
	protected float paragonPointsCritChance = 0f, paragonPointsCritDamage = 0f, paragonPointsArmorPercentage = 0f, paragonPointsHasteRating = 0f,
						paragonPointsAnimalDamage = 0f, paragonPointsOtherworldlyDamage = 0f, paragonPointsHumanlikeDamage = 0f,
						paragonPointsGoldAmount = 0f, paragonPointsExperience = 0f;
	public int 	 ParagonPoints						{ get {	return this.paragonPoints; }					protected set { this.paragonPoints = value; } }
	public int	 TotalParagonPoints					{ get {	return this.totalParagonPoints; }				protected set { this.totalParagonPoints = value; } }
	public float ParagonPointsArmorPercentage		{ get {	return this.paragonPointsArmorPercentage; }		set { this.paragonPointsArmorPercentage = value; } }
	public float ParagonPointsCritChance			{ get {	return this.paragonPointsCritChance; }			set { this.paragonPointsCritChance = value; } }
	public float ParagonPointsCritDamage			{ get {	return this.paragonPointsCritDamage; }			set { this.paragonPointsCritDamage = value; } }
	public float ParagonPointsAnimalDamage			{ get {	return this.paragonPointsAnimalDamage; }		set { this.paragonPointsAnimalDamage = value; } }
	public float ParagonPointsOtherworldlyDamage	{ get {	return this.paragonPointsOtherworldlyDamage; }	set { this.paragonPointsOtherworldlyDamage = value; } }
	public float ParagonPointsHumanlikeDamage		{ get {	return this.paragonPointsHumanlikeDamage; }		set { this.paragonPointsHumanlikeDamage = value; } }
	public float ParagonPointsGoldAmount			{ get {	return this.paragonPointsGoldAmount; }			set { this.paragonPointsGoldAmount = value; } }
	public float ParagonPointsExperience			{ get {	return this.paragonPointsExperience; }			set { this.paragonPointsExperience = value; } }

	[SerializeField]
	protected bool disableNaturalRegeneration = false;
	public bool DisableNaturalRegeneration	{ get {	return this.disableNaturalRegeneration; }	set { this.disableNaturalRegeneration = value; } }

	// **************
	// * EXPERIENCE *
	// **************
	// Experience needed to level at this current level.
	protected int totalExperienceNeededToLevel = 0;
	public int TotalExperienceNeededToLevel	{ get {	return this.totalExperienceNeededToLevel; }	protected set {	this.totalExperienceNeededToLevel = value; } }

	// The current level and the current experience value.
	protected int currentLevel = 1, currentExperience, maximumLevel = 100;
	public int CurrentLevel			{ get {	return this.currentLevel; }			protected set { this.currentLevel = value; } }
	public int CurrentExperience	{ get {	return this.currentExperience; }	protected set { this.currentExperience = value; } }

	// The experience given to the Player if this inherited entity is an Enemy.
	protected int	experienceGainedFromEntity = 10;
	public int		ExperienceGainedFromEntity	{ get {	return this.experienceGainedFromEntity; }	protected set {	this.experienceGainedFromEntity = value; } }

	// ***********
	// * WEAPONS *
	// ***********
	// The current equipped weapon and the weapon they start with (this is temp as the first weapon given will be generated randomly.
	public Weapon equippedWeapon, startingWeapon;
	public Weapon StartingWeapon	{ get {	return this.startingWeapon; }		set {	this.startingWeapon = value; } }
	public Weapon EquippedWeapon	{ get {	return this.equippedWeapon; }		set {	this.equippedWeapon = value; } }

	public Ammo startingAmmo, equippedAmmo;
	public Ammo StartingAmmo	{ get {	return this.startingAmmo; }		set {	this.startingAmmo = value; } }
	public Ammo EquippedAmmo	{ get {	return this.equippedAmmo; }		set {	this.equippedAmmo = value; } }

	public Transform weaponLocation;

	// Misc
	float nextRegenTime;

	// Temp Set Methods
	public void SetTempValue(int index, float value) {

		tempFloat [index] = value;

	}

	public void SetTempValue(int index, int value) {

		tempInt [index] = value;

	}

	public float GetTempFloatValue(int index) {

		return tempFloat [index];

	}

	public int GetTempValue(int index) {

		return tempInt [index];

	}

	// Health methods.
	public void Damage(float value, bool wasCrit = false) {
		
		CurrentHealth -= value;

		OverlayCanvasController.instance.ShowCombatText
							(gameObject, (wasCrit) ? CombatTextType.CriticalHit : CombatTextType.Hit, value.ToString ());
	
	}
	public void Heal(float value, bool wasCrit = false) {

		CurrentHealth += value;

		OverlayCanvasController.instance.ShowCombatText
							(gameObject, (wasCrit) ? CombatTextType.CriticalHeal : CombatTextType.Heal,	value.ToString ());

	}

	// Mana methods.
	public void DamageMana(float value, bool wasCrit = false)	{
		
		CurrentMana -= value;
		
		//OverlayCanvasController.instance.ShowCombatText
							//(gameObject, (wasCrit) ? CombatTextType.CriticalHitMana : CombatTextType.HitMana,	value.ToString ());

	}
	public void HealMana(float value, bool wasCrit = false)	{

		CurrentMana += value;

		//OverlayCanvasController.instance.ShowCombatText
							//(gameObject, (wasCrit) ? CombatTextType.CriticalHealMana : CombatTextType.HealMana, value.ToString ());
	
	}

	// (De)Buff stat methods.
	public void DebuffStat(EntityStat stat, int value)		{ BuffStat (stat, -value); }
	public void DebuffStat(EntityStat stat, float value)	{ BuffStat (stat, -value); }
	public void BuffStat(EntityStat stat, int value) {
		
		switch (stat) {

			case EntityStat.ARMOR_RATING:
				ArmorRating += value;
				break;

			case EntityStat.INTELLECT:
				Intellect += value;
				break;

			case EntityStat.STAMINA:
				Stamina += value;
				break;

		}

	}
	public void BuffStat(EntityStat stat, float value) {

		switch (stat) {

			case EntityStat.CRIT_RATING:
				CritRating += value;
				break;

			case EntityStat.HASTE_RATING:
				HasteRating += value;
				break;

			case EntityStat.HEALTH_REGENERATION:
				HealthRegenerationPerSecond += value;
				break;

			case EntityStat.INCREASED_ABERRATION:
				IncreasedAberrationDamage += value;
				break;

			case EntityStat.INCREASE_BEAST:
				IncreasedBeastDamage += value;
				break;

			case EntityStat.INCREASE_CRITTER:
				IncreasedCritterDamage += value;
				break;

			case EntityStat.INCREASE_DEITY:
				HealthRegenerationPerSecond += value;
				break;

			case EntityStat.INCREASE_DEMON:
				IncreasedDemonDamage += value;
				break;

			case EntityStat.INCREASE_ELEMENTAL:
				IncreasedElementalDamage += value;
				break;

			case EntityStat.INCREASE_HUMANOID:
				IncreasedHumanoidDamage += value;
				break;

			case EntityStat.INCREASE_UNDEAD:
				IncreasedUndeadDamage += value;
				break;

			case EntityStat.MANA_REGENERATION:
				ManaRegenerationPerSecond += value;
				break;

		}

	}

	public void CheckForDeath() {

		if (CurrentHealth <= 0) {

			if (gameObject.tag == "Enemy") {
				
				Player.current.DoExperienceAndLevelGain(Mathf.RoundToInt(ExperienceGainedFromEntity * Player.current.IncreasedExperienceGain));

			}

			Destroy(gameObject);

		}

	}

	void ClampHealthAndMana() {

		CurrentHealth 	= (float) System.Math.Round(Mathf.Clamp(CurrentHealth,	0f,	MaximumHealth), 2);
		CurrentMana 	= (float) System.Math.Round(Mathf.Clamp(CurrentMana,	0f, MaximumMana), 2);

	}

	void CalculateMaximumResources() {

		// TODO: This also needs to incorporate Health & Mana given through ...of Foritude.
		// The way we calculate this is by adding all resource gained from equipment to the commented out var, making sure to reset this
		//		value every time a piece of equipment is removed so that it does not stack.

		MaximumHealth	= (HealthPerStamina * Stamina)		+ BaseHealth;	// + TotalHealthGainedFromItems
		MaximumMana		= (ManaPerIntellect * Intellect)	+ BaseMana; 	// + TotalManaGainedFromItems

	}

	void DoHealthAndManaRegen() {

		if (Time.time > nextRegenTime) {

			nextRegenTime = Time.time + 1f;

			CurrentHealth	+= HealthRegenerationPerSecond;
			CurrentMana		+= ManaRegenerationPerSecond;

		}

	}

	protected virtual void Start() {

		entityEffectSlots = GetComponent<EffectSlots> ();

		if (StartingWeapon != null) {
			WeaponController.EquipWeapon (this, weaponLocation, StartingWeapon);
		}

		// Clamps the Health and Mana values to 0f and to their maximum.
		ClampHealthAndMana();

		// Makes sure that the maximum Health & Mana is always the amount given through Stamina.
		CalculateMaximumResources();

		// As this Entity has just spawned, the Current Health & Mana should be set to full
		// Note that this should not happen to the Player during normal gameplay.
		CurrentHealth = MaximumHealth;
		CurrentMana = MaximumMana;

	}

	protected virtual void Update() {

		// Makes sure that the maximum Health & Mana is always the amount given through Stamina.
		CalculateMaximumResources();

		// Does the natural Current Health & Mana regeneration per second, this can be increased via equipment.
		if (DisableNaturalRegeneration == false) {
		
			DoHealthAndManaRegen ();

		}

		// Clamps the Health and Mana values to 0f and to their maximum.
		ClampHealthAndMana();

	}

	public int GetStatValueOnEnum(EntityStat stat) {

		return 0;

	}

}

public enum EntityStat { ARMOR_RATING, CRIT_RATING, HASTE_RATING, HEALTH_REGENERATION,
							INCREASED_ABERRATION, INCREASE_BEAST, INCREASE_CRITTER, INCREASE_DEITY, INCREASE_DEMON, INCREASE_ELEMENTAL, INCREASE_HUMANOID, INCREASE_UNDEAD,
							INTELLECT, MANA_REGENERATION, STAMINA };

// ABERRATION, BEAST, CRITTER, DEITY, DEMON, ELEMENTAL, HUMANOID, UNDEAD