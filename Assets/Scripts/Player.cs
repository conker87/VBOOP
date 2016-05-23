using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(WeaponController))]
public class Player : MonoBehaviour {

	public bool _DEBUG_FREEZE_ROTATION = false, _DEBUG_FREEZE_MOVEMENT = false;

	PlayerController controller;
	WeaponController weaponController;

	public static Vector3 raycastPoint;

	Camera viewCamera;

	public float moveSpeed = 5f;

	void Start () {
		
		controller = GetComponent<PlayerController> ();
		weaponController = GetComponent<WeaponController> ();
		viewCamera = Camera.main;

	}

	void Update () {

		// Movement
		Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		Vector3 moveVelocity = moveInput.normalized * moveSpeed;

		if (!_DEBUG_FREEZE_MOVEMENT) {
			controller.Move (moveVelocity);
		}

		// Look
		Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.zero);
		float rayDistance;

		if (groundPlane.Raycast(ray, out rayDistance)) {
			Vector3 point = ray.GetPoint (rayDistance);

			Debug.DrawLine (ray.origin, point, Color.red);

			if (!_DEBUG_FREEZE_ROTATION) {
				controller.LookAt (point);
			}

			raycastPoint = point;
		}

		// Weapon Input
		// { SINGLE_SHOT, SEMI_AUTOMATIC, AUTOMATIC, SPREAD, SPRAY, PROJECTILE };
		if (new []{ WeaponFireType.AUTOMATIC, WeaponFireType.SEMI_AUTOMATIC, WeaponFireType.SPRAY }.Contains (weaponController.GetWeaponFireType ())) {//weaponController.GetWeaponFireType() == WeaponFireType.AUTOMATIC || ) {
			if (Input.GetMouseButton (0)) {
				weaponController.Shoot ();
			}
		} else if (!(new []{ WeaponFireType.AUTOMATIC, WeaponFireType.SEMI_AUTOMATIC, WeaponFireType.SPRAY }.Contains (weaponController.GetWeaponFireType ()))) {
			if (Input.GetMouseButtonDown (0)) {
				weaponController.Shoot ();
			}
		}

	}
}

