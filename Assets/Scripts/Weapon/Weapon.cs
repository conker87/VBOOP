using UnityEngine;
using System.Collections;
using System.Linq;

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

	public float manaCost = 1f;
	public float ManaCost				{ get {	return this.manaCost; }	set {	this.manaCost = value; } }

	[Range(0.0001f, 20f)]
	public float attackSpeed = 1f;
	public float projectileMinimumDamage = 5f, projectileMaximumDamage = 10f;
	[Space(5)]
	public int projectilesPerShot = 1;
	[Space(5)]

	protected float damagePerProjectile, gunDamageThisShot;
	public float DamagePerProjectile	{ get {	return this.damagePerProjectile; }	set {	this.damagePerProjectile = value; } }
	public float GunDamageThisShot		{ get {	return this.gunDamageThisShot; }	set {	this.gunDamageThisShot = value; } }

	// Projectile settings
	[Header("Projectile Settings")]
	[Range(5f, 100), SerializeField]
	protected float projectileVelocity = 35f;
	public float ProjectileVelocity		{ get {	return this.projectileVelocity; }	set {	this.projectileVelocity = value; } }

	protected Projectile newProjectile;

	protected bool shouldDamageBeCalculated = true;

	float nextShotTime;

	protected abstract void OverrideShoot (Transform loc, out Projectile newProjectile);

	protected virtual void Start () {

		GenerateWeaponQuality ();

	}

	void Update () {

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

		if (Time.time > nextShotTime && Player.current.CurrentMana >= ManaCost ) {

			Player.current.DamageMana (manaCost);

			GunDamageThisShot = Random.Range (projectileMinimumDamage, projectileMaximumDamage);

			if (Random.Range (0f, 100f) < Player.current.CritRating) {

				DamagePerProjectile = Random.Range(1.4f, 2f);

			} else {

				DamagePerProjectile = 1f;

			}

			DamagePerProjectile *= (GunDamageThisShot / projectilesPerShot);

			nextShotTime = Time.time + attackSpeed;

			for (int i = 0; i < projectilesPerShot; i++) {

				foreach (Transform loc in shootFromLocation) {

					OverrideShoot (loc, out newProjectile);

					newProjectile.ProjectileDamage = DamagePerProjectile;
					newProjectile.WeaponAverageDamage = (projectileMinimumDamage + projectileMaximumDamage) / 2;
					newProjectile.Lifetime = 5f;

					newProjectile.IsPiercing	= (weaponProjectileType == WeaponProjectileType.PIERCING) ? true : false;
					newProjectile.IsBurning		= (weaponProjectileType == WeaponProjectileType.BURNING) ? true : false;
					newProjectile.IsFreezing 	= (weaponProjectileType == WeaponProjectileType.FREEZING) ? true : false;
					newProjectile.IsHealing 	= (weaponProjectileType == WeaponProjectileType.HEALING) ? true : false;

					newProjectile.sourceWeapon = this;
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
public enum WeaponProjectileType	{ PIERCING, BURNING, FREEZING, HEALING, NULL } 											// TODO: There can be more than one of these added to the var with this information needs to be an array!