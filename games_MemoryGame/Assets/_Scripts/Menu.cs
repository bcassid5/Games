using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void play() {
		SceneManager.LoadScene ("_Scene_0");
		GameUI.startTime = (int)Time.time;

	}
	public void exit() {
		Application.Quit ();
	}
}
