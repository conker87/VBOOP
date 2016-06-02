using UnityEngine;
using System.Collections;

public class _Debug_ChangeWeaponOnNumbers : MonoBehaviour {

	public Weapon[] allGuns;

	public Player player;
	WeaponController weaponController;

	int currentGun = 0;

	void Start () {

		if (player != null) {

			weaponController = player.GetComponent<WeaponController> ();

		}

	}

	void Update () {

		if (allGuns != null) {

			if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
				
				currentGun--;
				if (currentGun < 0) {
					currentGun = allGuns.Length - 1;
				}

				weaponController.EquipWeaponOnPlayer (allGuns [currentGun]);

			} else if (Input.GetAxis ("Mouse ScrollWheel") > 0) {

				currentGun++;
				if (currentGun > allGuns.Length - 1) {
					currentGun = 0;
				}

				weaponController.EquipWeaponOnPlayer (allGuns [currentGun]);

			} else {

				if (Input.GetKeyDown (KeyCode.Alpha1)) {
					weaponController.EquipWeaponOnPlayer (allGuns [0]);
				} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
					weaponController.EquipWeaponOnPlayer (allGuns [1]);
				} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
					weaponController.EquipWeaponOnPlayer (allGuns [2]);
				} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
					weaponController.EquipWeaponOnPlayer (allGuns [3]);
				} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
					weaponController.EquipWeaponOnPlayer (allGuns [4]);
				} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
					weaponController.EquipWeaponOnPlayer (allGuns [5]);
				}

			}
		}

	}
}
