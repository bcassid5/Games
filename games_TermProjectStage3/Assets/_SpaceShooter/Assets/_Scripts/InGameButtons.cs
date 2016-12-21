using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameButtons : MonoBehaviour {

	private bool pause;
	public Text timeText;
	public static int currentTime;
	public Text level;


	void Awake() {
		timeText.text = "Time: 0";
		level.text = "Current Level";
	}

	void Start() {
		pause = false;
	}

	void FixedUpdate() {
		timeText.text = "Time: " + ((int)Time.time - currentTime);
		level.text = "Current Level: " + Main.currentLevel;
	}

	public void OnPauseClick() {
		pause = !pause;
		if (pause) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	public void OnRestartClick() {
		SceneManager.LoadScene ("_Scene_0");
		currentTime = (int)Time.time;
		Time.timeScale = 1;
	}

	public void OnStopClick() {
		History_SS.scoreAchieved = Main.scr;
		History_SS.setNewAP ();
		SceneManager.LoadScene ("_Scene_PlayAgain");
		currentTime = 0;
	}
}
