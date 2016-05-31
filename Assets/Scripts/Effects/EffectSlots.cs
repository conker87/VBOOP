using UnityEngine;
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

		if (testingBuffs != null && currentEntity.tag == "Player") {

			foreach (Effect testingBuff in testingBuffs) {
				buffs.Add(Instantiate (testingBuff) as Effect);

			}

		}

	}

	void Update () {

		foreach (Effect buff in buffs) {

			if (buff != null) {

				if (buff.firstRun) {

					buff.disabled = false;
					buff.OriginalTime = Time.time;
					buff.firstRun = false;

				}

				buff.entity = currentEntity;
				buff.DoEffect ();

				if (Time.time > buff.OriginalTime + buff.EffectDuration) {

					buffsToRemove.Add (buff);
					buff.EndEffect();
					buff.OriginalTime = 0f;

				}
			}

		}
	

		foreach (Effect debuff in debuffs) {

			if (debuff != null) {

				if (debuff.firstRun) {

					debuff.OriginalTime = Time.time;
					debuff.firstRun = false;

				}

				debuff.entity = currentEntity;
				debuff.DoEffect ();

				if (Time.time > debuff.OriginalTime + debuff.EffectDuration) {

					debuffsToRemove.Add (debuff);
					debuff.EndEffect();
					debuff.OriginalTime = 0f;

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
