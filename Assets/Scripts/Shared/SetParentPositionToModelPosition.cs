using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SetParentPositionToModelPosition : MonoBehaviour {

	public GameObject modelPrefab;
	public Vector3 offset;

	void Update () {
		
		if (modelPrefab != null) {

			transform.position = modelPrefab.transform.position + offset;

		}

	}
}
