using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour {

	EffectSlots entityEffectSlots;

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
	public float BaseHealth		{ get {	return this.baseHealth; }		set {	this.baseHealth = value; } }
	public float BaseMana		{ get {	return this.baseMana; }			set {	this.baseMana = value; } }

	// Maximum Health & Mana
	// The maximum resources.
	// Can be changed via Equipment and Effects.
	[SerializeField]
	protected float maximumHealth, maximumMana;
	public float MaximumHealth		{ get {	return this.maximumHealth; }	set {	this.maximumHealth = value; } }
	public float MaximumMana		{ get {	return this.maximumMana; }		set {	this.maximumMana = value; } }

	// Current Health & Mana
	// These values are the Entity's realtime health and mana resources.
	[SerializeField]
	protected float currentHealth, currentMana;
	public float CurrentHealth		{ get {	return this.currentHealth; }	set {	this.currentHealth = value; } }
	public float CurrentMana		{ get {	return this.currentMana; }		set {	this.currentMana = value; } }

	// *********
	// * STATS *
	// *********
	// Stamina, Intellect & Armor Rating (yes I'm spelling it the American way because it looks less weird)
	// This is the amount of total health, the amount of total mana and also adds to elemental damage (possibly).
	// This value reduces physical damage (all damage is physical, bar those caused by Burning & Freezing).
	// TODO: Should this be a percentage or an actual value that increases? Percentages scale with levels, values do not.
	[SerializeField]
	protected int stamina = 1, intellect = 1, armorRating = 0, critRating = 1, hasteRating = 1;
	public int Stamina						{ get { return this.stamina; }		set { this.stamina = value; } }
	public int Intellect					{ get { return this.intellect; }	set { this.intellect = value; } }
	public int ArmorRating					{ get { return this.armorRating; }	set { this.armorRating = value; } }
	public int CritRating					{ get { return this.critRating; }	set { this.critRating = value; } }
	public int HasteRating 					{ get { return this.hasteRating; }	set { this.hasteRating = value; } }

	[SerializeField]
	protected float healthRegenerationPerSecond = 5f, manaRegenerationPerSecond = 5f, goldFind = 1f, experienceGain = 1f;
	public float HealthRegenerationPerSecond	{ get {	return this.healthRegenerationPerSecond; }	set { this.healthRegenerationPerSecond = value; } }
	public float ManaRegenerationPerSecond		{ get {	return this.manaRegenerationPerSecond; }	set { this.manaRegenerationPerSecond = value; } }
	public float GoldFind						{ get {	return this.goldFind; }						set { this.goldFind = value; } }
	public float ExperienceGain					{ get {	return this.experienceGain; }				set { this.experienceGain = value; } }

	[SerializeField]
	protected float increasedCritDamage = 1f, increasedAberrationDamage = 1f, increasedBeastDamage = 1f, increasedCritterDamage = 1f, increasedDemonDamage = 1f,
						increasedDeityDamage = 1f, increasedElementalDamage = 1f, increasedHumanoidDamage = 1, increasedUndeadDamage = 1f;
	public float IncreasedCritDamage		{ get {	return this.increasedCritDamage; }			set { this.increasedCritDamage = value; } }
	public float IncreasedAberrationDamage	{ get {	return this.increasedAberrationDamage; }	set { this.increasedAberrationDamage = value; } }	// Otherworldy
	public float IncreasedBeastDamage		{ get {	return this.increasedBeastDamage; }			set { this.increasedBeastDamage = value; } }		// Animal
	public float IncreasedCritterDamage		{ get {	return this.increasedCritterDamage; }		set { this.increasedCritterDamage = value; } }		// Animal
	public float IncreasedDemonDamage		{ get {	return this.increasedDemonDamage; }			set { this.increasedDemonDamage = value; } }		// Otherworldy
	public float IncreasedDeityDamage		{ get {	return this.increasedDeityDamage; }			set { this.increasedDeityDamage = value; } }		// Otherworldy
	public float IncreasedElementalDamage	{ get {	return this.increasedElementalDamage; }		set { this.increasedElementalDamage = value; } }	// Otherworldy
	public float IncreasedHumanoidDamage	{ get {	return this.increasedHumanoidDamage; }		set { this.increasedHumanoidDamage = value; } }		// Humanlike
	public float IncreasedUndeadDamage		{ get {	return this.increasedUndeadDamage; }		set { this.increasedUndeadDamage = value; } }		// Humanlike

	// ******************
	// * PARAGON POINTS *
	// ******************
	// These are added to once the player hits max level, they will not be called Paragon Points.
	protected int paragonPoints;
	protected float paragonPointsCritChance = 1f, paragonPointsCritDamage = 1f, paragonPointsAnimalDamage = 1f, paragonPointsOtherWorldlyDamage = 1f, paragonPointsHumanlikeDamage = 1f,
					paragonPointsArmorRating = 1f, paragonPointsGoldAmount = 1f, paragonPointsExperience = 1f;




	[SerializeField]
	protected bool disableNaturalRegeneration = false;
	public bool DisableNaturalRegeneration	{ get {	return this.disableNaturalRegeneration; }	set { this.disableNaturalRegeneration = value; } }

	// **************
	// * EXPERIENCE *
	// **************
	// Experience needed to level at this current level.
	protected int totalExperienceNeededToLevel = 100;
	public int TotalExperienceNeededToLevel	{ get {	return this.totalExperienceNeededToLevel; }	protected set {	this.totalExperienceNeededToLevel = value; } }

	// The current level and the current experience value.
	protected int currentLevel = 1, currentExperience;
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
	public Weapon StartingWeapon	{ get {	return this.startingWeapon; }		protected set {	this.startingWeapon = value; } }
	public Weapon EquippedWeapon	{ get {	return this.equippedWeapon; }		protected set {	this.equippedWeapon = value; } }

	public Transform weaponLocation;

	// Misc
	float nextRegenTime;

	// Health methods.
	public void Damage(float value)		{ CurrentHealth -= value; }
	public void Heal(float value)		{ Damage(-value); }

	// Mana methods.
	public void DamageMana(float value)	{ CurrentMana -= value; }
	public void HealMana(float value)	{ DamageMana(-value); }

	public void BuffStat(EntityStat stat, int value) {
		
		switch (stat) {

			case EntityStat.ARMOR_RATING:
				ArmorRating += value;
				break;

			case EntityStat.CRIT_RATING:
				CritRating += value;
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

			case EntityStat.INTELLECT:
				Intellect += value;
				break;

			case EntityStat.MANA_REGENERATION:
				ManaRegenerationPerSecond += value;
				break;

			case EntityStat.STAMINA:
				Stamina += value;
				break;

		}

	}

	public void CheckForDeath() {

		if (CurrentHealth <= 0) {

			if (gameObject.tag == "Enemy") {
				
				Player.current.AmendCurrentXP (Mathf.RoundToInt(ExperienceGainedFromEntity * Player.current.ExperienceGain));

			}

			Destroy(gameObject);

		}

	}

	void ClampHealthAndMana() {

		CurrentHealth 	= Mathf.Clamp(CurrentHealth,	0f,	MaximumHealth);
		CurrentMana 	= Mathf.Clamp(CurrentMana,		0f, MaximumMana);

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

}

public enum EntityStat { ARMOR_RATING, CRIT_RATING, HEALTH_REGENERATION,
							INCREASED_ABERRATION, INCREASE_BEAST, INCREASE_CRITTER, INCREASE_DEITY, INCREASE_DEMON, INCREASE_ELEMENTAL, INCREASE_HUMANOID, INCREASE_UNDEAD,
							INTELLECT, MANA_REGENERATION, STAMINA };
// ABERRATION, BEAST, CRITTER, DEITY, DEMON, ELEMENTAL, HUMANOID, UNDEAD