using UnityEngine;
using System.Collections;

public class SetEnemyNameplates : MonoBehaviour {

	public GameObject nameNameplate, speciesNameplate;
	public GameObject healthPanel, manaPanel;

	EnemyBase enemyBase;

	void Start () {
	
		enemyBase = GetComponent<EnemyBase> ();

		if (enemyBase != null) {

			if (nameNameplate != null) {

				if (enemyBase.name != "") {
					
					nameNameplate.GetComponent<UnityEngine.UI.Text>().text = enemyBase.enemyName;

				} else {

					nameNameplate.GetComponent<UnityEngine.UI.Text>().text = "!ERROR! " + gameObject.name + " has no name!";

					Debug.LogError ("SetEnemyNameplates::Start() -- !ERROR! " + gameObject.name + " has no name!");

				}

			}

			if (speciesNameplate != null) {

				if (enemyBase.enemyNameSub != "") {
					
					speciesNameplate.GetComponent<UnityEngine.UI.Text>().text = enemyBase.enemyNameSub;

				} else {

					speciesNameplate.GetComponent<UnityEngine.UI.Text>().text = enemyBase.enemySpecies.ToString ();

				}
			}
		}

	}

	void SetResourceBar(GameObject panel, float currentResource, float maximumResource) {

		if (panel != null) {

			float percentage = currentResource / maximumResource;

			Vector3 newLocalScale = new Vector3 (percentage, 1f, 1f);

			panel.transform.localScale = newLocalScale;

		}

	}

	void Update() {

		SetResourceBar (healthPanel, enemyBase.GetCurrentHealth (), enemyBase.GetMaximumHealth ());
		SetResourceBar (manaPanel, enemyBase.GetCurrentMana (), enemyBase.GetMaximumMana ());

	}

}
