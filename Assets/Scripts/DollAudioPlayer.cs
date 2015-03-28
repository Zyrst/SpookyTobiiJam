using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DollAudioPlayer : AudioPlayer {

	public List<AudioClip> footstepSounds;

	void Update () {
	
	}

	public void PlayRandomFootstep() {
		if(footstepSounds.Count > 0) {
			src.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Count)]);
		}
	}
}
