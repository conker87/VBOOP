using UnityEngine;
using System.Collections;

public class SpreadWeapon : WeaponBase {

	[Header("Overriden Gun Settings")]
	public Projectile projectile;

	[Range(0f, 25f)]
	public float bulletSpread = 5f;

	protected override void OverrideShoot (Transform loc)
	{

		Quaternion fireRotation = loc.rotation, randomRotaton = Random.rotation;

		fireRotation = Quaternion.RotateTowards(fireRotation, randomRotaton, Random.Range(0.0f, bulletSpread));

		Projectile newProjectile = Instantiate (projectile, loc.position, fireRotation) as Projectile;

		newProjectile.SetSpeed (projectileVelocity);

		if (shouldDamageBeCalulated) {
			float gunDamageThisShot = Random.Range (projectileMinimumDamage, projectileMaximumDamage);
			damagePerProjectile = gunDamageThisShot / projectilesPerShot;

			newProjectile.SetDamage (damagePerProjectile);

			Debug.Log ("SpreadWeapon::OverrideShot (Override) -- gunDamageThisShot: " + gunDamageThisShot + ", damagePerProjectile: " + damagePerProjectile);
		}

	}

}
