using UnityEngine;
using System.Collections;

public class SetEnemyNameplates : MonoBehaviour {

	public GameObject nameNameplate, titleNameplate;
	public GameObject healthPanel, manaPanel;

	EnemyBase enemyBase;

	void Start () {
	
		enemyBase = GetComponent<EnemyBase> ();

		if (enemyBase != null) {

			if (nameNameplate != null) {

				if (enemyBase.name != "") {
					
					nameNameplate.GetComponent<UnityEngine.UI.Text>().text = enemyBase.name;

				} else {

					nameNameplate.GetComponent<UnityEngine.UI.Text>().text = "!ERROR! " + gameObject.name + " has no name!";

					Debug.LogError ("SetEnemyNameplates::Start() -- !ERROR! " + gameObject.name + " has no name!");

				}

			}

			if (titleNameplate != null) {

				if (enemyBase.title != "") {
					
					titleNameplate.GetComponent<UnityEngine.UI.Text>().text = enemyBase.title;

				} else {

					titleNameplate.GetComponent<UnityEngine.UI.Text>().text = enemyBase.species.ToString ();

				}
			}
		}

	}

	void SetResourceBar(GameObject panel, float currentResource, float maximumResource) {

		if (panel != null) {

			if (maximumResource == 0) {

				float zero = 0f;
				Vector3 newLocalScale = new Vector3 (zero, 1f, 1f);
				panel.transform.localScale = newLocalScale;

				return;

			}

			if (maximumResource > 0) {

				float percentage = currentResource / maximumResource;
				Vector3 newLocalScale = new Vector3 (percentage, 1f, 1f);
				panel.transform.localScale = newLocalScale;

			}

		}

	}

	void Update() {

		SetResourceBar (healthPanel, enemyBase.GetCurrentHealth (), enemyBase.GetMaximumHealth ());
		SetResourceBar (manaPanel, enemyBase.GetCurrentMana (), enemyBase.GetMaximumMana ());

	}

}
