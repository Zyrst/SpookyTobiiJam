using UnityEngine;
using System.Collections;

public class RedDoorUnlock : DoorUnlock {



	protected override void open() {
		doorHinge.AddForce(-transform.forward * openingForce);
		StartCoroutine (transition ());
	}

	private IEnumerator transition() {
		yield return SplashScreen.instance.fadeOut (Color.black, 2.0f);

		Application.LoadLevel ("DollShowLevel");
	}
}
