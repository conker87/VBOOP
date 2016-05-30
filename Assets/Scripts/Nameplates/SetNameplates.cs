using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetNameplates : MonoBehaviour {

	public bool enabled = true;

	[Header("Canvas Parent")]
	public GameObject canvasParent;

	[Header("Name & Title")]
	public GameObject nameNameplate;
	public GameObject titleNameplate;

	[Header("Health Descriptors")]
	public GameObject healthPanel;
	public GameObject healthPanelActual;
	public GameObject healthPanelPercetage;

	[Header("Mana Descriptors")]
	public GameObject manaPanel;
	public GameObject manaPanelActual;
	public GameObject manaPanelPercetage;

	Entity entity;

	bool dontSpamLog = true;

	float currentHealth, maximumHealth, currentMana, maximumMana;

	void SetNameplateText() {

		if (entity != null) {

			if (nameNameplate != null) {

				if (entity.EntityName != "") {

					nameNameplate.GetComponent<Text>().text = entity.EntityName;

				} else {

					nameNameplate.GetComponent<Text>().text = gameObject.name;

				}

			}

			if (titleNameplate != null) {

				if (entity.EntityTitle != "") {

					titleNameplate.GetComponent<UnityEngine.UI.Text>().text = entity.EntityTitle;

				} else {

					//titleNameplate.GetComponent<UnityEngine.UI.Text>().text = entity.species.ToString ();

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

		entity = GetComponentInParent<Entity> ();

		maximumHealth = entity.MaximumHealth;
		maximumMana = entity.MaximumMana;

		SetNameplateText ();

	}

	void Update() {

		if (canvasParent != null) {

			canvasParent.SetActive (enabled);

		}

		if (enabled) {

			currentHealth = entity.CurrentHealth;
			currentMana = entity.CurrentMana;

			// Health
			SetResourceBar (healthPanel, currentHealth, maximumHealth);
			SetResourceBarText (healthPanelActual, currentHealth, maximumHealth);
			SetResourceBarText (healthPanelPercetage, (currentHealth / maximumHealth) * 100, maximumHealth);

			// Mana
			SetResourceBar (manaPanel, currentMana, maximumMana);
			SetResourceBarText (manaPanelActual, currentMana, maximumMana);
			SetResourceBarText (manaPanelPercetage, (currentMana / maximumMana) * 100, maximumMana);

		}

	}

}
