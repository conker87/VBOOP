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

		// GetComponentInParent returns itself too, so the previous code could be consolidated into one monthly repayment.
		if (targetEntity == null) {

			return;

		}

		EffectSlots effectSlots = targetEntity.GetComponent<EffectSlots> ();

		// If the Entity we hit is a Coop player.
		if (targetEntity.tag == "Coop") {
			
			if (IsHealing) {

				// Heal the entity.
				targetEntity.Heal (ProjectileDamage, IsCrit);

				// Massive gameplay change here where we let those with CurrentHealth == MaximumHealth a HoT.
				effectSlots.Add (new Effect ("Healing", "Healing for whatever over whatever", 
					5f, Time.time, new float[] { 1 },
					sourceWeapon, targetEntity, new Action[] {
					() => targetEntity.Heal (ProjectileDamage * 0.5f),
				}, null), EffectType.BUFF);

			// If the AmmoType is not healing and the Entity we hit is a Coop player ([PH]), then ignore the rest of the code and passthrough.
			} else { return; }

		}

		// If the Entity we hit is a Coop player.
		if (targetEntity.tag == "Enemy") {

				// We want to do damage only when the times hit is equal to 1 (which means this is the first enemy it has encountered),
				// or if the times hit is more than 1 and that the enemy ID is no longer the same.
			if (timesHit == 1 || (timesHit > 1 && enemyID != targetEntity.GetInstanceID ())) {

				// Calculate the actualDamage depending on the armor reduction.
				// TODO: Calculate ArmorRating depending on the level of the Entity.
				float damageReductionDueToArmor = (100 - (targetEntity.ArmorRating * UnityEngine.Random.Range(0.9f, 1.1f))) / 100;

				// This Math.Round method truncates the float to 2 decimal places.
				float actualDamage = (float) Math.Round(Mathf.Clamp(ProjectileDamage * damageReductionDueToArmor, 0f, WeaponAverageDamage * 3f), 2);

				targetEntity.Damage (actualDamage, IsCrit);

				// If the AmmoType IsBurning, then give the target Entity the burning DoT.
				// This damaged is capped at 50% of the average damage.
				if (IsBurning) {

					int armourRatingTemp = (int) (targetEntity.ArmorRating - (targetEntity.ArmorRating * 0.9f));

					Debug.Log (armourRatingTemp);

						effectSlots.Add(new Effect("Burning", "Burning for 25% of projectile damage over 8 seconds and reduces the target's armor by 10%", 
							8f, Time.time, new float[] { 1f, 4.9f, 4.9f },
							sourceWeapon, targetEntity, new Action[] {
								() => targetEntity.Damage(Mathf.Abs(damageReductionDueToArmor * 0.25f)),
								() => targetEntity.SetTempValue(0, targetEntity.ArmorRating),
								() => targetEntity.DebuffStat(EntityStat.ARMOR_RATING, armourRatingTemp)
						}, new Action[] {
							() => targetEntity.BuffStat(EntityStat.ARMOR_RATING, armourRatingTemp)
						}), EffectType.DEBUFF);
								
				}

				// Reduce the ProjectileDamage by 4 but only if it is relivant to do so.
				if (IsPiercing) { ProjectileDamage -= (ProjectileDamage / 4); }
				enemyID = targetEntity.GetInstanceID ();

			}
		}

		// Destroy the gameObject now as it's either not a piercing round or it is and it has hit more than 4 enemies or
		// the projectile hit geometry and as such needs to be destroyed.
		if (IsPiercing == false || (IsPiercing == true && timesHit > 3) || (new []{ "Geometry", "Untagged" }.Contains (other.tag))) {

			//Debug.Log ("This Projectile (" + gameObject.name + ") hit geometry and was destroyed");

			Destroy (gameObject);

			return;

		}

	}

}