using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class OnTriggerFailState : MonoBehaviour {
	Collider c;
	void Awake() {
		c = GetComponent<Collider> ();
		if (!c.isTrigger) {
			Debug.LogError("Collider needs to be trigger!");
		}
	}
	
	void OnTriggerEnter(Collider other) {
		Player p;
		if ((p = other.transform.GetComponentInChildren<Player> ()) != null) {
			p.die();
		}
	}
}
