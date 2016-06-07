using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EffectSlots : MonoBehaviour {

	Entity currentEntity;
	protected float originalTime;

	public List<Effect> buffs	= new List<Effect>();
	public List<Effect> debuffs	= new List<Effect>();

	List<Effect> buffsToRemove		= new List<Effect>();
	List<Effect> debuffsToRemove	= new List<Effect>();

	public List<Effect> testingBuffs = new List<Effect>();

	void Start() {

		currentEntity = GetComponent<Entity> ();

	}

	public void Add(Effect effect, EffectType effectType) {

		if (effectType == EffectType.BUFF) {

			foreach (Effect buff in buffs) {

				if (buff.SourceWeapon == effect.SourceWeapon) {

					return;

				} 
		
			}

			buffs.Add (effect);

		} else if (effectType == EffectType.DEBUFF) {

			foreach (Effect debuff in debuffs) {

				if (debuff.SourceWeapon == effect.SourceWeapon) {

					return;

				} 

			}

			debuffs.Add (effect);

		} 

	}

	void Update () {

		foreach (Effect buff in buffs) {

			if (buff != null) {

				for (int i = 0; i < buff.TempTicker.Length; i++) {

					if (Time.time > buff.TempTicker[i]) {

						buff.DoEffect[i].Invoke ();

						buff.TempTicker[i] = Time.time + buff.TimeUntilNextTick[i];

					}

				}

				if (Time.time > buff.OriginalTime + buff.EffectDuration) {

					buffsToRemove.Add (buff);

					if (buff.EndEffect != null) {

						foreach (Action endEffect in buff.EndEffect) {

							endEffect.Invoke ();

						}

					}

				}

			}

		}

		foreach (Effect debuff in debuffs) {

			if (debuff != null) {

				for (int i = 0; i < debuff.TempTicker.Length; i++) {

					if (Time.time > debuff.TempTicker[i]) {

						debuff.DoEffect[i].Invoke ();

						debuff.TempTicker[i] = Time.time + debuff.TimeUntilNextTick[i];

					}

				}

				if (Time.time > debuff.OriginalTime + debuff.EffectDuration) {

					debuffsToRemove.Add (debuff);

					if (debuff.EndEffect != null) {
						
						foreach (Action endEffect in debuff.EndEffect) {

							endEffect.Invoke ();

						}

					}

				}

			}

		}

		foreach (Effect buffToRemove in buffsToRemove) {
			
			buffs.Remove(buffToRemove);

		}

		foreach (Effect debuffToRemove in debuffsToRemove) {
			
			debuffs.Remove(debuffToRemove);

		}

		buffsToRemove.Clear ();
		debuffsToRemove.Clear ();
	}
}
