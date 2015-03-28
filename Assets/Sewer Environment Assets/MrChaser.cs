using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MrChaser : MonoBehaviour {

	public float speed = 5.0f;
	public float turnSpeed = 1.5f;
	public List<Transform> pointArray = new List<Transform> ();
	private TraversalList cList = new TraversalList();
	public Vector3 target = new Vector3(0,0,0);
	public AnimationCurve animeCurve;
	//private CircularList cList = new CircularList();
	
	void Awake() {
		StartCoroutine (Move ());
	}
	
	IEnumerator Move () {
		foreach (Transform t in pointArray)
			cList.Add (t);
		cList.inc ();
		//transform.position = cList.current ().position;
		Vector3 current = cList.current().position;
		Vector3 curDir = current - cList.previous().position;
		curDir.Normalize();
		
		while(cList.cont()) {
			
			
			Vector3 next = cList.next().position;
			Vector3 nextDir = next - cList.nextnext().position;
			nextDir.Normalize();
			nextDir = Vector3.Lerp(curDir, nextDir, 0.5f);
			Vector3 between0;
			Vector3 between1;
			Math3D.ClosestPointsOnTwoLines(out between0, out between1, current, curDir, next, nextDir);
			Vector3 between = Vector3.Lerp(between0, between1, 0.5f);
			
			float elapsed = 0.0f;
			while(elapsed < 1.0f) {
				
				float eval = animeCurve.Evaluate(elapsed);
				Vector3 first = Vector3.Lerp(current, between, elapsed);
				Vector3 second = Vector3.Lerp(between, next, elapsed);
				transform.position = Vector3.Lerp(first, second, elapsed);
				elapsed += Time.deltaTime;
				
				Debug.DrawLine(current, between, Color.green);
				Debug.DrawLine(between, next, Color.red);
				Debug.DrawLine(first, second, Color.yellow);
				Debug.DrawRay(current, next - current, Color.cyan);
				Debug.DrawRay(current, curDir, Color.magenta);
				Debug.DrawRay(next, nextDir, Color.blue);
				Debug.DrawLine(second, cList.nextnext().position, Color.cyan);
				yield return null;
			}
			cList.inc ();
			
			current = next;
			curDir = nextDir;
			yield return null;
		}


	}
	// Update is called once per frame
	void Update () {
		
	}
}
