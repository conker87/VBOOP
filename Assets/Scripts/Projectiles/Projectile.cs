using UnityEngine;
using System.Collections;
using System.Linq;

public class Projectile : MonoBehaviour {

	public DamageIndicator damageIndicator;

	public Effect DamageOverTime;

	// Speed and Damage values.
	[SerializeField]
	float speed = 1f, weaponTotalDamage, projectileDamage, lifetime = 8f;
	public float Speed				{ get {	return this.speed; }				set {	this.speed = value; } }
	public float WeaponAverageDamage	{ get {	return this.weaponTotalDamage; }	set {	this.weaponTotalDamage = value; } }
	public float ProjectileDamage	{ get {	return this.projectileDamage; }		set {	this.projectileDamage = value; } }
	public float Lifetime			{ get {	return this.lifetime; }				set {	this.lifetime = value; } }

	[SerializeField]
	bool isPiercing, isBurning, isFreezing;
	public bool IsPiercing		{ get {	return this.isPiercing; }	set {	this.isPiercing = value; } }
	public bool IsBurning		{ get {	return this.isBurning; }	set {	this.isBurning = value; } }
	public bool IsFreezing		{ get {	return this.isFreezing; }	set {	this.isFreezing = value; } }

	[SerializeField]
	float destroyingIn;

	public Weapon sourceWeapon;
	bool doBurning = false;

	int timesHit = 0, enemyID;

	void Start () {

		/// Destroys the gameObject in Lifetime seconds.
		Destroy (gameObject, lifetime);

		/// Sets the lifetime of the Projectile to a public var that shows in the inspector, this should be removed in retail.
		destroyingIn = lifetime;

	}

	void Update () {

		transform.Translate (Vector3.forward * Time.deltaTime * speed);

		// More of a DEBUG option really.
		destroyingIn -= Time.deltaTime;

	}

	void OnTriggerEnter(Collider other) {

		bool doDmg = false;

		Entity entity = other.GetComponentInParent<Entity>();

		/// If enemyBaseParent isn't null, then GetComponentInParent found an EnemyBase comp.
		if (entity != null) {

			if (timesHit == 0) {

				enemyID = entity.GetInstanceID ();

			}

			timesHit++;

			/// We want to do damage only when the times hit is equal to 1 (which means this is the first enemy it has encountered),
			/// or if the times hit is more than 1 and that the enemy ID is no longer the same.
			if (timesHit == 1 || (timesHit > 1 && enemyID != entity.GetInstanceID ())) {

				enemyID = entity.GetInstanceID ();
				doDmg = true;

			}

			if (doDmg) {

				//Debug.Log (gameObject.name + " hit " + other.transform.name + " (" + entity.gameObject.name + "), timesHit: " + timesHit + ", for damage: " + currentDamage + ", isPiercing: " + isPiercing);

				if (entity != null) {
					entity.CurrentHealth -= ProjectileDamage;
				}

				/// This fires up the Static DamageIndicators class which takes in the current Projectile (to get the currentDamage) and the Enemy (to get the Transform).

				if (damageIndicator != null) {

					DamageIndicator dmg = Instantiate(damageIndicator, other.transform.position, other.transform.rotation) as DamageIndicator;

					dmg.GetComponentInChildren<TextMesh>().text = this.ProjectileDamage.ToString();

				}

				/// Temp code to instantiate a temp dmg indicator.
				//GameObject dmg = Instantiate (dmgIndicator, other.transform.position, other.transform.rotation) as GameObject;
				//dmg.GetComponentInChildren<TextMesh> ().text = currentDamage.ToString ();
				//dmg.transform.LookAt (Camera.main.transform);

				if (IsBurning) {

					//if (sourceWeapon != 

					EffectSlots effectSlots = entity.GetComponent<EffectSlots> ();

					if (effectSlots.debuffs.Count > 0) {

						foreach (EffectDamageOverTime debuff in effectSlots.debuffs) {

							if (debuff.SourceWeapon == sourceWeapon) {

								debuff.OriginalTime = Time.time;
								debuff.value += WeaponAverageDamage * 0.1f;
								debuff.value = Mathf.Clamp (debuff.value, 0f, WeaponAverageDamage * 0.5f);

							} else { 

								doBurning = true;

							}

						}
					} else {

						doBurning = true;

					}

					if (doBurning == true) {

						EffectDamageOverTime BurningDamageOverTime = Instantiate (DamageOverTime) as EffectDamageOverTime;

						BurningDamageOverTime.value = WeaponAverageDamage * 0.1f;
						BurningDamageOverTime.EffectDuration = 1000f;
						BurningDamageOverTime.transform.parent = entity.transform;

						BurningDamageOverTime.SourceWeapon = sourceWeapon;

						effectSlots.Add (BurningDamageOverTime, EffectType.DEBUFF);

					}

					doBurning = false;

				}

				/// Destroy the gameObject now as it's either not a piercing round or it is and it has hit more than 4 enemies.
				if (IsPiercing == false || (IsPiercing == true && timesHit > 3)) {

					//Debug.Log (gameObject.name + " destroyed with timesHit: " + timesHit + " and damage: " + currentDamage);

					Destroy (gameObject);

					return;

				}

				ProjectileDamage -= (ProjectileDamage / 4);
				doDmg = false;
			}

		}

		/// The projectile hit geometry and as such needs to be destroyed.
		if ((new []{ "Geometry", "Untagged" }.Contains (other.tag))) {

			//Debug.Log ("This Projectile (" + gameObject.name + ") hit geometry and was destroyed");

			Destroy (gameObject);

			return;

		}

	}

}