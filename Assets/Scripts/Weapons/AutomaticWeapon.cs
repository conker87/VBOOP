using UnityEngine;
using System.Collections;

public class AutomaticWeapon : WeaponBase {

	[Header("Overriden Gun Settings")]
	public Projectile projectile;

	protected override void Start() {

		base.Start ();

		weaponFireType = WeaponFireType.AUTOMATIC;

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

			if (false) {
			
				Debug.Log ("SpreadWeapon::OverrideShot (Override) -- gunDamageThisShot: " + gunDamageThisShot + ", damagePerProjectile: " + damagePerProjectile);

			}
		}

	}

}