using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour {
	public List<AudioClip> clips;
	protected AudioSource src;
	void Awake() {
		src = GetComponent<AudioSource> ();
	}

	public void PlaySound(string clipName) {
		foreach (AudioClip c in clips) {
			if(c.name == clipName) {
				src.PlayOneShot(c);
				return;
			}
		}
		Debug.LogWarning ("Could not find clip: '" + clipName + "'. Bro, do you even spell?");
	}
}
