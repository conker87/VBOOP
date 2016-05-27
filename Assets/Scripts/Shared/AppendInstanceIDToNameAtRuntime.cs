using UnityEngine;
using System.Collections;

public class AppendInstanceIDToNameAtRuntime : MonoBehaviour {

	// Use this for initialization
	void Start () {

		gameObject.name = gameObject.name + "_" + gameObject.GetInstanceID ();

	}

}
