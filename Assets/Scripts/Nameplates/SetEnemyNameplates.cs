using UnityEngine;
using System.Collections;

public class SetEnemyNameplates : MonoBehaviour {

	public UnityEngine.UI.Text nameNameplate, speciesNameplate;

	EnemyBase enemyBase;

	void Start () {
	
		enemyBase = GetComponent<EnemyBase> ();

		if (enemyBase != null) {

			if (nameNameplate != null) {

				nameNameplate.text = enemyBase.enemyName;

			}

			if (speciesNameplate != null) {

				if (enemyBase.enemyNameSub != "") {
					
					speciesNameplate.text = enemyBase.enemyNameSub;

				} else {

					speciesNameplate.text = enemyBase.enemySpecies.ToString ();

				}

			}

		}

	}

}
