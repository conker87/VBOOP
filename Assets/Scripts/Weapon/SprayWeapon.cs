using UnityEngine;
using System.Collections;

public class SprayWeapon : Weapon {

	[Header("Overriden Gun Settings")]
	public Projectile projectile;

	protected override void Start() {

		base.Start ();

		weaponFireType = WeaponFireType.SPRAY;

		// You can override the start method to force weapons to be certain qualities and have certain attributes.

	}

	protected override void OverrideShoot (Transform loc)
	{
				
		Quaternion fireRotation = loc.rotation;

		Projectile newProjectile = Instantiate (projectile, loc.position, fireRotation) as Projectile;
		newProjectile.Speed = projectileVelocity;

		//TODO: Spray weapons need their own way of dealing with damage due to them being more of a continuous DoT than anything else;

	}

}
