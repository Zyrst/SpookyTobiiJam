using UnityEngine;
using System.Collections;
[RequireComponent(typeof(GazePointDataComponent2))]
[RequireComponent(typeof(Camera))]
public class FOVInsanity : MonoBehaviour {
	[Range(0.01f, 10.0f)]
	public float multiplier = 0.01f;
	[Range(0.1f, 100.0f)]
	public float distanceCap = 10.0f;
	public AnimationCurve falloffCurve;
	public float smoothTime = 0.5f;

	public float baseFov = 60.0f;
	[Range(10.0f, 60.0f)]
	public float minFov = 30.0f;
	[Range(60.0f, 170.0f)]
	public float maxFov = 170.0f;


	private GazePointDataComponent2 c;
	private Camera cam;
	private float avgDist = 1.0f;
	private float curVel;
	// Use this for initialization
	void Awake () {
		c = GetComponent<GazePointDataComponent2> ();
		cam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GazePointDataComponent2.EyeXGazePoint2 gp = c.LastGazePoint;
		if (gp.IsValid && gp.IsWithinScreenBounds) {
			RaycastHit hit; //gp.Screen
			if (Physics.Raycast (cam.ScreenPointToRay (gp.screen), out hit)) {
				Debug.DrawRay(transform.position, hit.point, Color.red);
				avgDist = Mathf.Lerp(avgDist, Mathf.MoveTowards(avgDist, hit.distance, 5.0f), 0.05f);
				
				cam.fieldOfView = Mathf.SmoothDamp (
					cam.fieldOfView,
					Mathf.Lerp (minFov, maxFov, falloffCurve.Evaluate (Mathf.Clamp (avgDist * multiplier, 0.1f, distanceCap) / distanceCap)),
					ref curVel, smoothTime);

			} else {
				cam.fieldOfView = Mathf.SmoothDamp (
					cam.fieldOfView,
					baseFov,
					ref curVel, 0.5f);
			}
		}
	}
}
