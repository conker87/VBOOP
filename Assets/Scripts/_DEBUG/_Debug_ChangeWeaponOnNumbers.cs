﻿using UnityEngine;
using System.Collections;

public class _Debug_ChangeWeaponOnNumbers : MonoBehaviour {

	public WeaponBase[] allGuns;

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

				weaponController.EquipWeapon (allGuns [currentGun]);

			} else if (Input.GetAxis ("Mouse ScrollWheel") > 0) {

				currentGun++;
				if (currentGun > allGuns.Length - 1) {
					currentGun = 0;
				}

				weaponController.EquipWeapon (allGuns [currentGun]);

			} else {

				if (Input.GetKeyDown (KeyCode.Alpha1)) {
					weaponController.EquipWeapon (allGuns [0]);
				} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
					weaponController.EquipWeapon (allGuns [1]);
				} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
					weaponController.EquipWeapon (allGuns [2]);
				} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
					weaponController.EquipWeapon (allGuns [3]);
				} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
					weaponController.EquipWeapon (allGuns [4]);
				} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
					weaponController.EquipWeapon (allGuns [5]);
				}

			}
		}

	}
}