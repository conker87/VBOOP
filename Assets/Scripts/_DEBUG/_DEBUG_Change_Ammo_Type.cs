using UnityEngine;
using System.Collections;

public class _DEBUG_Change_Ammo_Type : MonoBehaviour {

	public void ChangeAmmoTypeToBleeding() {

		Player.current.EquippedAmmo = new Ammo ("Burning Ammo", "Ammo that burns, mother fucker!", 1, AmmoType.BLEEDING);

	}

	public void ChangeAmmoTypeToBurning() {

		Player.current.EquippedAmmo = new Ammo ("Burning Ammo", "Ammo that burns, mother fucker!", 1, AmmoType.BURNING);

	}

	public void ChangeAmmoTypeToFreezing() {

		Player.current.EquippedAmmo = new Ammo ("Burning Ammo", "Ammo that burns, mother fucker!", 1, AmmoType.FREEZING);

	}

	public void ChangeAmmoTypeToHealing() {

		Player.current.EquippedAmmo = new Ammo ("Burning Ammo", "Ammo that burns, mother fucker!", 1, AmmoType.HEALING);

	}

	public void ChangeAmmoTypeToLeeching() {

		Player.current.EquippedAmmo = new Ammo ("Burning Ammo", "Ammo that burns, mother fucker!", 1, AmmoType.LEECHING);

	}

	public void ChangeAmmoTypeToNone() {

		Player.current.EquippedAmmo = new Ammo ("Burning Ammo", "Ammo that burns, mother fucker!", 1, AmmoType.NONE);

	}

	public void ChangeAmmoTypeToPiercing() {

		Player.current.EquippedAmmo = new Ammo ("Burning Ammo", "Ammo that burns, mother fucker!", 1, AmmoType.PIERCING);

	}

	public void ChangeAmmoTypeToPoison() {

		Player.current.EquippedAmmo = new Ammo ("Burning Ammo", "Ammo that burns, mother fucker!", 1, AmmoType.POISON);

	}

}
