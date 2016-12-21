using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStarts : MonoBehaviour {

	public void playApplePicker() {
		SceneManager.LoadScene ("_Scene_0_ApplePicker");
	}
	public void playSpaceShoot() {
		SceneManager.LoadScene ("_Scene_0");
		InGameButtons.currentTime = (int)Time.time;
	}
	public void playRPS() {
		SceneManager.LoadScene ("_Scene_0_RPS");
	}
	public void playMatcher() {
		SceneManager.LoadScene ("_Scene_0_Matcher");
	}
}
