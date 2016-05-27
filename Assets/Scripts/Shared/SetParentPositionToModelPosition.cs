using UnityEngine;
using System.Collections;

public class SetParentPositionToModelPosition : MonoBehaviour {

	public GameObject modelPrefab;

	void Update () {
		
		if (modelPrefab != null) {

			transform.position = modelPrefab.transform.position;

		}

	}
}
