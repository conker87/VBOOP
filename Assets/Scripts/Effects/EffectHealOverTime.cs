using UnityEngine;
using System;
using System.Collections;

public class EffectHealOverTime : Effect {

	public float value;
	float currentTime;

	public override void DoEffect() {

		if (disabled == false) {

			if (Time.time > currentTime) {

				effectEntity.Heal (value);

				currentTime = Time.time + TimeUntilNextTick;

			}

		}

	}

}