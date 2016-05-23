using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	WeaponBase equippedWeapon;

	public bool shouldWeaponScaleToPlayer = false;

	public Transform weaponLocation;
	public WeaponBase startingWeapon;

	void Start ()
	{
		if (startingWeapon != null) {
			EquipWeapon (startingWeapon);
		}
	}

	public void EquipWeapon(WeaponBase weaponToEquip) {

		if (equippedWeapon != null) {
			Destroy (equippedWeapon.gameObject);
		}
		equippedWeapon = Instantiate (weaponToEquip, weaponLocation.position, weaponLocation.rotation) as WeaponBase;
		equippedWeapon.transform.parent = weaponLocation;

		//equippedWeapon.transform.localPosition += weaponToEquip.localPositionOffset;

		if (shouldWeaponScaleToPlayer) {
			equippedWeapon.transform.localScale = equippedWeapon.transform.parent.localScale;
		}

	}

	void Update ()
	{
		//Debug.Log (equippedWeapon.transform.localPosition.ToString());
	}

	public void Shoot() {

		if (equippedWeapon != null) {
			equippedWeapon.Shoot ();
		}

	}

	public WeaponType GetWeaponType() {
		return equippedWeapon.weaponType;
	}

	public void SetAttackSpeed(float _time) {

		if (equippedWeapon != null) {
			
			equippedWeapon.SetAttackSpeed (_time);

		}

	}

	public void SetProjectileVelocity(float velocity) {

		if (equippedWeapon != null) {
			
			equippedWeapon.SetProjectileVelocity (velocity);

		}

	}
}
