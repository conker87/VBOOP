using UnityEngine;
using System;
using System.Collections;

public class EffectDamageOverTime : Effect {

	public int value;

	public override void DoEffect() {

		if (disabled == false) {

			if (Time.time > TimeUntilNextTick) {

				entity.Damage (value);

				TimeUntilNextTick = Time.time + 1f;

			}

		}

	}

}