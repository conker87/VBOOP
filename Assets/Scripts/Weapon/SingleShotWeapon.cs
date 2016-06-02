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

	protected override void OverrideShoot (Transform loc, out Projectile newProjectile)
	{

		Quaternion fireRotation = loc.rotation;

		newProjectile = Instantiate (projectile, loc.position, fireRotation) as Projectile;
		newProjectile.Speed = projectileVelocity;

	}

}
