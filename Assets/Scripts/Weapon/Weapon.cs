using UnityEngine;
using System.Collections;
using System.Linq;

[ExecuteInEditMode]
public abstract class Weapon : MonoBehaviour {

	SharedFunctions sf = new SharedFunctions();

	[Header("Location Settings")]
	public Transform[] shootFromLocation;

	// Weapon Descriptors
	[Header("Weapon Descriptors")]
	public WeaponFireType weaponFireType;
	public WeaponHanded weaponHanded;						// TODO: Decide what to do with this.
	public WeaponType weaponType; 
	public WeaponQuality weaponQuality;
	public WeaponPrefix weaponPrefix;
	public WeaponSuffix weaponSuffix;
	public WeaponProjectileType weaponProjectileType;
	[SerializeField]
	string weaponName;

	// Damage & Projectiles Descriptors
	[Header("Damage & Projectiles Descriptors")]
	[Range(0.0001f, 20f)]
	public float attackSpeed = 1f;
	public float projectileMinimumDamage = 5f, projectileMaximumDamage = 10f;
	[Space(5)]
	public int projectilesPerShot = 1, projectilesPerClip = 8;

	[Space(5)]

	// Do we really need this value? Should we just calculate them in code?
	[SerializeField]
	float damagePerSecond;

	protected float damagePerProjectile;
	public float DamagePerProjectile	{ get {	return this.damagePerProjectile; }	set {	this.damagePerProjectile = value; } }

	// Projectile settings
	[Header("Projectile Settings")]
	[Range(5f, 100), SerializeField]
	protected float projectileVelocity = 35f;
	public float ProjectileVelocity		{ get {	return this.projectileVelocity; }	set {	this.projectileVelocity = value; } }


	protected bool shouldDamageBeCalculated = true;

	float nextShotTime;

	protected abstract void OverrideShoot (Transform loc);
	//protected abstract void OverrideShoot (Transform loc);

	protected virtual void Start () {

		GenerateWeaponQuality ();

	}

	void Update () {

		damagePerSecond = ( (1 / attackSpeed) * ( ( projectileMinimumDamage + projectileMaximumDamage ) / 2 ) ) * projectilesPerShot;

	}

	public void GenerateWeaponQuality() {

		weaponQuality = sf.RandomEnumValue<WeaponQuality>();
		if (weaponQuality == WeaponQuality.UNIQUE) { weaponQuality = WeaponQuality.MAGNIFICENT; }
		weaponPrefix = sf.RandomEnumValue<WeaponPrefix>();
		weaponSuffix = sf.RandomEnumValue<WeaponSuffix>();
		weaponProjectileType = sf.RandomEnumValue<WeaponProjectileType>();

		weaponName = weaponQuality + " " + weaponPrefix + " " + weaponType + " " + weaponSuffix + ", with " + weaponProjectileType + " rounds.";

	}

	public virtual void Shoot () {

		if (Time.time > nextShotTime) {

			shouldDamageBeCalculated = true;

			nextShotTime = Time.time + attackSpeed;

			for (int i = 0; i < projectilesPerShot; i++) {

				foreach (Transform loc in shootFromLocation) {

					OverrideShoot (loc);

					shouldDamageBeCalculated = false;
				}
			}

		}

	}

	public void SetAttackSpeed(float _attackSpeed) {

		attackSpeed = _attackSpeed;

	}

	public void SetProjectileVelocity(float _projectileVelocity) {

		projectileVelocity = _projectileVelocity;

	}

}

public enum WeaponFireType			{ SINGLE_SHOT, SEMI_AUTOMATIC, AUTOMATIC, SPREAD, SPRAY, PROJECTILE };
public enum WeaponHanded			{ ONE_HANDED, TWO_HANDED }													 	// TODO: On the fence on whether to use this or not, can pistols be dual wielded?
public enum WeaponQuality			{ AWFUL, CRAP, NORMAL, GOOD, GREAT, BRILLIANT, MAGNIFICENT, UNIQUE }
public enum WeaponPrefix			{ UNDEAD_SLAYING, ABERRATION_ANNIHILATING, BEAST_BLASTING, MAN_MURDERING, CRITTER_CRUSHING, HEALING, NULL };
public enum WeaponType				{ SHOTGUN, RIFLE, PISTOL, FLAMETHROWER, FROSTTHROWER, UZI, SUBMACHINE_GUN, MUSKET };
public enum WeaponSuffix			{ MANA_REGENERATION, HEALTH_REGENERATION, FORTITUDE, MANA_CAPACITY, NULL }; 	// TODO: There can be more than one of these added to the var with this information needs to be an array!
public enum WeaponProjectileType	{ PIERCING, BURNING, FREEZING, NULL } 											// TODO: There can be more than one of these added to the var with this information needs to be an array!