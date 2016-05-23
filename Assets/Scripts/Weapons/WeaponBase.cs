using UnityEngine;
using System.Collections;
using System.Linq;

public abstract class WeaponBase : MonoBehaviour {

	[Header("On Player Location")]
	public Transform[] shootFromLocation;
	public Vector3 localPositionOffset = Vector3.zero;

	// Gun details
	[Header("Damage Settings")]
	public float attackSpeed = 1f;
	public WeaponType weaponType;
	public float projectilesPerShot = 1f, projectilesPerClip = 8f;
	[Space(5)]
	public float projectileMinimumDamage = 5f, projectileMaximumDamage = 10f;
	protected float damagePerProjectile;
	[SerializeField]
	float damagePerSecond;

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