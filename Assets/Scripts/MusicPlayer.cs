using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicPlayer : AudioPlayer {
	public static MusicPlayer instance { get; private set; }


	public List<AudioClip> soundTracks;
	
	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else {
			Debug.LogError("Multiple MusicPlayers!");
		}
	}

	public void SwitchTrack(AudioClip clip) {
		if (src.isPlaying) {
			src.Stop();
		}
		src.clip = clip;
		src.Play();
	}

	public void PlaySound(string clipName) {
		foreach (AudioClip c in soundTracks) {
			if(c.name == clipName) {
				SwitchTrack(c);
				return;
			}
		}
		Debug.LogWarning ("Could not find clip: '" + clipName + "'. Bro, do you even spell?");
	}
}
