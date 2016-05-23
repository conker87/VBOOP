using UnityEngine;
using System.Collections;

public class SprayWeapon : WeaponBase {

	[Header("Overriden Gun Settings")]
	public Projectile projectile;

	void Start () {

		weaponType = WeaponType.SPRAY;

	}

	protected override void OverrideShoot (Transform loc)
	{
				
		Quaternion fireRotation = loc.rotation;

		Projectile newProjectile = Instantiate (projectile, loc.position, fireRotation) as Projectile;
		newProjectile.SetSpeed (projectileVelocity);

		//TODO: Spray weapons need their own way of dealing with damage due to them being more of a continuous DoT than anything else;

	}

}
