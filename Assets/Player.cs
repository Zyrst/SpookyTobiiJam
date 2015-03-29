﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float instanity { get { return insanityGainCurve.Evaluate(_insanity/maxInsanity); } } 
	public bool dead { get; private set; }
	public AnimationCurve insanityGainCurve;
	public Transform spawnPoint;

	public float fadeInTime = 2.0f;

	public static Player instance  { get; private set; }
	// Use this for initialization

	public int maxInsanity = 10;
	private int _insanity;

	private bool firstTick = true;

	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (firstTick) {
			firstTick = false;
			spawn ();
		}
	}

	public void die() {
		dead = true;
	}

	public void spawn(Vector3 point) {
		StartCoroutine (spawnTransition(point));
	}

	public void spawn() {
		spawn (spawnPoint.position);
	}

	private IEnumerator spawnTransition(Vector3 point) {
		transform.position = point;
		Debug.Log (SplashScreen.instance);
		yield return StartCoroutine (SplashScreen.instance.fadeIn (Color.black, fadeInTime));
	}

	public void setInsanity(int ins){
		//Set a value which are used with animation curve
		_insanity = ins;
	}

	public void incInsanity(){
		_insanity++;
		if(_insanity > 10)
			_insanity = 10;

	}
	public void decInsanity(){
		_insanity--;
		if (_insanity < 0)
			_insanity = 0;
	}
}
