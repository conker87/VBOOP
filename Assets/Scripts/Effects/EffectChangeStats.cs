using UnityEngine;
using System;
using System.Collections;

public class EffectChangeStats : Effect {

	public EntityStat stat;
	public int value;

	public override void DoEffect() {

		if (disabled == false) {

			Debug.Log ("DoEffect");

			entity.BuffStat (stat, value);

			disabled = true;

		}

	}

	public override void EndEffect () {

		entity.BuffStat(stat, -value);

		base.EndEffect ();
	}

}