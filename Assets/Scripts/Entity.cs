using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	// Name and Title
	protected string entityName, entityTitle;
	public string EntityName	{ get {	return this.entityName; }	set {	this.entityName = value; } }
	public string EntityTitle	{ get {	return this.entityTitle; }	set {	this.entityTitle = value; } }

	// Base Health & Mana
	// Used to calculate health base upon level.
	protected float baseHealth = 20, baseMana = 10;
	public float BaseHealth		{ get {	return this.baseHealth; }	set {	this.baseHealth = value; } }
	public float BaseMana		{ get {	return this.baseMana; }		set {	this.baseMana = value; } }

	// Current Health & Mana
	[SerializeField]
	protected float currentHealth, currentMana;
	public float CurrentHealth	{ get {	return this.currentHealth; }	set {	this.currentHealth = value; } }
	public float CurrentMana	{ get {	return this.currentMana; }		set {	this.currentMana = value; } }

	// Maximum Health & Mana.
	[SerializeField]
	protected float maximumHealth, maximumMana;
	public float MaximumHealth	{ get {	return this.maximumHealth; }	set {	this.maximumHealth = value; } }
	public float MaximumMana	{ get {	return this.maximumMana; }		set {	this.maximumMana = value; } }

	// enemyXPForCurrentLevel
	// should be calculated depending on the Player current level, if the Player is more than [3] levels above the enemy should not give XP. 1/3 of the total XP should be removed per level above enemyCurrentLevel.
	public int XPForCurrentLevel = 10;


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
