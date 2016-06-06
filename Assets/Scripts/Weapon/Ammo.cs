using UnityEngine;
using System.Collections;

public class Ammo : Item {

	protected AmmoType ammoType;
	public AmmoType AmmoType		{ get {	return this.ammoType; }	set {	this.ammoType = value; } }

	void Start() {

		StackSize = 1;

	}

}

public enum AmmoType { NORMAL, BURNING, FREEZING, HEALING, POISON, LEECHING }