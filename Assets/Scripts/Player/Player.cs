using UnityEngine;
using System.Collections;

public class Player : Entity {

	public static Player current { get; protected set; }

	public int currentLevel, currentXP;

	/// Temporary value, this should be got via passing currentLevel to a shared method that calculates the total xp needed depending on their level.
	int totalXPForLevel;

	void Start () {

		current = this;

		totalXPForLevel = XPNeededToLevel (GetCurrentLevel());

	}

	public int GetCurrentLevel() {

		return currentLevel;

	}

	public void GainLevel() {

		currentLevel++;

	}

	/// This really should not be used an should probably not ship to prevent exploits.
	public void SetCurrentLevel(int _currentLevel) {

		currentLevel = _currentLevel;

	}

	public int GetCurrentXP() {

		return currentXP;

	}

	public void SetCurrentXP(int _currentXP) {

		currentXP = _currentXP;

	}

	public void AmendCurrentXP(int _currentXP) {

		currentXP += _currentXP;

		if (GetCurrentXP () >= totalXPForLevel) {

			DoLevelUp (GetCurrentXP (), totalXPForLevel);

		}

	}

	public void DoLevelUp(int _currentXP, int totalXP) {

		GainLevel ();

		// Sort out new XP gained.
		int overflow = _currentXP - totalXP;
		SetCurrentXP (overflow);

		// Set the XP needed to level to the calculated value.
		totalXPForLevel = XPNeededToLevel (GetCurrentLevel());

	}

	public int GetTotalXPForLevel() {

		return totalXPForLevel;

	}

	void OnGUI() {
		
		GUI.Label(new Rect(10, 10, 200, 20), "XP: " + currentXP + "/" + totalXPForLevel + ". Level: " + currentLevel);

	}

	// TODO: This method.
	public int XPNeededToLevel (int _currentLevel) {

		return 100;

	}

}
