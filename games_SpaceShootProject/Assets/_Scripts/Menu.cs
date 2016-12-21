using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {

	public Canvas mainMenu;
	public Canvas titleMenu;
	public Canvas levelsBronze;
	public Canvas levelsSilver;
	public Canvas levelsGold;
	public Canvas levelSelect;
	public Canvas configs;
	public Canvas enemies;
	public Canvas background;
	public Canvas music;

	private static AudioSource backSound;
	public AudioSource ambient;
	public AudioSource ohChild;
	public AudioSource spaceJam;

	public GameObject AudioSources;

	void Awake() {
		if (PlayerPrefs.HasKey ("SpaceShootBackMusic")) {
			if (PlayerPrefs.GetString ("SpaceShootBackMusic") == "Ambient") {
				backSound = ambient;
			} else if (PlayerPrefs.GetString ("SpaceShootBackMusic") == "SpaceJam") {
				backSound = spaceJam;
			} else {
				backSound = ohChild;
			}
		} else {
			backSound = spaceJam;
		}

		if (PlayerPrefs.HasKey ("SpaceShootBackVolume")) {
			backSound.volume = PlayerPrefs.GetFloat ("SpaceShootBackVolume");
		} else {
			backSound.volume = 0.5f;
		}

		backSound.Play ();
	}

	void Update() {
		if (PlayerPrefs.HasKey ("SpaceShootBackMusic")) {
			if (PlayerPrefs.GetString ("SpaceShootBackMusic") == "Ambient") {
				backSound = ambient;
			} else if (PlayerPrefs.GetString ("SpaceShootBackMusic") == "SpaceJam") {
				backSound = spaceJam;
			} else {
				backSound = ohChild;
			}
		}
	}

	// Use this for initialization
	void Start () {		
		mainMenu.enabled = false;
		levelsBronze.enabled = false;
		levelsSilver.enabled = false;
		levelsGold.enabled = false;
		levelSelect.enabled = false;
		configs.enabled = false;
		enemies.enabled = false;
		background.enabled = false;
		music.enabled = false;

		DontDestroyOnLoad (AudioSources);
	}

	public static void stopBackSound () {
		backSound.Stop ();
	}
	public static void playBackSound () {
		backSound.Play ();
	}
	public static void pauseBackSound () {
		backSound.Pause ();
	}

	public void ExitGame () {
		Application.Quit ();
	}

	public void BeginGameScene0(){
		SceneManager.LoadScene ("_Scene_0");
		InGameButtons.currentTime = (int) Time.time;
	}

	public void LoadTitleScreen(){
		backSound.Stop ();
		SceneManager.LoadScene ("_Scene_Title");
	}

	public void OpenMainGameScreen() {
		titleMenu.enabled = false;
		mainMenu.enabled = true;
	}

	public void OpenLevelsScreen() {
		mainMenu.enabled = false;
		levelSelect.enabled = true;
	}

	public void OpenConfigs(){
		mainMenu.enabled = false;
		configs.enabled = true;
	}

	public void Bronze(){
		levelSelect.enabled = false;
		levelsBronze.enabled = true;
	}

	public void Silver(){
		levelSelect.enabled = false;
		levelsSilver.enabled = true;
	}

	public void Gold(){
		levelSelect.enabled = false;
		levelsGold.enabled = true;
	}

	public void BronzeSilverToLevel (){
		levelsBronze.enabled = false;
		levelsSilver.enabled = false;
		levelsGold.enabled = false;
		levelSelect.enabled = true;
	}

	public void LevelsToMain(){
		levelSelect.enabled = false;
		mainMenu.enabled = true;
	}

	public void ConfigsToMain() {
		configs.enabled = false;
		mainMenu.enabled = true;
	}

	public void ConfigsToEnemies() {
		configs.enabled = false;
		enemies.enabled = true;
	}

	public void ConfigsToMusic() {
		configs.enabled = false;
		music.enabled = true;
	}

	public void ConfigsToBackground() {
		configs.enabled = false;
		background.enabled = true;
	}

	public void BackToConfigs() {
		enemies.enabled = false;
		music.enabled = false;
		background.enabled = false;
		configs.enabled = true;
	}

}
