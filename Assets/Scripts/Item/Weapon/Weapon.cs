using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public abstract class Weapon : Item {

	SharedFunctions sf = new SharedFunctions();

	[Header("Location Settings")]
	public Transform[] shootFromLocation;

	// Weapon Descriptors
	[Header("Weapon Descriptors")]
	[SerializeField] protected WeaponFireType weaponFireType;
	public WeaponFireType 	WeaponFireType				{ get {	return this.weaponFireType; }	set {	this.weaponFireType = value; } }
	[SerializeField] protected WeaponHanded weaponHanded;
	public WeaponHanded 	WeaponHanded				{ get {	return this.weaponHanded; }		set {	this.weaponHanded = value; } }
	[SerializeField] protected WeaponType weaponType; 
	public WeaponType 		WeaponType					{ get {	return this.weaponType; }		set {	this.weaponType = value; } }
	[SerializeField] protected WeaponQuality weaponQuality;
	public WeaponQuality 	WeaponQuality				{ get {	return this.weaponQuality; }	set {	this.weaponQuality = value; } }
	[SerializeField] protected WeaponPrefix weaponPrefix;
	public WeaponPrefix 	WeaponPrefix				{ get {	return this.weaponPrefix; }		set {	this.weaponPrefix = value; } }
	[SerializeField] protected WeaponProjectileType weaponProjectileType;
	public WeaponProjectileType WeaponProjectileType	{ get {	return this.weaponProjectileType; }	set {	this.weaponProjectileType = value; } }

	// Mana & Weapon Descriptors Values
	[Header("Mana & Weapon Descriptors Values")]
	[SerializeField]
	protected float manaCost = 1f;
	public float ManaCost			{ get {	return this.manaCost; }	set {	this.manaCost = value; } }
	[Space(5)]
	protected float prefixValueAmount, suffixValueAmount;
	public float PrefixValueAmount	{ get {	return this.prefixValueAmount; }	set {	this.prefixValueAmount = value; } }
	public float SuffixValueAmount	{ get {	return this.suffixValueAmount; }	set {	this.suffixValueAmount = value; } }

	// Damage & Projectiles Descriptors
	[Header("Damage & Projectiles Descriptors")]
	[Range(0.0001f, 20f)]
	[SerializeField]
	protected float attackSpeed = 1f;
	[SerializeField]
	protected float projectileMinimumDamage = 5f, projectileMaximumDamage = 10f;
	public float AttackSpeed				{ get {	return this.attackSpeed; }	set {	this.attackSpeed = value; } }
	public float ProjectileMinimumDamage	{ get {	return this.projectileMinimumDamage; }	set {	this.projectileMinimumDamage = value; } }
	public float ProjectileMaximumDamage	{ get {	return this.projectileMaximumDamage; }	set {	this.projectileMaximumDamage = value; } }
	[Space(5)]
	[SerializeField]
	public int projectilesPerShot = 1;
	public int ProjectilesPerShot		{ get {	return this.projectilesPerShot; }	set {	this.projectilesPerShot = value; } }
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

	bool crit = false;
	float nextShotTime;

	protected virtual void Start () {

		// Set default Item inheritance for Weapon
		StackSize = 1;

		GenerateWeaponQuality ();

	}

	void Update () {

	}

	public void GenerateWeaponQuality() {

		WeaponQuality = sf.RandomEnumValue<WeaponQuality>();
		if (weaponQuality == WeaponQuality.UNIQUE) { weaponQuality = WeaponQuality.MAGNIFICENT; }
		WeaponPrefix = sf.RandomEnumValue<WeaponPrefix>();
		WeaponProjectileType = sf.RandomEnumValue<WeaponProjectileType>();

		ItemName = weaponQuality + " " + weaponPrefix + " " + weaponType + " " + ", with " + weaponProjectileType + " rounds.";

	}

	public virtual void Shoot () {

		// Checks to see if the cooldown of the shot is over and the Player has more Mana than it costs to fire.
		if (Time.time > nextShotTime && Player.current.CurrentMana >= ManaCost ) {

			Player.current.DamageMana (manaCost);

			GunDamageThisShot = (float) Math.Round(UnityEngine.Random.Range (ProjectileMinimumDamage, ProjectileMaximumDamage), 2);

			if (UnityEngine.Random.Range (0f, 100f) < Player.current.CritRating) {

				DamagePerProjectile = 1.6f * Player.current.IncreasedCritDamage;
				crit = true;

			} else {

				DamagePerProjectile = 1.0f;

			}

			DamagePerProjectile *= (float) Math.Round((GunDamageThisShot / ProjectilesPerShot) * 100f) / 100f;

			for (int i = 0; i < projectilesPerShot; i++) {

				foreach (Transform loc in shootFromLocation) {

					OverrideShoot (loc, out newProjectile);

					newProjectile.ProjectileDamage = DamagePerProjectile;
					newProjectile.WeaponAverageDamage = (float) Math.Round((projectileMinimumDamage + projectileMaximumDamage) / 2f, 2);
					newProjectile.Lifetime = 5f;

					newProjectile.IsBleeding	= (Player.current.EquippedAmmo.AmmoType == AmmoType.BLEEDING)	? true : false;
					newProjectile.IsBurning 	= (Player.current.EquippedAmmo.AmmoType == AmmoType.BURNING)	? true : false;
					newProjectile.IsFreezing 	= (Player.current.EquippedAmmo.AmmoType == AmmoType.FREEZING)	? true : false;
					newProjectile.IsHealing 	= (Player.current.EquippedAmmo.AmmoType == AmmoType.HEALING)	? true : false;
					newProjectile.IsLeeching 	= (Player.current.EquippedAmmo.AmmoType == AmmoType.LEECHING)	? true : false;
					newProjectile.IsPiercing 	= (Player.current.EquippedAmmo.AmmoType == AmmoType.PIERCING)	? true : false;
					newProjectile.IsPoison 		= (Player.current.EquippedAmmo.AmmoType == AmmoType.POISON)		? true : false;
	
					newProjectile.IsCrit = crit;

					newProjectile.sourceWeapon = this;
				}

			}

			nextShotTime = Time.time + AttackSpeed;

			crit = false;
		}

	}

	protected abstract void OverrideShoot (Transform loc, out Projectile newProjectile);

	public void SetAttackSpeed(float _attackSpeed) {

		AttackSpeed = _attackSpeed;

	}

	public void SetProjectileVelocity(float _projectileVelocity) {

		ProjectileVelocity = _projectileVelocity;

	}

}

public enum WeaponFireType			{ SINGLE_SHOT, SEMI_AUTOMATIC, AUTOMATIC, SPREAD, SPRAY, PROJECTILE };
public enum WeaponHanded			{ ONE_HANDED, TWO_HANDED }													 	// TODO: On the fence on whether to use this or not, can pistols be dual wielded?
public enum WeaponQuality			{ AWFUL, CRAP, NORMAL, GOOD, GREAT, BRILLIANT, MAGNIFICENT, UNIQUE }
public enum WeaponPrefix			{ NULL, UNDEAD_SLAYING, ABERRATION_ANNIHILATING, BEAST_BLASTING, MAN_MURDERING, CRITTER_CRUSHING };
public enum WeaponSuffix			{ NULL, MANA_REGENERATION, HEALTH_REGENERATION, FORTITUDE, MANA_CAPACITY }; 	// TODO: There can be more than one of these added to the var with this information needs to be an array!
public enum WeaponType				{ SHOTGUN, RIFLE, PISTOL, FLAMETHROWER, FROSTTHROWER, UZI, SUBMACHINE_GUN, MUSKET };
public enum WeaponProjectileType	{ NULL, PIERCING, BURNING, FREEZING, HEALING } 											// TODO: There can be more than one of these added to the var with this information needs to be an array!