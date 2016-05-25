﻿using UnityEngine;
using System.Collections;

public class SingleShotWeapon : WeaponBase {

	[Header("Overriden Gun Settings")]
	public Projectile projectile;

	protected override void Start() {

		base.Start ();

		weaponFireType = WeaponFireType.SINGLE_SHOT;

		// You can override the start method to force weapons to be certain qualities and have certain attributes.

	}

	protected override void OverrideShoot (Transform loc)
	{

		Quaternion fireRotation = loc.rotation;

		Projectile newProjectile = Instantiate (projectile, loc.position, fireRotation) as Projectile;
		newProjectile.SetSpeed (projectileVelocity);

		if (shouldDamageBeCalculated) {
			float gunDamageThisShot = Random.Range (projectileMinimumDamage, projectileMaximumDamage);
			damagePerProjectile = gunDamageThisShot / projectilesPerShot;

			newProjectile.SetDamage (damagePerProjectile);
			newProjectile.SetLifetime (5f);

			newProjectile.SetIsPiercing((weaponProjectileType == WeaponProjectileType.PIERCING) ? true : false);
			newProjectile.SetIsBurning((weaponProjectileType == WeaponProjectileType.BURNING) ? true : false);
			newProjectile.SetIsFreezing((weaponProjectileType == WeaponProjectileType.FREEZING) ? true : false);

		}

	}

}
