using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgainScreen : MonoBehaviour {

	public Button playAgain;
	public Button mainMenu;

	void Start() {
		playAgain = playAgain.GetComponent<Button> ();
		mainMenu = mainMenu.GetComponent<Button> ();
	}

	public void OnPlayAgainClick(){
		SceneManager.LoadScene ("_Scene_Main");
	}

	public void OnMenuClick(){
		SceneManager.LoadScene ("_Scene_Title");
	}

}
