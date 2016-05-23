using UnityEngine;
using System.Collections;
using System.Linq;

public abstract class WeaponBase : MonoBehaviour {

	[Header("On Player Location")]
	public Transform[] shootFromLocation;
	public Vector3 localPositionOffset = Vector3.zero;

	// Gun details
	[Header("Gun Settings")]
	public float timeBetweenNextShots = 100f;
	public WeaponType weaponType;

	// Projectile settings
	[Header("Projectile Settings")]
	[Range(5f, 250f)]
	public float projectileVelocity = 35f;
	public float projectilesPerShot = 1f, projectilesPerClip = 8f;
	[Space(5)]
	public float projectileMinimumDamage = 5f, projectileMaximumDamage = 10f;
	protected float damagePerProjectile;

	protected bool shouldDamageBeCalulated = true;

	float nextShotTime;

	protected abstract void OverrideShoot (Transform loc);
	//protected abstract void OverrideShoot (Transform loc);

	public virtual void Shoot () {

		if (Time.time > nextShotTime) {

			shouldDamageBeCalulated = true;

				nextShotTime = Time.time + timeBetweenNextShots / 1000;

				for (int i = 0; i < projectilesPerShot; i++) {

					foreach (Transform loc in shootFromLocation) {

						OverrideShoot (loc);

					}
				}

				Debug.Log ("Weapon::Shoot -- Shooting.");

			}

	}

	public void SetTimeBetweenNextShots(float _timeBetweenNextShots) {

		timeBetweenNextShots = _timeBetweenNextShots;

	}

	public void SetProjectileVelocity(float _projectileVelocity) {

		projectileVelocity = _projectileVelocity;

	}

}

public enum WeaponType { SINGLE_SHOT, SEMI_AUTOMATIC, AUTOMATIC, SPREAD, SPRAY, PROJECTILE };