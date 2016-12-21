using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Scoring : MonoBehaviour {

	public static int scr = 0;
	public Text scoreText;

	void Update() {
		scoreText.text = "Current Score: " + scr;
	}

}
