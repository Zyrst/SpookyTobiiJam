using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircularList {
	private List<Transform> list = new List<Transform> ();

	private int iterator = 0;

	public void Add (Transform t) {
		list.Add (t);
	}

	public Transform next() {
		if(iterator + 1 >= list.Count)
			return list [0];
		return list[iterator + 1];
	}

	public Transform nextnext() {
		if(iterator + 1 >= list.Count)
			return list [1];
		if(iterator + 2 >= list.Count)
			return list [0];
		return list[iterator + 2];
	}

	public Transform nextnextnext() {
		if(iterator + 1 >= list.Count)
			return list [2];
		if(iterator + 2 >= list.Count)
			return list [1];
		if(iterator + 3 >= list.Count)
			return list [0];
		return list[iterator + 3];
	}

	public Transform current() {
		return list[iterator];
	}

	public Transform previous() {
		if(iterator - 1 < 0)
			return list [list.Count-1];
		else
			return list[iterator - 1];
	}

	public void inc(){
		iterator++;
		if (iterator >= list.Count)
			iterator = 0;
	}

}
