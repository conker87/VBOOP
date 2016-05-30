﻿using UnityEngine;
using System.Collections;

public class SpreadWeapon : Weapon {

	[Header("Overriden Gun Settings")]
	public Projectile projectile;

	[Range(0f, 25f)]
	public float bulletSpread = 5f;

	protected override void Start() {

		base.Start ();

		weaponFireType = WeaponFireType.SPREAD;

		// You can override the start method to force weapons to be certain qualities and have certain attributes.

	}

	protected override void OverrideShoot (Transform loc)
	{

		Quaternion fireRotation = loc.rotation, randomRotaton = Random.rotation;

		fireRotation = Quaternion.RotateTowards(fireRotation, randomRotaton, Random.Range(0.0f, bulletSpread));

		Projectile newProjectile = Instantiate (projectile, loc.position, fireRotation) as Projectile;

		newProjectile.Speed = projectileVelocity;

		if (shouldDamageBeCalculated) {
			float gunDamageThisShot = Random.Range (projectileMinimumDamage, projectileMaximumDamage);
			damagePerProjectile = gunDamageThisShot / projectilesPerShot;
		}

		newProjectile.Damage = damagePerProjectile;
		newProjectile.Lifetime = 5f;

		newProjectile.IsPiercing	= (weaponProjectileType == WeaponProjectileType.PIERCING) ? true : false;
		newProjectile.IsBurning		= (weaponProjectileType == WeaponProjectileType.BURNING) ? true : false;
		newProjectile.IsFreezing 	= (weaponProjectileType == WeaponProjectileType.FREEZING) ? true : false;

	}

}