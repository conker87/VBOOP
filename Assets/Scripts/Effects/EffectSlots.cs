using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectSlots : MonoBehaviour {

	Entity currentEntity;

	public List<Effect> buffs	= new List<Effect>();
	public List<Effect> debuffs	= new List<Effect>();

	void Start() {

		currentEntity = GetComponentInParent<Entity> ();

		Debug.Log (currentEntity);

	}

	void Update () {
	
		foreach (Effect buff in buffs) {

			buff.DoEffect ();

		}

		foreach (Effect debuff in debuffs) {

			debuff.DoEffect ();

		}

	}
}
