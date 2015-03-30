using UnityEngine;
using System.Collections;

[AddComponentMenu("Tobii+/GazeAware2")]
[RequireComponent(typeof(GazeAwareComponent))]
public class GazeAware2 : MonoBehaviour {

	public bool HasGaze { 
		get { 
			if(AlternativeLookTarget.instance.eyeTrackerAvailable) {
				return gaze.HasGaze;
			} else {
				return AlternativeLookTarget.instance.curTarget != null && AlternativeLookTarget.instance.curTarget.Equals(this);
			}
		}
	}

	private GazeAwareComponent gaze;

	void Start () {
		gaze = GetComponent<GazeAwareComponent> ();
	}
}
