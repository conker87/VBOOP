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
	protected int stamina = 1, intellect = 1, armorRating = 0, critRating = 1, healthRegenerationPerSecond = 5, manaRegenerationPerSecond = 5;
	[SerializeField] protected bool disableNaturalRegeneration = false;
	public int Stamina						{ get {	return this.stamina; }			set {	this.stamina = value; } }
	public int Intellect					{ get {	return this.intellect; }		set {	this.intellect = value; } }
	public int ArmorRating					{ get {	return this.armorRating; }		set {	this.armorRating = value; } }
	public int CritRating					{ get {	return this.critRating; }		set {	this.critRating = value; } }
	public int HealthRegenerationPerSecond	{ get {	return this.healthRegenerationPerSecond; }		set {	this.healthRegenerationPerSecond = value; } }
	public int ManaRegenerationPerSecond	{ get {	return this.manaRegenerationPerSecond; }		set {	this.manaRegenerationPerSecond = value; } }
	public bool DisableNaturalRegeneration	{ get {	return this.disableNaturalRegeneration; }		set {	this.disableNaturalRegeneration = value; } }

	// **************
	// * EXPERIENCE *
	// **************
	// Experience needed to level at this current level.
	protected int totalExperienceNeededToLevel = 10;
	public int TotalExperienceNeededToLevel	{ get {	return this.totalExperienceNeededToLevel; }	protected set {	this.totalExperienceNeededToLevel = value; } }

	// The current level and the current experience value.
	protected int currentLevel = 1, currentExperience;
	public int CurrentLevel			{ get {	return this.currentLevel; }			protected set {	this.currentLevel = value; } }
	public int CurrentExperience	{ get {	return this.currentExperience; }	protected set {	this.currentExperience = value; } }

	// The experience given to the Player if this inherited entity is an Enemy.
	protected int experienceGainedFromEntity = 10;
	public int ExperienceGainedFromEntity	{ get {	return this.experienceGainedFromEntity; }	protected set {	this.experienceGainedFromEntity = value; } }

	// ***********
	// * WEAPONS *
	// ***********
	// The current equipped weapon.
	public Weapon equippedWeapon, startingWeapon;
	public Weapon StartingWeapon	{ get {	return this.startingWeapon; }		protected set {	this.startingWeapon = value; } }
	public Weapon EquippedWeapon	{ get {	return this.equippedWeapon; }		protected set {	this.equippedWeapon = value; } }

	// Misc
	float nextRegenTime;

	public void Damage(float value) {

		CurrentHealth -= value;

	}

	public void DamageMana(float value) {

		CurrentMana -= value;

	}

	public void Heal(float value) {

		Damage(-value);

	}

	public void HealMana(float value) {

		DamageMana(-value);

	}

	public void BuffStat(EntityStat stat, int value) {
		
		switch (stat) {

			case EntityStat.ARMORRATING:
				ArmorRating += value;
				break;

			case EntityStat.CRITRATING:
				CritRating += value;
				break;

			case EntityStat.HEALTHREGENERATIONPERSECOND:
				HealthRegenerationPerSecond += value;
				break;

			case EntityStat.INTELLECT:
				Intellect += value;
				break;

			case EntityStat.MANAREGENERATIONPERSECOND:
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
				
				Player.current.AmendCurrentXP (TotalExperienceNeededToLevel);

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
			EquipWeapon (StartingWeapon);
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


public enum EntityStat { ARMORRATING, CRITRATING, HEALTHREGENERATIONPERSECOND, INTELLECT, MANAREGENERATIONPERSECOND, STAMINA };