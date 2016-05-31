using UnityEngine;
using System.Collections;
using System.Linq;

public class Projectile : MonoBehaviour {

	public DamageIndicator damageIndicator;

	public Effect DamageOverTime;

	// Speed and Damage values.
	[SerializeField]
	float speed = 1f, damage, currentDamage, lifetime = 8f;
	public float Speed			{ get {	return this.speed; }			set {	this.speed = value; } }
	public float Damage			{ get {	return this.damage; }			set {	this.damage = value; } }
	public float CurrentDamage	{ get {	return this.currentDamage; }	set {	this.currentDamage = value; } }
	public float Lifetime		{ get {	return this.lifetime; }			set {	this.lifetime = value; } }

	[SerializeField]
	bool isPiercing, isBurning, isFreezing;
	public bool IsPiercing		{ get {	return this.isPiercing; }	set {	this.isPiercing = value; } }
	public bool IsBurning		{ get {	return this.isBurning; }	set {	this.isBurning = value; } }
	public bool IsFreezing		{ get {	return this.isFreezing; }	set {	this.isFreezing = value; } }

	[SerializeField]
	float destroyingIn;

	int timesHit = 0, enemyID;

	void Start () {

		/// Destroys the gameObject in Lifetime seconds.
		Destroy (gameObject, lifetime);

		/// Sets the lifetime of the Projectile to a public var that shows in the inspector, this should be removed in retail.
		destroyingIn = lifetime;

		/// Sets the current damage to the total damage, this will be changed depending if the projectile is Piercing or not.
		currentDamage = damage;

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

				Debug.Log (gameObject.name + " hit " + other.transform.name + " (" + entity.gameObject.name + "), timesHit: " + timesHit + ", for damage: " + currentDamage + ", isPiercing: " + isPiercing);

				if (entity != null) {
					entity.CurrentHealth -= currentDamage;
				}

				/// This fires up the Static DamageIndicators class which takes in the current Projectile (to get the currentDamage) and the Enemy (to get the Transform).

				if (damageIndicator != null) {

					DamageIndicator dmg = Instantiate(damageIndicator, other.transform.position, other.transform.rotation) as DamageIndicator;

					dmg.GetComponentInChildren<TextMesh>().text = this.CurrentDamage.ToString();

				}

				/// Temp code to instantiate a temp dmg indicator.
				//GameObject dmg = Instantiate (dmgIndicator, other.transform.position, other.transform.rotation) as GameObject;
				//dmg.GetComponentInChildren<TextMesh> ().text = currentDamage.ToString ();
				//dmg.transform.LookAt (Camera.main.transform);

				if (IsBurning) {

					EffectSlots effectSlots = entity.GetComponent<EffectSlots> ();

					EffectDamageOverTime BurningDamageOverTime = Instantiate (DamageOverTime) as EffectDamageOverTime;

					BurningDamageOverTime.value = CurrentDamage * 0.1f;

					effectSlots.Add (BurningDamageOverTime, EffectType.DEBUFF);

				}

				/// Destroy the gameObject now as it's either not a piercing round or it is and it has hit more than 4 enemies.
				if (IsPiercing == false || (IsPiercing == true && timesHit > 3)) {

					//Debug.Log (gameObject.name + " destroyed with timesHit: " + timesHit + " and damage: " + currentDamage);

					Destroy (gameObject);

					return;

				}

				currentDamage -= (damage / 4);
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