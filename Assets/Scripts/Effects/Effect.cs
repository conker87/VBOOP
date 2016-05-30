using UnityEngine;
using System.Collections;

public abstract class Effect : MonoBehaviour {

	protected string effectName = "Test Buff", effectDescription = "It's a test. I dunno.";
	public string EffectName			{ get {	return this.effectName; }			set {	this.effectName = value; } }
	public string EffectDescription		{ get {	return this.effectDescription; }	set {	this.effectDescription = value; } }

	protected EffectType effectType;
	public EffectType EffectType		{ get {	return this.effectType; }			set {	this.effectType = value; } }

	public abstract void DoEffect ();
}

public enum EffectType { BUFF, DEBUFF };