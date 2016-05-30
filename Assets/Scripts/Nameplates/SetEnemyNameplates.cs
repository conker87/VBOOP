using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetEnemyNameplates : MonoBehaviour {

	public GameObject nameNameplate, titleNameplate;
	public GameObject healthPanel, manaPanel;
	public GameObject healthPanelPercetage, healthPanelActual, manaPanelPercetage, manaPanelActual;

	Enemy enemy;

	bool dontSpamLog = true;

	float currentHealth, maximumHealth, currentMana, maximumMana;

	void SetNameplateText() {

		if (enemy != null) {

			if (nameNameplate != null) {

				if (enemy.EntityName != "") {

					nameNameplate.GetComponent<Text>().text = enemy.EntityName;

				} else {

					nameNameplate.GetComponent<Text>().text = gameObject.name;

				}

			}

			if (titleNameplate != null) {

				if (enemy.EntityTitle != "") {

					titleNameplate.GetComponent<UnityEngine.UI.Text>().text = enemy.EntityTitle;

				} else {

					titleNameplate.GetComponent<UnityEngine.UI.Text>().text = enemy.species.ToString ();

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

		enemy = GetComponent<Enemy> ();

		maximumHealth = enemy.MaximumHealth;
		maximumMana = enemy.MaximumMana;

		SetNameplateText ();

	}

	void Update() {

		currentHealth = enemy.CurrentHealth;
		currentMana = enemy.CurrentMana;

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
