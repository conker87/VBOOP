using UnityEngine;
using System.Collections;
using System.Linq;

[ExecuteInEditMode]
public abstract class WeaponBase : MonoBehaviour {

	[Header("Location Settings")]
	public Transform[] shootFromLocation;

	// Weapon Details
	[Header("Gun Details")]
	public WeaponType weaponType;
	public WeaponQuality weaponQuality;
	public WeaponPrefix weaponPrefix;
	public WeaponSuffix[] weaponSuffix;
	public WeaponProjectileType weaponProjectileType;

	// Damage Settings
	[Header("Damage Settings")]
	[Range(0.0001f, 20f)]
	public float attackSpeed = 1f;
	public int projectilesPerShot = 1, projectilesPerClip = 8;
	[Space(5)]
	public float projectileMinimumDamage = 5f;
	public float projectileMaximumDamage = 10f;
	[Space(5)]
	[SerializeField]
	float damagePerSecond;

	protected float damagePerProjectile;

	// Projectile settings
	[Header("Projectile Settings")]
	[Range(5f, 250f)]
	public float projectileVelocity = 35f;


	protected bool shouldDamageBeCalculated = true;

	float nextShotTime;

	protected abstract void OverrideShoot (Transform loc);
	//protected abstract void OverrideShoot (Transform loc);

	void Update () {

		damagePerSecond = ( (1 / attackSpeed) * ( ( projectileMinimumDamage + projectileMaximumDamage ) / 2 ) ) * projectilesPerShot;

	}

	public virtual void Shoot () {

		Debug.Log ("Time: " + Time.time + ", nextShotTime: " + nextShotTime);

		if (Time.time > nextShotTime) {

			shouldDamageBeCalculated = true;

			nextShotTime = Time.time + attackSpeed;

				for (int i = 0; i < projectilesPerShot; i++) {

					foreach (Transform loc in shootFromLocation) {

						OverrideShoot (loc);

						shouldDamageBeCalculated = false;
					}
				}

				Debug.Log ("Weapon::Shoot -- Shooting.");

			}

	}

	public void SetAttackSpeed(float _attackSpeed) {

		attackSpeed = _attackSpeed;

	}

	public void SetProjectileVelocity(float _projectileVelocity) {

		projectileVelocity = _projectileVelocity;

	}

}

public enum WeaponType { SINGLE_SHOT, SEMI_AUTOMATIC, AUTOMATIC, SPREAD, SPRAY, PROJECTILE };
public enum WeaponQuality { AWFUL, CRAP, NORMAL, GOOD, GREAT, BRILLIANT, MAGNIFICENT, UNIQUE }
public enum WeaponPrefix { UNDEAD_SLAYING, ABERRATION_ANNIHILATION, BEAST_BLASTING, MAN_MURDERING, CRITTER_CRUSHING, HEALING, NULL };
public enum WeaponSuffix { MANA_REGENERATION, HEALTH_REGENERATION, FORTITUDE, MANA_CAPACITY, NULL }; //TODO: There can be more than one of these added to the var with this information needs to be an array!
public enum WeaponProjectileType { PIERCING, NULL }