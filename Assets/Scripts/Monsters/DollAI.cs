using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GazeAwareComponent))]
public class DollAI : MonoBehaviour {
	private static bool anyIsSeen { 
		get {
			foreach(DollAI doll in dolls) {
				if(doll.isSeen) return true;
			}
			return false;
		}}
	private static List<DollAI> dolls = new List<DollAI> ();

	public Transform target;
	public Transform dollHead;
	private CharacterController controller;

	public float followSpeed = 0.3f;

	private Renderer rend;
	private Animator anim;

	private bool isSeen {
		get { return gaze.HasGaze; }
	}

	private bool isSeenToggle = false;
	private GazeAwareComponent gaze;
	// Use this for initialization
	void Awake () {
		dolls.Add (this);
		controller = GetComponent<CharacterController> ();
		if (target == null) {
			Debug.LogError("Target is null");
		}
		rend = GetComponentInChildren<Renderer> ();
		anim = GetComponentInChildren<Animator> ();
		gaze = GetComponentInChildren<GazeAwareComponent> ();
	}

	private void freeze() {
		anim.speed = 0;
		if (isSeenToggle) {
			isSeenToggle = false;
			dollHead.LookAt(target.position);
		}
	}

	// Update is called once per frame
	void Update () {
		if (anyIsSeen) {
			freeze();
		} else {
			isSeenToggle = true;
			anim.speed = 1;
		}
		if (target != null) {
			if(!anyIsSeen) {
				transform.LookAt(target.position);
				transform.eulerAngles = Vector3.up * transform.eulerAngles.y;
				Vector3 moveDir = (target.position - transform.position);
				moveDir.y = 0;
				moveDir.Normalize();
				controller.SimpleMove(moveDir * followSpeed);
			}
		}
	}
}
