using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(FixationDataComponent))]
public class DoFTargeter : MonoBehaviour {
	private FixationDataComponent fdc;
	public Camera cam;
	public GameObject target;

	// Use this for initialization
	void Awake () {
		fdc = GetComponent<FixationDataComponent> ();
		Debug.Log (fdc);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (fdc.isActiveAndEnabled && fdc.LastFixation != null && fdc.LastFixation.IsValid) {
			RaycastHit hit; //gp.Screen
			if (Physics.Raycast (cam.ScreenPointToRay (fdc.LastFixation.GazePoint.Screen), out hit)) {
				target.transform.position = hit.point;
			}
		}
	}
}
