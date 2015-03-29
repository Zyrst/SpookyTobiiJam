using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Simpleai : MonoBehaviour {

	public float speed = 5.0f;
	public float turnSpeed = 1.5f;
	public List<Transform> pointArray = new List<Transform> ();
	
	public Vector3 target = new Vector3(0,0,0);
	public bool debug = false;
	public AnimationCurve animeCurve;
	private CircularList cList = new CircularList();

	void Start() {
		StartCoroutine (Move ());
	}
	
	IEnumerator Move () {
		foreach (Transform t in pointArray)
			cList.Add (t);
		//transform.position = cList.current ().position;

		while(true) {
			Vector3 current = cList.current().position;
			Vector3 curDir = current - cList.previous().position;
			curDir.Normalize();

			Vector3 next = cList.next().position;
			Vector3 nextDir = next - current;
			nextDir.Normalize();

			Vector3 nextnext = cList.nextnext ().position;
			Vector3 nextnextDir = nextnext - next;
			nextnextDir.Normalize();

			Vector3 firstDir = Vector3.Lerp(curDir, nextDir, 0.5f);
			Vector3 secondDir = Vector3.Lerp(nextDir, nextnextDir, 0.5f);

			Vector3 between0;
			Vector3 between1;
			Math3D.ClosestPointsOnTwoLines(out between0, out between1, current, firstDir, next, secondDir);
			Vector3 between = Vector3.Lerp(between0, between1, 0.5f);

			float dist = (next - current).magnitude;

			float elapsed = 0.0f;
			while(elapsed < 1.0f) {

				float eval = animeCurve.Evaluate(elapsed);
				Vector3 first = Vector3.Lerp(current, between, elapsed);
				Vector3 second = Vector3.Lerp(between, next, elapsed);
				transform.position = Vector3.Lerp(first, second, elapsed);
				elapsed += Time.deltaTime * speed * 0.1f /  dist;
				if(debug) {
					Debug.DrawLine(current, between, Color.green);
					Debug.DrawLine(between, next, Color.red);
					Debug.DrawLine(first, second, Color.yellow);
					
					Debug.DrawRay(current, curDir, Color.magenta);
					Debug.DrawRay(next, nextDir, Color.magenta);
					
					Debug.DrawRay(cList.previous().position, (current - cList.previous ().position).normalized, Color.blue);
					Debug.DrawRay(cList.nextnext().position, (cList.nextnextnext().position - cList.nextnext().position).normalized, Color.blue);
					
					
					Debug.DrawLine(cList.previous().position, current, Color.cyan);
					Debug.DrawLine(current, next, Color.cyan);
					Debug.DrawLine(next, cList.nextnext().position, Color.cyan);
					Debug.DrawLine(cList.nextnext().position, cList.nextnextnext().position, Color.cyan);
				}

				yield return null;
			}
			cList.inc ();
		}
	}
	
	// Use this for initialization
	/*IEnumerator Start () {
		Vector3 startPos = transform.position;
		while(true)
		{
			for(int i = 0; i < pointArray.Count;i++)
			{
				int j = i+1;
				int k = j +1;
				if(j == pointArray.Count)
					j = 0;
				if(k == pointArray.Count)
					k = 0;
			
				yield return StartCoroutine(MoveObject(transform,pointArray[i],copy,3.0f));

			}
		}
	}*/

	/*IEnumerator MoveObject(Transform thisTransform, Transform startPos, Transform endPos, float time)
	{
		float i = 0.0f;
		float rate = 1.0f / time;
		while(i < 1.0f)
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos.position, endPos.position, i);
			yield return null;
		}


	}*/

	// Update is called once per frame
	void Update () {
		
	}
}
