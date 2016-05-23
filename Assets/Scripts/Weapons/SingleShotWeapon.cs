using UnityEngine;
using System.Collections;

public class SingleShotWeapon : WeaponBase {

	[Header("Overriden Gun Settings")]
	public Projectile projectile;

	protected override void OverrideShoot (Transform loc)
	{

		Quaternion fireRotation = loc.rotation;

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
