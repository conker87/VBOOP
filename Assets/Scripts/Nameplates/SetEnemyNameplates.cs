using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetEnemyNameplates : MonoBehaviour {

	public GameObject nameNameplate, titleNameplate;
	public GameObject healthPanel, manaPanel;
	public GameObject healthPanelPercetage, healthPanelActual, manaPanelPercetage, manaPanelActual;

	EnemyBase enemyBase;

	bool dontSpamLog = true;

	float currentHealth, maximumHealth, currentMana, maximumMana;

	void SetNameplateText() {

		if (enemyBase != null) {

			if (nameNameplate != null) {

				if (enemyBase.nameof != "") {

					nameNameplate.GetComponent<Text>().text = enemyBase.nameof;

				} else {

					nameNameplate.GetComponent<Text>().text = gameObject.name;

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

	void SetResourceBarText(GameObject panel, float value, float maximumResource) {

		if (panel != null) {

			if (maximumResource > 0) {

				panel.GetComponent<Text> ().text = value.ToString ();

			}

		}

	}

	void Start () {

		enemyBase = GetComponent<EnemyBase> ();

		maximumHealth = enemyBase.GetMaximumHealth ();
		maximumMana = enemyBase.GetMaximumMana ();

		SetNameplateText ();

	}

	void Update() {

		currentHealth = enemyBase.GetCurrentHealth ();
		currentMana = enemyBase.GetCurrentMana ();

		// Health
		SetResourceBar(healthPanel, currentHealth, maximumHealth);
		SetResourceBarText(healthPanelActual, currentHealth, maximumHealth);
		SetResourceBarText (healthPanelPercetage, (currentHealth / maximumHealth) * 100, maximumHealth);

		// Mana
		SetResourceBar (manaPanel, currentMana, maximumMana);
		SetResourceBarText(manaPanelActual, currentMana, maximumMana);
		SetResourceBarText (manaPanelPercetage, (currentMana / maximumMana) * 100, maximumMana);

	}

}
