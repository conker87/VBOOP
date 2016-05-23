using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour {

	public Transform weaponLocation;

	public float health, mana;
	public WeaponBase startingWeapon;
	WeaponBase equippedWeapon;

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

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
