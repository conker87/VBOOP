using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int currentLevel, currentXP;

	/// Temporary value, this should be got via passing currentLevel to a shared method that calculates the total xp needed depending on their level.
	int totalXPForLevel = 100;

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

			// MOVE TO METHOD
			totalXPForLevel = 110;

		}

	}

	public void DoLevelUp(int _currentXP, int totalXP) {

		GainLevel ();

		int overflow = _currentXP - totalXP;

		SetCurrentXP (overflow);

	}

	public int GetTotalXPForLevel() {

		return totalXPForLevel;

	}

	void OnGUI() {
		
		GUI.Label(new Rect(10, 10, 200, 20), "XP: " + currentXP + "/" + totalXPForLevel + ". Level: " + currentLevel);

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
