using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetNameplates : MonoBehaviour {

	public bool isEnabled = true;

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
	public float CurrentHealth				{ get {	return this.currentHealth; }	set {	this.currentHealth = value; } }
	public float MaximumHealth				{ get {	return this.maximumHealth; }	set {	this.maximumHealth = value; } }
	public float CurrentMana				{ get {	return this.currentMana; }		set {	this.currentMana = value; } }
	public float MaximumMana				{ get {	return this.maximumMana; }	set {	this.maximumMana = value; } }

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

		SetNameplateText ();

	}

	void Update() {

		if (canvasParent != null) {

			canvasParent.SetActive (isEnabled);

		}

		if (isEnabled) {

			// Prevents the var being constantly updated.
			if (CurrentHealth != entity.CurrentHealth)	{ CurrentHealth = entity.CurrentHealth; }
			if (MaximumHealth != entity.MaximumHealth)	{ MaximumHealth = entity.MaximumHealth; }
			if (CurrentMana != entity.CurrentMana)		{ CurrentMana = entity.CurrentMana; }
			if (MaximumMana != entity.MaximumMana)		{ MaximumMana = entity.MaximumMana; }

			// Health
			SetResourceBar		(healthPanel,			CurrentHealth, 							MaximumHealth);
			SetResourceBarText	(healthPanelActual, 	CurrentHealth, 							MaximumHealth);
			SetResourceBarText	(healthPanelPercetage, 	(CurrentHealth / MaximumHealth) * 100,	MaximumHealth);

			// Mana
			SetResourceBar		(manaPanel, 			CurrentMana,							MaximumMana);
			SetResourceBarText 	(manaPanelActual, 		CurrentMana,							MaximumMana);
			SetResourceBarText 	(manaPanelPercetage, 	(CurrentMana / MaximumMana) * 100,		MaximumMana);

		}

	}

}
