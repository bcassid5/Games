using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour {

	static public int score = 1000;

	void Awake(){
		if (PlayerPrefs.HasKey ("SpaceShootHighScore")) {
			score = PlayerPrefs.GetInt ("SpaceShootHighScore");
		}
		PlayerPrefs.SetInt ("SpaceShootHighScore", score);
	}

	void Start(){
		
	}

	void Update(){
		GUIText gt = this.GetComponent<GUIText> ();
		gt.text = "High Score: " + score;
		if (score > PlayerPrefs.GetInt ("SpaceShootHighScore")) {
			PlayerPrefs.SetInt ("SpaceShootHighScore", score);
		}
	}
}
