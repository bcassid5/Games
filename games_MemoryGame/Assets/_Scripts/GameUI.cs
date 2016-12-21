using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

	public Text time;
	public Text score;
	public static int scr = 1000;
	public static int startTime = 0;

	void Update() {
		time.text = "Time | " + (int)(Time.time-startTime);

		score.text = "Score | " + scr;
	}

}
