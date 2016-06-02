using UnityEngine;
using System.Collections;

public class DamageIndicator : MonoBehaviour {

	public Vector3 damageIndicatorAngle = new Vector3 (-45f, 180f, 0f);

	// Use this for initialization
	void Start () {
	
		// TODO: There needs to be an animation here, maybe choose between move up, rotate around a point [counter] clockwise.
		transform.rotation = Quaternion.Euler (damageIndicatorAngle);

	}

}
