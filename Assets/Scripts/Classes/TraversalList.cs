using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TraversalList : MonoBehaviour {

	private List<Transform> list = new List<Transform> ();
	
	private int iterator = 0;
	
	public void Add (Transform t) {
		list.Add (t);
	}

	public bool cont() {
		return iterator + 2 < list.Count;
	}

	public Transform next() {
		return list[iterator + 1];
	}
	
	public Transform nextnext() {
		return list[iterator + 2];
	}
	
	public Transform nextnextnext() {
		return list[iterator + 3];
	}
	
	public Transform current() {
		return list[iterator];
	}
	
	public Transform previous() {
		return list[iterator - 1];
	}
	
	public void inc(){
		iterator++;
	}
}
