using UnityEngine;
using System.Collections;

public class _DEBUG_Append_InstanceID_At_Runtime : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.name = gameObject.name + "_" + gameObject.GetInstanceID();
	}

}
