using UnityEngine;
using System.Collections;

public class SingleShotWeapon : Weapon {

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
		newProjectile.Speed = projectileVelocity;

		if (shouldDamageBeCalculated) {
			float gunDamageThisShot = Random.Range (projectileMinimumDamage, projectileMaximumDamage);
			damagePerProjectile = gunDamageThisShot / projectilesPerShot;

			newProjectile.ProjectileDamage = damagePerProjectile;
			newProjectile.WeaponAverageDamage = (projectileMinimumDamage + projectileMaximumDamage) / 2;
			newProjectile.Lifetime = 5f;

			newProjectile.IsPiercing	= (weaponProjectileType == WeaponProjectileType.PIERCING) ? true : false;
			newProjectile.IsBurning		= (weaponProjectileType == WeaponProjectileType.BURNING) ? true : false;
			newProjectile.IsFreezing 	= (weaponProjectileType == WeaponProjectileType.FREEZING) ? true : false;

		}

	}

}
