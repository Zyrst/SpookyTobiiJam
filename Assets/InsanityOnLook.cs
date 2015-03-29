using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GazeAwareComponent))]
public class InsanityOnLook : MonoBehaviour {
	GazeAwareComponent fdc;

	public float coolDown;
	private float last;

	public float fixationTimeNeeded = 0.5f;
	private float curFixated;

	// Use this for initialization
	void Start () {
		fdc = GetComponent<GazeAwareComponent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (curFixated > fixationTimeNeeded && coolDown < last) {
			Player.instance.incInsanity();
		}
		last += Time.deltaTime;
		if (fdc.HasGaze) {
			curFixated += Time.deltaTime;
		} else {
			curFixated = 0.0f;
		}

	}
}
