using UnityEngine;
using System;
using System.Collections;

public class Effect : MonoBehaviour {

	public Entity entity;

	[SerializeField]
	protected string effectName = "Test Buff", effectDescription = "It's a test. I dunno.";
	public string EffectName			{ get {	return this.effectName; }			set {	this.effectName = value; } }
	public string EffectDescription		{ get {	return this.effectDescription; }	set {	this.effectDescription = value; } }

	[SerializeField]
	protected EffectType effectType;
	public EffectType EffectType		{ get {	return this.effectType; }			set {	this.effectType = value; } }

	[SerializeField]
	protected float effectDuration = 8f;
	public float EffectDuration			{ get {	return this.effectDuration; }		set {	this.effectDuration = value; } }

	protected float originalTime;
	public float OriginalTime			{ get {	return this.originalTime; }			set {	this.originalTime = value; } }

	[SerializeField]
	protected float timeUntilNextTick = 1f;
	public float TimeUntilNextTick		{ get {	return this.timeUntilNextTick; }	protected set {	this.timeUntilNextTick = value; } }

	public bool disabled = false;
	public bool firstRun = true;

	public virtual void DoEffect() {

		// To be overriden in inheritted prefabs.

	}

	public virtual void EndEffect () {

		// To be overriden in inheritted prefabs.

	}
}

public enum EffectType { BUFF, DEBUFF };