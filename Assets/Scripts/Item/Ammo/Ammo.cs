using UnityEngine;
using System.Collections;

public class Ammo : Item {

	public Ammo() {

	}

	// TODO: Ammo requires an array that contains EffectDuration and EffectAmount
	public Ammo(string _itemName, string _itemDescription, int _stackSize, AmmoType _ammoType) {

		ItemName = _itemName;
		ItemDescription = _itemDescription;
		StackSize = _stackSize;
		AmmoType = new AmmoType[] { _ammoType };

	}

	public Ammo(string _itemName, string _itemDescription, int _stackSize, AmmoType[] _ammoType) {

		ItemName = _itemName;
		ItemDescription = _itemDescription;
		StackSize = _stackSize;
		ammoType = _ammoType;

	}

	protected	AmmoType[] ammoType;
	public		AmmoType[] AmmoType		{ get {	return this.ammoType; }	set {	this.ammoType = value; } }

}

public enum AmmoType { BLEEDING, BURNING, FREEZING, HEALING, LEECHING, NONE, PIERCING, POISON }