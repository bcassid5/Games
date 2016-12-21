using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgainScreen : MonoBehaviour {
	
	public void OnPlayAgainClick(){
		SceneManager.LoadScene ("_Scene_0_RPS");
	}

	public void OnMenuClick(){
		SceneManager.LoadScene ("_Scene_MainMenu");
	}

}
