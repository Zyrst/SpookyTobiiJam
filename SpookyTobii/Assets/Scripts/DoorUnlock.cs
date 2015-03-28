using UnityEngine;
using System.Collections;

public class DoorUnlock : MonoBehaviour {

	private GazeAwareComponent c;

	private float progress = 0.0f;
	public float timeNeeded;
	public Item requiredItem = Item.ANYTHING_NOTHING;
	public bool destroyItemOnUnlock = true;

	public Renderer doorLock;
	public Color highlightColor;

	public Rigidbody doorHinge;
	public float openingForce = 1.0f;

	void Awake () {
		c = GetComponent<GazeAwareComponent> ();
		doorLock.material.EnableKeyword ("_Color");
	}

	void FixedUpdate () {
		doorLock.material.SetColor("_Color", Color.white);
		if (progress > timeNeeded) {
			doorHinge.AddForce(-transform.forward * openingForce);
			Inventory.instance.removeItem(requiredItem);
			this.enabled = false;
		}
		

		if (c.HasGaze) {
			if (Inventory.instance.hasItem(requiredItem)) {
				progress += Time.fixedDeltaTime;

			} else {
				if (doorLock != null) {
					doorLock.material.SetColor("_Color", Color.Lerp(Color.white, highlightColor, (Mathf.Cos (Time.time * Mathf.PI) + 1)/2));
				} else {
					Debug.LogWarning("Door requires item but lacks indicator");
				}
			}
		}
	}
}
