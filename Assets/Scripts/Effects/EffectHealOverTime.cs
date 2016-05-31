using UnityEngine;
using System;
using System.Collections;

public class EffectHealOverTime : Effect {

	public int value;
	float currentTime;

	public override void DoEffect() {

		if (disabled == false) {

			if (Time.time > currentTime) {

				entity.Heal (value);

				currentTime = Time.time + TimeUntilNextTick;

			}

		}

	}

}