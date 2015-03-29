using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float instanity { get { return insanityGainCurve.Evaluate(_insanity/maxInsanity); } } 
	public bool dead { get; private set; }
	public AnimationCurve insanityGainCurve;
	public Transform spawnPoint;
	private Camera _camera;

	public float fadeInTime = 2.0f;

	public static Player instance  { get; private set; }
	// Use this for initialization

	public int maxInsanity = 10;
	private int _insanity;

	private bool firstTick = true;

	void Awake () {
		instance = this;
		_camera = GetComponentInChildren<Camera> ();
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

	void OnTriggerEnter(Collider other){
		GetComponent<Movement> ().enabled = false;
		die ();
		StartCoroutine(bobbingDeath());

	}

	IEnumerator bobbingDeath() {
		CharacterController controller = GetComponent<CharacterController> ();
		Vector3 vel = controller.velocity;
		float elapsed = 0.0f;
		while(elapsed < 1.0f){

			elapsed += Time.deltaTime;
			Vector3 v = controller.velocity;
			float vY = v.y;
			v *= 0.75f * Time.deltaTime;

			if(controller.transform.position.y > -0.3){
				vY -= Time.deltaTime;
			}/* else if(controller.transform.position.y <= -0.1){
				vY += Time.deltaTime;
			}*/
			v.y = vY;
			controller.Move (v);
			yield return null;
			/*if(controller.transform.position.y > -0.1){
				Debug.Log ("Hello");
					controller.Move (new Vector3(0,(-Mathf.Sin (1*animeCurve.Evaluate(v.magnitude))),0));
				yield return null;
			}
			if(controller.transform.position.y <= -0.1 || controller.transform.position.y > -0.2 ){
				//controller.Move(controll0er.transform.position * 0.5f);
				Debug.Log("hello");
					controller.Move (new Vector3(0,(Mathf.Sin (1*animeCurve.Evaluate(v.magnitude))),0));
				yield return null;
			}*/
		}
	}
}
