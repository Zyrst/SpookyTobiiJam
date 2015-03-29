using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {
	[Range(0.01f, 1.0f)]
	public float minFlickerSpeed = 0.1f;
	[Range(0.1f, 10.0f)]
	public float maxFlickerSpeed = 1.0f;
	public GameObject target;
	// Use this for initialization
	void Start () {
		if(target != null)
			StartCoroutine (Flickerater ());
	}

	IEnumerator Flickerater() {
		while (isActiveAndEnabled) {
			target.SetActive (true);
			yield return new WaitForSeconds (Random.Range(minFlickerSpeed, maxFlickerSpeed ));
			target.SetActive (false);
			yield return new WaitForSeconds (Random.Range(minFlickerSpeed, maxFlickerSpeed ));
		}
			
	}
}
