using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory {

	private static Item inv;
	
	public static void addItem(Item item) {
		inv |= item;
	}

	public static bool hasItem(Item item) {
		return (inv & item) == item;
	}

	public static void removeItem(Item item) {
		inv &= ~item;
	}
}
