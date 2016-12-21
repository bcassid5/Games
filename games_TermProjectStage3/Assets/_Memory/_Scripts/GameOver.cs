using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public static int winScore;
	public static int winTime;
	public static string winLoseText = "";

	public Text winLose;
	public Text scr;
	public Text time;

	void Awake() {
		History_MU.username = Login.current.retrieveUsername ();
		History_MU.scoreAchieved = winScore;
		History_MU.setNewAP ();
	}

	void Start() {
		winLose.text = winLoseText;
		scr.text = "Final Score | " + winScore;
		time.text = "Total Time | " + winTime;
	}

	public void playAgain() {
		GameUI.startTime = (int)Time.time;
		Game.overCount = 0;
		GameUI.scr = 1000;
		SceneManager.LoadScene ("_Scene_0_Matcher");
	}
	public void mainMenu() {
		Game.overCount = 0;
		GameUI.scr = 1000;
		SceneManager.LoadScene ("_Scene_MainMenu");
	}
}
