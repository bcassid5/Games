using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour {

	static public Main S;
	static public Dictionary<WeaponType,WeaponDefinition> W_DEFS;

	public GameObject[] prefabEnemies;

	public float enemySpawnPerSec = 0.5f;
	private float enemySpawnSave;
	public float enemySpawnPadding = 1.5f;
	public WeaponDefinition[] weaponDefinitions;

	public GameObject prefabPowerUp;
	public WeaponType[] powerUpFrequency = new WeaponType[]{
		WeaponType.blaster, WeaponType.blaster, WeaponType.spread, WeaponType.shield, WeaponType.laser, WeaponType.bomb
	};

	public GUIText scoreGT;

	//scoring variables for each ship
	public Text e0;
	public Text e1;
	public Text e2;
	public Text e3;
	public Text e4;

	public static int eScore0 = 100;
	public static int eScore1 = 100;
	public static int eScore2 = 150;
	public static int eScore3 = 200;
	public static int eScore4 = 250;

	public static float screenWidth = 0;
	public static float screenHeight = 0;

	public static int wid = 0;
	public static int hei = 0;

	public AudioSource backSound;
	public AudioClip ambient;
	public AudioClip ohChild;
	public AudioClip spaceJam;
	public AudioSource destroySound;
	public AudioClip explode;
	public AudioClip glass;
	public AudioSource winSound;
	public AudioClip horn;
	public AudioClip league;


	public static int enemiesOnScreen;


	public bool ____________;

	public WeaponType[] activeWeaponTypes;
	public float enemySpawnRate;



	//different level variables
	public static string currentLevel = "Bronze";
	public static int maxEnemies = 10;
	int count = 0;
	//active enemies are a static from the 


	void Awake(){
		History_SS.date_time = System.DateTime.Now.ToString ();
		History_SS.username = Login.current.retrieveUsername ();
		S = this;
		enemySpawnSave = enemySpawnPerSec;

		if (screenWidth == 0 && screenHeight == 0) {
			Utils.SetCameraBounds (this.GetComponent<Camera> ());
		} else {
			Utils.SetCameraBounds (screenWidth, screenHeight);
			Screen.SetResolution (wid, hei, true);
		}
		enemySpawnRate = 1f / enemySpawnPerSec;
		Invoke ("SpawnEnemy", enemySpawnRate);

		W_DEFS = new Dictionary<WeaponType,WeaponDefinition> ();

		foreach (WeaponDefinition def in weaponDefinitions) {
			W_DEFS [def.type] = def;
		}

		if (PlayerPrefs.HasKey ("SpaceShootBackMusic")) {
			if (PlayerPrefs.GetString ("SpaceShootBackMusic") == "Ambient") {
				backSound.clip = ambient;
			} else if (PlayerPrefs.GetString ("SpaceShootBackMusic") == "SpaceJam") {
				backSound.clip = spaceJam;
			} else {
				backSound.clip = ohChild;
			}
		} else {
			backSound.clip = spaceJam;
		}

		if (PlayerPrefs.HasKey ("SpaceShootBackVolume")) {
			backSound.volume = PlayerPrefs.GetFloat ("SpaceShootBackVolume");
		} else {
			backSound.volume = 0.5f;
		}

		backSound.Play ();

		GameObject scoreGO = GameObject.Find ("ScoreCounter");
		scoreGT = scoreGO.GetComponent<GUIText> ();
		scoreGT.text = "0";

		e0.text = "0";
		e1.text = "0";
		e2.text = "0";
		e3.text = "0";
		e4.text = "0";

		if (PlayerPrefs.HasKey ("SpaceShootDestroyMusic")) {
			if (PlayerPrefs.GetString ("SpaceShootDestroyMusic") == "Glass") {
				destroySound.clip = glass;
			} else {
				destroySound.clip = explode;
			}
		} else {
			destroySound.clip = explode;
		}

		if (PlayerPrefs.HasKey ("SpaceShootDestroyVolume")) {
			destroySound.volume = PlayerPrefs.GetFloat ("SpaceShootDestroyVolume");
		} else {
			destroySound.volume = 0.5f;
		}
		if (PlayerPrefs.HasKey ("SpaceShootWinMusic")) {
			if (PlayerPrefs.GetString ("SpaceShootWinMusic") == "Horn") {
				winSound.clip = horn;
			} else {
				winSound.clip = league;
			}
		} else {
			winSound.clip = league;
		}

		if (PlayerPrefs.HasKey ("SpaceShootWinVolume")) {
			winSound.volume = PlayerPrefs.GetFloat ("SpaceShootWinVolume");
		} else {
			winSound.volume = 0.5f;
		}
	}

	static public WeaponDefinition GetWeaponDefinition(WeaponType wt){
		if (W_DEFS.ContainsKey (wt)) {
			return (W_DEFS[wt]);
		}

		return (new WeaponDefinition ());
	}

	void Start(){
		S = this;
		if (screenWidth == 0 && screenHeight == 0) {
			Utils.SetCameraBounds (this.GetComponent<Camera> ());
		} else {
			Utils.SetCameraBounds (screenWidth, screenHeight);
			Screen.SetResolution (wid, hei, true);
		}

		activeWeaponTypes = new WeaponType[weaponDefinitions.Length];
		for (int i = 0; i < weaponDefinitions.Length; i++) {
			activeWeaponTypes [i] = weaponDefinitions [i].type;
		}

	}

	void Update() {
		if (enemiesOnScreen > maxEnemies) {
			enemySpawnPerSec = 0;				
		} else {
			enemySpawnPerSec = enemySpawnSave;
		}

		setActiveLevel ();
	}

	public void SpawnEnemy(){
		int ndx = Random.Range (0, prefabEnemies.Length);
		GameObject go = Instantiate (prefabEnemies [ndx]) as GameObject;

		Vector3 pos = Vector3.zero;
		float xMin = Utils.camBounds.min.x + enemySpawnPadding;
		float xMax = Utils.camBounds.max.x + enemySpawnPadding;

		pos.x = Random.Range (xMin, xMax);
		pos.y = Utils.camBounds.max.y + enemySpawnPadding;
		go.transform.position = pos;

		if (enemiesOnScreen < maxEnemies) {
			enemiesOnScreen++;
			Debug.Log (enemiesOnScreen);
		} else {
			Destroy (go);
		}

		if (!checkActiveEnemy (ndx)) {
			Destroy (go);
			enemiesOnScreen--;
		}

		Invoke ("SpawnEnemy", enemySpawnRate);
	}

	public void DelayedRestart(float delay){
		Invoke ("Restart", delay);
	}

	public void Restart(){
		History_SS.scoreAchieved = Main.scr;
		History_SS.setNewAP ();
		SceneManager.LoadScene ("_Scene_PlayAgain");
	}

	//============ ship destroyed ===============\\
	public void ShipDestroyed(Enemy e){
		if (Random.value <= e.powerUpDropChance) {
			int ndx = Random.Range (0, powerUpFrequency.Length);
			WeaponType puType = powerUpFrequency [ndx];

			GameObject go = Instantiate (prefabPowerUp) as GameObject;
			PowerUp pu = go.GetComponent<PowerUp> ();

			pu.SetType (puType);
			pu.transform.position = e.transform.position;
		}

		destroySound.Play ();
		enemiesOnScreen--;
		//Debug.Log (enemiesOnScreen);

		WhichEnemyDestroyed (e);
	}
	public static int scr;
	//enemy based scoring function...
	public void WhichEnemyDestroyed (Enemy e){

		int enemyScore = 0;
		/*
			Test for each type of enemy name and see which one it is
			after finding the correct enemy:
			1. add 1 to the type of enemy killed display bar on the screen
			2. add the specific enemy score per kill to the total score
		*/
		if (e.name == "Enemy_0(Clone)") {
			int score = int.Parse (e0.text);
			enemyScore = eScore0;
			score++;
			e0.text = "" + score;
		}
		else if (e.name == "Enemy_1(Clone)") {
			int score = int.Parse (e1.text);
			enemyScore = eScore1;
			score++;
			e1.text = "" + score;
		}
		else if (e.name == "Enemy_2(Clone)") {
			int score = int.Parse (e2.text);
			enemyScore = eScore2;
			score++;
			e2.text = "" + score;
		}
		else if (e.name == "Enemy_3(Clone)") {
			int score = int.Parse (e3.text);
			enemyScore = eScore3;
			score++;
			e3.text = "" + score;
		}
		else if (e.name == "Enemy_4(Clone)") {
			int score = int.Parse (e4.text);
			enemyScore = eScore4;
			score++;
			e4.text = "" + score;
		}

		scr = int.Parse (scoreGT.text);
		scr += enemyScore;
		scoreGT.text = scr.ToString ();

		if (scr > HighScore.score) {
			HighScore.score = scr;
		}
	}

	public bool checkActiveEnemy (int ndx) {
		bool result = true;
		if (currentLevel == "Bronze") {
			result = LevelBronze.enemyActive [ndx];
		} else if (currentLevel == "Silver") {
			result = LevelSilver.enemyActive [ndx];
		} if (currentLevel == "Gold") {
			result = LevelGold.enemyActive [ndx];
		}
		return result;
	}

	public GameObject backgroundSquare;
	public Material bronzeBack;
	public Material silverBack;
	public Material goldBack;

	public void setActiveLevel() {
		
		int score = int.Parse (scoreGT.text);

		if (score < LevelBronze.proScr) {
			currentLevel = "Bronze";
			maxEnemies = LevelBronze.maxE;
			backgroundSquare.GetComponent<Renderer> ().material = bronzeBack;
			History_SS.levelAchieved = "Bronze";
		} else if (score >= LevelBronze.proScr && score < LevelSilver.proScr) {
			currentLevel = "Silver";
			maxEnemies = LevelSilver.maxE;
			backgroundSquare.GetComponent<Renderer> ().material = silverBack;
			History_SS.levelAchieved = "Silver";
			if (count == 0) {
				winSound.Play ();
				count++;			
			}
		} else if (score >= LevelSilver.proScr && score < LevelGold.proScr) {
			currentLevel = "Gold";
			maxEnemies = LevelGold.maxE;
			backgroundSquare.GetComponent<Renderer> ().material = goldBack;
			History_SS.levelAchieved = "Gold";
			if (count == 1) {
				winSound.Play ();
				count++;
			}
		} else if (score >= LevelGold.proScr) {
			
			if (count == 2) {
				winSound.Play ();
				count++;
			}

			DelayedRestart (3f);
		}
	}
}
