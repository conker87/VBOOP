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

	}

}
