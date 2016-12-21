using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour {

	public Button play;
	public Button exit;

	// Use this for initialization
	void Start () {
		play = play.GetComponent<Button> ();
		exit = exit.GetComponent<Button> ();
	}
	
	public void OnPlayPress(){
		SceneManager.LoadScene ("_Scene_Main");
	}

	public void OnExitPress(){
		Application.Quit ();
	}

}
