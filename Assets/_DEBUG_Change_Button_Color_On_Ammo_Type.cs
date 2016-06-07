using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class _DEBUG_Change_Button_Color_On_Ammo_Type : MonoBehaviour {

	public AmmoType _ammoType;
	Image button;

	// Use this for initialization
	void Start () {
	
		button = GetComponent<Image> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		ChangeOnAmmoType ();

	}

	void ChangeOnAmmoType() {

		if (button != null && Player.current.EquippedAmmo.AmmoType != null) {

			if (Player.current.EquippedAmmo.AmmoType == _ammoType) {

				button.color = Color.green;

			} else { 

				button.color = Color.white;

			}
		}
	}
}
