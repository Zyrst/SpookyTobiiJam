using UnityEngine;
using System.Collections;

public class Tmp : MonoBehaviour {
	private GazeAwareComponent c;
	// Use this for initialization
	void Start () {
		c = GetComponent<GazeAwareComponent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (c.HasGaze) {
			transform.Rotate(Vector3.forward);
		}
	}
}
