using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public static Inventory instance { get; private set; }

	private Item inv;

	// Use this for initialization
	void Awake () {
		if (instance != null) {
			Debug.LogError ("Multiple Inventories! There can only be one!");
		} else {
			instance = this;
		}
	}
	
	public void addItem(Item item) {
		inv |= item;
	}

	public bool hasItem(Item item) {
		return (inv & item) == item;
	}

	public void removeItem(Item item) {
		item &= ~Item.KEY_DOLLHOUSE_DOOR;
	}
}
