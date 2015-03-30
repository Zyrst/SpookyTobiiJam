using UnityEngine;
using System.Collections;
[AddComponentMenu("Tobii+/GazePointDataComponent2")]
[RequireComponent(typeof(GazePointDataComponent))]
public class GazePointDataComponent2 : MonoBehaviour {
	private GazePointDataComponent gp;

	/*public Vector2 LastGazePoint { 
		get { 
			if(AlternativeLookTarget.instance.eyeTrackerAvailable) {
				EyeXGazePoint ep = gp.LastGazePoint;
				if(ep.IsValid) {
					return gp.LastGazePoint.Screen;
				} else {
					return null;
				}

			} else {
				return AlternativeLookTarget.instance.screenCenter;
			}
		} 
	}*/
	public class EyeXGazePoint2
	{
		public bool IsValid { get; private set; }
		public bool IsWithinScreenBounds { get; private set; }
		public Vector2 screen { get; private set; }

		public EyeXGazePoint2(bool valid, bool inScreenBounds, Vector2 screen) {
			IsValid = valid;
			IsWithinScreenBounds = inScreenBounds;
			this.screen = screen;
		}
	}

	public EyeXGazePoint2 LastGazePoint { 
		get {
			if (AlternativeLookTarget.instance.eyeTrackerAvailable) {
				EyeXGazePoint p = gp.LastGazePoint;
				return new EyeXGazePoint2(p.IsValid, p.IsWithinScreenBounds, p.Screen);
			} else {
				return new EyeXGazePoint2(true, true, AlternativeLookTarget.instance.screenCenter);
			}
		}
	}
	// Use this for initialization
	void Start () {
		gp = GetComponent<GazePointDataComponent> ();


	}
}
