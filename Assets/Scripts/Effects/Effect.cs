using UnityEngine;
using System;
using System.Collections;

public class Effect {

	public Effect() {

	}

	public Effect(string _effectName, string _effectDescription, float _effectDuration, float _startTime, float _tickDuration,
		Weapon _sourceWeapon, Entity _sourceEntity, Action _doEffect, Action _endEffect) {

		EffectName = _effectName;
		EffectDescription = _effectDescription;
		EffectDuration = _effectDuration;
		OriginalTime = _startTime;
		TimeUntilNextTick = _tickDuration;
		SourceWeapon = _sourceWeapon;
		SourceEntity = _sourceEntity;
		DoEffect = _doEffect;
		EndEffect = _endEffect;

	}

	public Entity effectEntity;

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

	protected Weapon sourceWeapon;
	protected Entity sourceEntity;
	public Weapon SourceWeapon		{ get {	return this.sourceWeapon; }	set {	this.sourceWeapon = value; } }
	public Entity SourceEntity		{ get {	return this.sourceEntity; }	set {	this.sourceEntity = value; } }

	protected Action doEffect, endEffect;
	public Action DoEffect		{ get {	return this.doEffect; }		set {	this.doEffect = value; } }
	public Action EndEffect		{ get {	return this.endEffect; }	set {	this.endEffect = value; } }

	public bool disabled = false;
	public bool firstRun = true;

	[SerializeField]
	float destroyingIn;

	//void Start() {

	//	destroyingIn = EffectDuration;

	//}

	//void Update() {

	//	destroyingIn -= Time.deltaTime;

	//}

//	public virtual void DoEffect() {

		// To be overriden in inheritted prefabs.

//	}

//	public virtual void EndEffect () {

		// To be overriden in inheritted prefabs.

		//Destroy (gameObject);

//	}
}

public enum EffectType { BUFF, DEBUFF };