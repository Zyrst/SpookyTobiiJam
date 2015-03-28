using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float instanity { get { return animeCurve.Evaluate(_insanity/maxInsanity); } } 

	public AnimationCurve animeCurve;

	public static Player instance  { get; private set; }
	// Use this for initialization

	public int maxInsanity = 10;
	private int _insanity;

	void Awake () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
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
