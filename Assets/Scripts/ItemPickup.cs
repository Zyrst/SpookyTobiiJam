using UnityEngine;
using System.Collections;
[RequireComponent(typeof(GazeAware2))]
public class ItemPickup : MonoBehaviour {

	private GazeAware2 c;
	public Item type;

	private float progress = 0.0f;
	public float timeNeeded = 0.2f;

	void Awake () {
		c = GetComponent<GazeAware2> ();
		if (type == Item.EVERYTHING || type == Item.ANYTHING_NOTHING) {
			Debug.LogWarning("You have an item of type EVERYTHING or ANYTHING. Are you insane?");
		}
	}

	void FixedUpdate () {
		if (progress > timeNeeded) {
			Inventory.addItem(type);
			Destroy(gameObject);
		}

		if (c.HasGaze) {
			progress += Time.fixedDeltaTime;
		} else {
			progress = 0;
		}
	}
}
