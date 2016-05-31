using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour {

	// CONST
	int healthPerStamina = 10, manaPerIntellect = 10;
	public int HealthPerStamina	{ get {	return this.healthPerStamina; } }
	public int ManaPerIntellect	{ get {	return this.manaPerIntellect; } }

	// Name and Title
	//[SerializeField]
	public string entityName, entityTitle;
	public string EntityName	{ get {	return this.entityName; }		set {	this.entityName = value; } }
	public string EntityTitle	{ get {	return this.entityTitle; }		set {	this.entityTitle = value; } }

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
	public float MaximumHealth	{ get {	return this.maximumHealth; }	set {	this.maximumHealth = value; } }
	public float MaximumMana	{ get {	return this.maximumMana; }		set {	this.maximumMana = value; } }

	// Current Health & Mana
	// These values are the Entity's realtime health and mana resources.
	[SerializeField]
	protected float currentHealth, currentMana;
	public float CurrentHealth	{ get {	return this.currentHealth; }	set {	this.currentHealth = value; } }
	public float CurrentMana	{ get {	return this.currentMana; }		set {	this.currentMana = value; } }

	// Stamina, Intellect & Armor Rating (yes I'm spelling it the American way because it looks less weird)
	// This is the amount of total health, the amount of total mana and also adds to elemental damage (possibly).
	// This value reduces physical damage (all damage is physical, bar those caused by Burning & Freezing).
	// TODO: Should this be a percentage or an actual value that increases? Percentages scale with levels, values do not.
	[SerializeField]
	protected int stamina = 1, intellect = 1, armorRating = 0;
	public int Stamina			{ get {	return this.stamina; }			set {	this.stamina = value; } }
	public int Intellect		{ get {	return this.intellect; }		set {	this.intellect = value; } }
	public int ArmorRating		{ get {	return this.armorRating; }		set {	this.armorRating = value; } }

	// enemyXPForCurrentLevel
	// should be calculated depending on the Player current level, if the Player is more than [3] levels above the enemy should not give XP. 1/3 of the total XP should be removed per level above enemyCurrentLevel.
	public int XPForCurrentLevel = 10;

	public void Damage(float value) {

		CurrentHealth -= value;

	}

	public void Heal(float value) {

		Damage (-value);

	}

	public void CheckForDeath() {

		if (CurrentHealth <= 0) {

			Player.current.AmendCurrentXP (XPForCurrentLevel);

			Destroy(gameObject);

		}

	}

	public void ClampHealthAndMana() {

		if (CurrentMana <= 0) {

			CurrentMana = 0;

		}

		if (CurrentMana > MaximumMana) {

			CurrentMana = MaximumMana;

		}

		if (CurrentHealth > MaximumHealth) {

			CurrentHealth = MaximumHealth;

		}

	}

	protected abstract void Start();

	protected virtual void Update() {

		// This needs to be in Update and in a method of it's own due to equipment.
		MaximumHealth = (HealthPerStamina * Stamina) + BaseHealth;
		MaximumMana = (ManaPerIntellect * Intellect) + BaseMana;

	}

}
