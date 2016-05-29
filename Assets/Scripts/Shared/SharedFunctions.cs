using UnityEngine;
using System;
using System.Collections;

public class SharedFunctions : MonoBehaviour {

	public T RandomEnumValue<T> ()
	{
		var v = Enum.GetValues (typeof (T));
		return (T) v.GetValue (UnityEngine.Random.Range(0, v.Length));
	}

}
