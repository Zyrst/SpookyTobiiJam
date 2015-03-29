using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class SplashScreen : MonoBehaviour {
	public static SplashScreen instance { get; private set; }
	private Image splash;
	void Start () {
		if (instance == null) {
			instance = this;
		} else {
			Debug.LogWarning("Multiple SplashScreen instances!");
		}

		splash = GetComponent<Image> ();
	}

	public IEnumerator fadeIn(Color color, float duration) {

	}
}
