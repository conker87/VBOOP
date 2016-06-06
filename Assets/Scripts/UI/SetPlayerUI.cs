using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetPlayerUI : MonoBehaviour {

	public GameObject playerExperiencePanel, playerHealthPanel, playerManaPanel;
	public Text playerExperienceActual, playerHealthActual, playerHealthPercentage, playerManaActual, playerManaPercentage;

	// Use this for initialization
	void SetResourceBar(GameObject panel, float currentResource, float maximumResource) {

		if (panel != null) {

			if (maximumResource == 0) {

				float zero = 0f;
				Vector3 newLocalScale = new Vector3 (zero, 1f, 1f);

				panel.transform.localScale = newLocalScale;

			}

			if (maximumResource > 0) {

				float percentage = currentResource / maximumResource;
				Vector3 newLocalScale = new Vector3 (percentage, 1f, 1f);

				panel.transform.localScale = newLocalScale;

			}

		}
	}

	void SetResourceBarText(Text panel, string value, float maximumResource, string format = "") {

		if (panel != null) {

			if (maximumResource > 0) {

				panel.text = (format == "") ? value : string.Format(format, value);

			}

		}

	}
	
	// Update is called once per frame
	void Update () {
	
		SetResourceBar		(playerExperiencePanel,		Player.current.CurrentExperience,		Player.current.TotalExperienceNeededToLevel);
		SetResourceBarText	(playerExperienceActual, 	Player.current.CurrentExperience.ToString("N0"),	Player.current.TotalExperienceNeededToLevel);

		SetResourceBar		(playerHealthPanel,			Player.current.CurrentHealth, 			Player.current.MaximumHealth);
		SetResourceBarText	(playerHealthActual, 		Player.current.CurrentHealth.ToString("N0"),		Player.current.MaximumHealth);
		SetResourceBarText	(playerHealthPercentage, 	(Player.current.CurrentHealth / Player.current.MaximumHealth).ToString("P"),
																								Player.current.MaximumHealth);

		SetResourceBar		(playerManaPanel,			Player.current.CurrentMana,				Player.current.MaximumMana);
		SetResourceBarText	(playerManaActual, 			Player.current.CurrentMana.ToString("N0"),			Player.current.MaximumMana);
		SetResourceBarText	(playerManaPercentage, 		(Player.current.CurrentMana / Player.current.MaximumMana).ToString("P"),
																								Player.current.MaximumMana);



	}
}
