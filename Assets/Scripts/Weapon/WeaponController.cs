using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	//Weapon equippedWeapon;

	public bool shouldWeaponScaleToPlayer = false;

	public Transform weaponLocation;
	public Weapon startingWeapon;

	void Start ()
	{
		if (startingWeapon != null) {
			EquipWeapon (startingWeapon);
		}
	}

	public void EquipWeapon(Weapon weaponToEquip) {

		if (Player.current.equippedWeapon != null) {
			Destroy (Player.current.equippedWeapon.gameObject);
		}
		Player.current.equippedWeapon = Instantiate (weaponToEquip, weaponLocation.position, weaponLocation.rotation) as Weapon;
		Player.current.equippedWeapon.transform.parent = weaponLocation;

		if (shouldWeaponScaleToPlayer) {
			Player.current.equippedWeapon.transform.localScale = Player.current.equippedWeapon.transform.parent.localScale;
		}

	}

	void Update ()
	{
		//Debug.Log (equippedWeapon.transform.localPosition.ToString());
	}

	public void Shoot() {

		if (Player.current.equippedWeapon != null) {
			Player.current.equippedWeapon.Shoot ();
		}

	}

	//public WeaponFireType GetWeaponFireType() {
	//	return Player.current.equippedWeapon.weaponFireType;
	//}

	public void SetAttackSpeed(float _time) {

		if (Player.current.equippedWeapon != null) {
			
			Player.current.equippedWeapon.SetAttackSpeed (_time);

		}

	}

	public void SetProjectileVelocity(float velocity) {

		if (Player.current.equippedWeapon != null) {
			
			Player.current.equippedWeapon.SetProjectileVelocity (velocity);

		}

	}
}
