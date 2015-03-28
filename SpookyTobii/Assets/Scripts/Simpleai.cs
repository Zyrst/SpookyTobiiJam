using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Simpleai : MonoBehaviour {

	public float speed = 5.0f;
	public float turnSpeed = 1.5f;
	public List<Transform> pointArray = new List<Transform> ();
	
	public AnimationCurve animeCurve;
	private CircularList cList = new CircularList();

	void Awake() {
		StartCoroutine (Move ());
	}
	
	IEnumerator Move () {
		foreach (Transform t in pointArray)
			cList.Add (t);
		//transform.position = cList.current ().position;
		while(true) {
			Vector3 previous = cList.previous().position;
			Vector3 current = cList.current().position;
			Vector3 next = cList.next().position;
			Vector3 nextnext = cList.nextnext().position;
			Vector3 nextnextnext = cList.nextnextnext().position;
			float elapsed = 0.0f;
			while(elapsed < 1.0f) {
				float eval = animeCurve.Evaluate(Mathf.Clamp (elapsed,0,1));
				Vector3 first = Vector3.Lerp(current, next, eval);
				Vector3 second = Vector3.Lerp (next,nextnext, eval);
				Vector3 third = Vector3.Lerp(nextnext,nextnextnext, eval);
				Vector3 result1 = Vector3.Lerp(first,second,eval);
				Vector3 result2 = Vector3.Lerp(second, third, eval);
				Vector3 result = Vector3.Lerp (result1, result2, eval);
				transform.position = first;
				//transform.position = Vector3.Lerp(current, next, eval);
				elapsed += Time.deltaTime;
				yield return null;
			}
			cList.inc ();
			yield return null;
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
