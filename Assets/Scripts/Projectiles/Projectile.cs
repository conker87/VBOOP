using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public class Projectile : MonoBehaviour {

	public Effect DamageOverTime, HealOverTime;

	// Speed and Damage values.
	[SerializeField]
	float speed = 1f, weaponTotalDamage, projectileDamage, lifetime = 8f;
	public float Speed					{ get {	return this.speed; }				set {	this.speed = value; } }
	public float WeaponAverageDamage	{ get {	return this.weaponTotalDamage; }	set {	this.weaponTotalDamage = value; } }
	public float ProjectileDamage		{ get {	return this.projectileDamage; }		set {	this.projectileDamage = value; } }
	public float Lifetime				{ get {	return this.lifetime; }				set {	this.lifetime = value; } }

	[SerializeField]
	bool isBleeding, isBurning, isFreezing, isHealing, isLeeching, isPiercing, isPoison;
	public bool IsBleeding		{ get {	return this.isBleeding; }	set {	this.isBleeding = value; } }
	public bool IsBurning		{ get {	return this.isBurning; }	set {	this.isBurning = value; } }
	public bool IsFreezing		{ get {	return this.isFreezing; }	set {	this.isFreezing = value; } }
	public bool IsHealing		{ get {	return this.isHealing; }	set {	this.isHealing = value; } }
	public bool IsLeeching		{ get {	return this.isLeeching; }	set {	this.isLeeching = value; } }
	public bool IsPiercing		{ get {	return this.isPiercing; }	set {	this.isPiercing = value; } }
	public bool IsPoison		{ get {	return this.isPoison; }		set {	this.isPoison = value; } }

	bool isCrit;
	public bool IsCrit			{ get {	return this.isCrit; }		set {	this.isCrit = value; } }

	[SerializeField]
	float destroyingIn;

	public Weapon sourceWeapon;
	bool doFreezing = false, doBurning = false, doHealingOverTime = false;

	int timesHit = 0, enemyID;

	void Start () {

		// Destroys the gameObject in Lifetime seconds.
		Destroy (gameObject, lifetime);

		// Sets the lifetime of the Projectile to a public var that shows in the inspector, this should be removed in retail.
		destroyingIn = lifetime;

	}

	void FixedUpdate () {

		transform.Translate (Vector3.forward * Time.fixedDeltaTime * speed);

		// More of a DEBUG option really.
		destroyingIn -= Time.fixedDeltaTime;

	}

	void OnTriggerEnter(Collider other) {

		timesHit++;

		Entity targetEntity = other.GetComponentInParent<Entity> ();

		if (targetEntity == null) {

			targetEntity = other.GetComponent<Entity> ();

		}

		if (IsHealing == false && targetEntity != null && targetEntity.tag == "Coop") {

			return;

		}

		// The Weapon should not do damage to enemies if it is healing (TODO: or should it but do half damage?)
		if (IsHealing) {

			// If entity isn't null and the entity tag is "Coop".
			if (targetEntity != null && targetEntity.tag == "Coop") {

				// Heal the entity.
				targetEntity.Heal (ProjectileDamage, IsCrit);

				EffectSlots effectSlots = targetEntity.GetComponent<EffectSlots> ();

				// If doHealing is true and the entity's current health isn't it's maximum health then we should give them a HoT.
				if (targetEntity.CurrentHealth != targetEntity.MaximumHealth) {

					effectSlots.Add(new Effect("Healing", "Healing for whatever over whatever", 
						5f, Time.time, new float[] { 1 },
						sourceWeapon, targetEntity, new Action[] {
							() => targetEntity.Heal(ProjectileDamage * 0.5f),
						}, null), EffectType.BUFF);

				}

			}

		} else {

			// If entity isn't null and the entity tag is "Enemy".
			if (targetEntity != null && targetEntity.tag == "Enemy") {

				// We want to do damage only when the times hit is equal to 1 (which means this is the first enemy it has encountered),
				// or if the times hit is more than 1 and that the enemy ID is no longer the same.
				if (timesHit == 1 || (timesHit > 1 && enemyID != targetEntity.GetInstanceID ())) {

					enemyID = targetEntity.GetInstanceID ();

					//Debug.Log (gameObject.name + " hit " + other.transform.name + " (" + entity.gameObject.name + "), timesHit: " + timesHit + ", for damage: " + currentDamage + ", isPiercing: " + isPiercing);

					float damageReductionDueToArmor = (100 - (targetEntity.ArmorRating * UnityEngine.Random.Range(0.9f, 1.1f))) / 100;
					float actualDamage = (float) Math.Round(Mathf.Clamp(ProjectileDamage * damageReductionDueToArmor, 0f, WeaponAverageDamage * 3f), 2);

					targetEntity.Damage (actualDamage, IsCrit);

					// If the projectile (and therefore the gun) has a burning effect, them give the target Entity the burning DoT.
					// This damaged is capped at 50% of the average damage.
					if (IsBurning) {

						EffectSlots effectSlots = targetEntity.GetComponent<EffectSlots> ();

						if (effectSlots.debuffs.Count > 0) {

							foreach (Effect debuff in effectSlots.debuffs) {

								if (debuff.SourceWeapon != sourceWeapon) {

									doBurning = true;

								}

							}

						} else {

							doBurning = true;

						}

						if (doBurning == true) {

							effectSlots.Add(new Effect("Burning", "Burning for whatever over whatever", 
								16f, Time.time, new float[] { 1 },
								sourceWeapon, targetEntity, new Action[] {
									() => targetEntity.Damage(ProjectileDamage * 0.5f)
								}, null), EffectType.DEBUFF);

						}

						doBurning = false;

					}

					ProjectileDamage -= (ProjectileDamage / 4);
				}
			}

		}

		// Destroy the gameObject now as it's either not a piercing round, it is and it has hit more than 4 enemies or
		// the projectile hit geometry and as such needs to be destroyed.
		if (IsPiercing == false || (IsPiercing == true && timesHit > 3) || (new []{ "Geometry", "Untagged" }.Contains (other.tag))) {

			//Debug.Log ("This Projectile (" + gameObject.name + ") hit geometry and was destroyed");

			Destroy (gameObject);

			return;

		}

	}

}