using UnityEngine;
using System;
using System.Collections;

public class EffectChangeStats : Effect {

	public EntityStat stat;
	public int value;

	public override void DoEffect() {

		if (disabled == false) {

			Debug.Log ("DoEffect");

			effectEntity.BuffStat (stat, value);

			disabled = true;

		}

	}

	public override void EndEffect () {

		effectEntity.BuffStat(stat, -value);

		base.EndEffect ();
	}

}