/// <summary>
/// THIS FILE IS OBSELETE AS OF 22ND MAY, 2016.
/// </summary>

using UnityEngine;
using System.Collections;
using System.Linq;

public class Weapon : MonoBehaviour {
	/*
	public Transform[] shootFromLocation;
	public Projectile projectile;

	// Gun details
	public float projectilesPerShot = 1f;
	public float timeBetweenNextShots = 100f;

	// Projectile settings
	public float projectileVelocity = 35f;
	public float variance = 5f;

	public WeaponType weaponType = WeaponType.SINGLE_SHOT;

	float nextShotTime;

	public void Shoot () {
		
		if (Time.time > nextShotTime) {

			nextShotTime = Time.time + timeBetweenNextShots / 1000;

			Debug.Log ("Weapon::Shoot -- Shooting.");

			if (new []{ WeaponType.SPREAD }.Contains (weaponType)) {

				for (int i = 0; i < projectilesPerShot; i++) {
					
					foreach (Transform loc in shootFromLocation) {
						
						O2verrideShoot (true);

					}

				}
			}
		}

	}

	public void O2verrideShoot(bool useOffset) {

		foreach (Transform loc in shootFromLocation) {

			Quaternion fireRotation = loc.rotation, randomRotaton = Random.rotation;

			fireRotation = Quaternion.RotateTowards(fireRotation, randomRotaton, Random.Range(0.0f, variance));

			Projectile newProjectile = Instantiate (projectile, loc.position, fireRotation) as Projectile;
			newProjectile.SetSpeed (projectileVelocity);

		}

	}
		
	 public Quaternion ShootSpray (Vector3 aim, float distance, float variance) {

		aim.Normalize ();

		Debug.Log ("Weapon::ShootNormal -- " + aim.ToString());

		Vector3 v3;

		do {

			v3 = Random.insideUnitSphere;

		} while (v3 == aim || v3 == -aim);

		v3 = Vector3.Cross (aim, v3);
		v3 = v3 * Random.Range (0.0f, variance);

		return Quaternion.Euler(aim * distance + v3);

	} 

	public void SetTimeBetweenNextShots(float _timeBetweenNextShots) {

		timeBetweenNextShots = _timeBetweenNextShots;

	}

	public void SetProjectileVelocity(float _projectileVelocity) {

		projectileVelocity = _projectileVelocity;

	} */

}

///public enum WeaponType { SINGLE_SHOT, SEMI_AUTOMATIC, AUTOMATIC, SPREAD, SPRAY, PROJECTILE };