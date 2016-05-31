using UnityEngine;
using System.Collections;

public class Player : Entity {

	public static Player current { get; protected set; }

	protected override void Start () {

		base.Start();

		current = this;

		TotalExperienceNeededToLevel = XPNeededToLevel (CurrentLevel);

	}

	protected override void Update() {

		base.Update();

	}

	public void AmendCurrentXP(int _currentExperience) {

		CurrentExperience += _currentExperience;

		if (CurrentExperience >= TotalExperienceNeededToLevel) {

			DoLevelUp (CurrentExperience, TotalExperienceNeededToLevel);

		}

	}

	public void DoLevelUp(int _currentXP, int totalXP) {

		GainLevel ();

		// Sort out new XP gained.
		int overflow = _currentXP - totalXP;
		CurrentExperience = overflow;

		// Set the XP needed to level to the calculated value.
		TotalExperienceNeededToLevel = XPNeededToLevel (CurrentLevel);

	}

	protected void GainLevel() {

		currentLevel++;

	}

	void OnGUI() {
		
		GUI.Label(new Rect(10, 10, 200, 20), "XP: " + CurrentExperience + "/" + TotalExperienceNeededToLevel + ". Level: " + currentLevel);

	}

	// TODO: This method.
	public int XPNeededToLevel (int _currentLevel) {

		return 100;

	}

}
