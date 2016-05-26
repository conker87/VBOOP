using UnityEngine;
using System.Collections;

public class SetEnemyNameplates : MonoBehaviour {

	public UnityEngine.UI.Text nameNameplate, speciesNameplate;

	EnemyBase enemyBase;

	void Start () {
	
		enemyBase = GetComponent<EnemyBase> ();

		if (enemyBase != null) {

			if (nameNameplate != null) {

				if (enemyBase.name != "") {
					
					nameNameplate.text = enemyBase.enemyName;

				} else {

					nameNameplate.text = "!ERROR! " + gameObject.name + " has no name!";

					Debug.LogError ("SetEnemyNameplates::Start() -- !ERROR! " + gameObject.name + " has no name!");

				}

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
