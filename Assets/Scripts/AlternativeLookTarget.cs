using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EyeXEngineAvailabilityComponent))]
[RequireComponent(typeof(Camera))]
public class AlternativeLookTarget : MonoBehaviour {
	public static AlternativeLookTarget instance { get; private set; }
	private EyeXEngineAvailabilityComponent ev;
	private Camera cam;
	public GazeAware2 curTarget { get; private set; }
	public Vector2 screenCenter {
		get { return new Vector2(Screen.width/2, Screen.height/2); }
	}
	public bool eyeTrackerAvailable { get { return ev.IsEyeXAvailable; } }

	void Start () {
		instance = this;
		ev = GetComponent<EyeXEngineAvailabilityComponent> ();
		cam = GetComponent<Camera> ();

	}

	void Update () {
		if (!eyeTrackerAvailable) {
			RaycastHit hit;
			if(Physics.Raycast(cam.ViewportPointToRay( new Vector3(0.5f, 0.0f, 0.5f)), out hit)) {
				GazeAware2 t;
				if( (t = hit.transform.GetComponent<GazeAware2>()) != null) {
					curTarget = t;
				}
			}
			

		}
	}
}
