using UnityEngine;
using System.Collections;

public class Player : Entity {

	public static Player current { get; protected set; }

	protected override void Start () {

		base.Start();

		current = this;

		TotalExperienceNeededToLevel = XPNeededToLevel ();

		EquippedAmmo = new Ammo ("Burning Ammo", "Ammo that burns, mother fucker!", 1, new AmmoType[] { AmmoType.BURNING, AmmoType.PIERCING });

	}

	protected override void Update() {

		base.Update();

	}

	public void DoExperienceAndLevelGain(int experienceFromEntity) {

		CurrentExperience += experienceFromEntity;

		while (CurrentExperience >= TotalExperienceNeededToLevel) {

			CurrentLevel++;

			int overflow = Player.current.CurrentExperience - Player.current.TotalExperienceNeededToLevel;
			CurrentExperience = overflow;

			// Set the XP needed to level to the calculated value.
			TotalExperienceNeededToLevel = XPNeededToLevel ();

		}

	}

	void OnGUI() {
		
		GUI.Label(new Rect(10, 10, 200, 20), "XP: " + CurrentExperience + "/" + TotalExperienceNeededToLevel + ". Level: " + currentLevel);

	}

	// TODO: This method.
	public int XPNeededToLevel () {
		
		int levels = Player.current.maximumLevel;
		int xp_for_first_level = 1000;
		int xp_for_last_level = 1000000;

		float B = Mathf.Log(1f * xp_for_last_level / xp_for_first_level) / (levels - 1f);
		float A = 1f * xp_for_first_level / (Mathf.Exp(B) - 1f);

		int x = (int) (A * Mathf.Exp(B * Player.current.CurrentLevel));
		int y = (int) (10 * (Mathf.Log(x) / Mathf.Log(10) - 2.2f));

		return (int)(x / y) * y;

	}

}
