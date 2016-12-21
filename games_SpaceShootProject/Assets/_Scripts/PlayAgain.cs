using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayAgain : MonoBehaviour {

	public Button playAgain;
	public Button exit;

	private GameObject go;

	void Start() {
		playAgain = playAgain.GetComponent<Button> ();
		exit = exit.GetComponent<Button> ();
	}

	public void BeginGameScene0(){
		SceneManager.LoadScene ("_Scene_0");
		InGameButtons.currentTime = (int)Time.time;
	}

	public void LoadTitleScreen(){
		Menu.stopBackSound ();
		SceneManager.LoadScene ("_Scene_Title");
	}
}
