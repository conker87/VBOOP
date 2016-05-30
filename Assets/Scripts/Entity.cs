using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

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

	// Current Health & Mana
	// These values are the Entity's realtime health and mana resources.
	[SerializeField]
	protected float currentHealth, currentMana;
	public float CurrentHealth	{ get {	return this.currentHealth; }	set {	this.currentHealth = value; } }
	public float CurrentMana	{ get {	return this.currentMana; }		set {	this.currentMana = value; } }

	// Maximum Health & Mana
	// The maximum resources.
	// Can be changed via Equipment and Effects.
	[SerializeField]
	protected float maximumHealth, maximumMana;
	public float MaximumHealth	{ get {	return this.maximumHealth; }	set {	this.maximumHealth = value; } }
	public float MaximumMana	{ get {	return this.maximumMana; }		set {	this.maximumMana = value; } }

	// Armor Rating (yes I'm spelling it the American way because it looks less weird)
	// This value reduces physical damage (all damage is physical, bar those caused by Burning & Freezing).
	// Can be changed via Equipment and Effects.
	// TODO: Should this be a percentage or an actual value that increases? Percentages scale with levels, values do not.
	//				[Range(0, 100)]
	[SerializeField]
	protected int armorRating = 0;
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

		if (currentMana <= 0) {

			currentMana = 0;

		}

		if (currentMana > maximumMana) {

			currentMana = maximumMana;

		}

		if (currentHealth > maximumHealth) {

			currentHealth = maximumHealth;

		}

	}

}
