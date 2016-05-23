using UnityEngine;
using System.Collections;

public class DestroyIn : MonoBehaviour {

	public float destroyInSeconds = 5f;
	public float destroyingIn;

	void Start () {
	
		Destroy (gameObject, destroyInSeconds);

		destroyingIn = destroyInSeconds;

	}

	void Update() {

		destroyingIn -= Time.deltaTime;

	}

}
