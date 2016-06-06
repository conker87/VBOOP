using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	[SerializeField]
	protected string	itemName = "";
	public string 		ItemName		{ get {	return this.itemName; }		set {	this.itemName = value; } }

	protected int 		stackSize = 1;
	public int	 		StackSize		{ get {	return this.stackSize; }	protected set {	this.stackSize = value; } }

}
