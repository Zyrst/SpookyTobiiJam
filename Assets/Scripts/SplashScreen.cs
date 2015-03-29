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
		Color target = color;
		target.a = 0.0f;
		Color start = color;
		start.a = 1.0f;
		splash.color = start;

		float elapsed = 0.0f;
		while(elapsed < duration) {
			splash.color = Color.Lerp(start, target, elapsed/duration);
			elapsed += Time.deltaTime;
			yield return null;
		}
		splash.color = target;
		yield return null;
	}

	public IEnumerator fadeOut(Color color, float duration) {
		Color target = color;
		target.a = 1.0f;
		Color start = color;
		start.a = 0.0f;
		splash.color = start;
		
		float elapsed = 0.0f;
		while(elapsed < duration) {
			splash.color = Color.Lerp(start, target, elapsed/duration);
			elapsed += Time.deltaTime;
			yield return null;
		}
		splash.color = target;
		yield return null;
	}
}
