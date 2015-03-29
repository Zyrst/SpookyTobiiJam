using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
	public GameObject planks;
	public MrChaser chaser;
	void OnTriggerEnter(Collider other) {
		if (other.GetComponentInChildren<Player> () != null) {
			planks.SetActive(false);
			chaser.enabled = true;
		}
	}
}
