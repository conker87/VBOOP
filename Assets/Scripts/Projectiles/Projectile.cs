using UnityEngine;
using System.Collections;
using System.Linq;

public class Projectile : MonoBehaviour {

	public DamageIndicator damageIndicator;

	public Effect DamageOverTime, HealOverTime;

	// Speed and Damage values.
	[SerializeField]
	float speed = 1f, weaponTotalDamage, projectileDamage, lifetime = 8f;
	public float Speed					{ get {	return this.speed; }				set {	this.speed = value; } }
	public float WeaponAverageDamage	{ get {	return this.weaponTotalDamage; }	set {	this.weaponTotalDamage = value; } }
	public float ProjectileDamage		{ get {	return this.projectileDamage; }		set {	this.projectileDamage = value; } }
	public float Lifetime				{ get {	return this.lifetime; }				set {	this.lifetime = value; } }

	[SerializeField]
	bool isPiercing, isBurning, isFreezing, isHealing;
	public bool IsPiercing		{ get {	return this.isPiercing; }	set {	this.isPiercing = value; } }
	public bool IsBurning		{ get {	return this.isBurning; }	set {	this.isBurning = value; } }
	public bool IsFreezing		{ get {	return this.isFreezing; }	set {	this.isFreezing = value; } }
	public bool IsHealing		{ get {	return this.isHealing; }	set {	this.isHealing = value; } }

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

		// Debug.Log ("OnTriggerEnter");

		timesHit++;

		Entity entity = other.GetComponentInParent<Entity> ();

		if (entity == null) {

			entity = other.GetComponent<Entity> ();

		}

		if (IsHealing == false && entity != null && entity.tag == "Coop") {

			return;

		}

		// The Weapon should not do damage to enemies if it is healing (TODO: or should it but do half damage?)
		if (IsHealing) {

			// If entity isn't null and the entity tag is "Coop".
			if (entity != null && entity.tag == "Coop") {

				// Heal the entity.
				entity.Heal (ProjectileDamage);

				EffectSlots effectSlots = entity.GetComponent<EffectSlots> ();

				if (effectSlots.buffs.Count > 0) {

					foreach (EffectHealOverTime buffs in effectSlots.buffs) {

						if (buffs.SourceWeapon == sourceWeapon) {

							// TODO: BALANCING!!
							buffs.OriginalTime = Time.time;
							buffs.value += WeaponAverageDamage * 0.25f;
							buffs.value = Mathf.Clamp (buffs.value, 0f, WeaponAverageDamage * 0.9f);

						} else { 

							doHealingOverTime = true;

						}

					}
				} else {

					doHealingOverTime = true;

				}

				// If doHealing is true and the entity's current health isn't it's maximum health then we should give them a HoT.
				if (doHealingOverTime == true && entity.CurrentHealth != entity.MaximumHealth) {

					Debug.Log ("HealingOverTime");

//					// TODO: This needs balancing.
//					EffectHealOverTime HealingOverTime = Instantiate (HealOverTime) as EffectHealOverTime;
//
//					HealingOverTime.value = ProjectileDamage * 0.1f;
//					HealingOverTime.EffectDuration = 1000f;
//					HealingOverTime.transform.parent = entity.transform;
//
//					HealingOverTime.SourceWeapon = sourceWeapon;
//
//					effectSlots.Add (HealingOverTime, EffectType.BUFF);

				}

				doHealingOverTime = false;

			}

		} else {

			// If entity isn't null and the entity tag is "Enemy".
			if (entity != null && entity.tag == "Enemy") {

				// We want to do damage only when the times hit is equal to 1 (which means this is the first enemy it has encountered),
				// or if the times hit is more than 1 and that the enemy ID is no longer the same.
				if (timesHit == 1 || (timesHit > 1 && enemyID != entity.GetInstanceID ())) {

					enemyID = entity.GetInstanceID ();

					//Debug.Log (gameObject.name + " hit " + other.transform.name + " (" + entity.gameObject.name + "), timesHit: " + timesHit + ", for damage: " + currentDamage + ", isPiercing: " + isPiercing);

					float damageReductionDueToArmor = (100 - (entity.ArmorRating * Random.Range(0.9f, 1.1f))) / 100;
					float actualDamage = Mathf.Clamp(ProjectileDamage * damageReductionDueToArmor, 0f, WeaponAverageDamage * 3f);

					entity.Damage (actualDamage);

					// Fire up the DamageIndicators prefab which takes in the current Projectile (to get the currentDamage) and the Enemy (to get the Transform).
					if (damageIndicator != null) {

						DamageIndicator dmg = Instantiate (damageIndicator, other.transform.position, other.transform.rotation) as DamageIndicator;

						dmg.GetComponentInChildren<TextMesh> ().text = actualDamage.ToString();

					}

					// If the projectile (and therefore the gun) has a burning effect, them give the target Entity the burning DoT.
					// This damaged is capped at 50% of the average damage.
					if (IsBurning) {

						EffectSlots effectSlots = entity.GetComponent<EffectSlots> ();

						if (effectSlots.debuffs.Count > 0) {

							foreach (EffectDamageOverTime debuff in effectSlots.debuffs) {

								if (debuff.SourceWeapon == sourceWeapon) {

//									debuff.OriginalTime = Time.time;
//									debuff.value += WeaponAverageDamage * 0.5f;
//									debuff.value = Mathf.Clamp (debuff.value, 0f, WeaponAverageDamage * 0.5f);

								} else { 

									doBurning = true;

								}

							}
						} else {

							doBurning = true;

						}

						if (doBurning == true) {

//							EffectDamageOverTime BurningDamageOverTime = Instantiate (DamageOverTime) as EffectDamageOverTime;

							effectSlots.Add(new Effect("Burning

//							BurningDamageOverTime.value = ProjectileDamage * 0.5f;
//							BurningDamageOverTime.EffectDuration = 1000f;
//							BurningDamageOverTime.transform.parent = entity.transform;
//
//							BurningDamageOverTime.SourceWeapon = sourceWeapon;
//
//							effectSlots.Add (BurningDamageOverTime, EffectType.DEBUFF);

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