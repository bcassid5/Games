using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelGold : MonoBehaviour {

	public Image errorMessage;
	public Text errorTitle;
	public Text errorReport;

	public Toggle gE1;
	public Toggle gE2;
	public Toggle gE3;
	public Toggle gE4;
	public Toggle gE5;

	public InputField goldMaxEnemies;
	public InputField goldProScore;

	public static bool[] enemyActive = {true, true, true, true, true };

	public static string level = "Bronze";

	public static int maxE = 10;
	public static int proScr = 10000;

	private int onCount = 5;


	void Update() {
		if (onCount < LevelSilver.onCount) {			
			errorMessage.enabled = true;
			errorTitle.enabled = true;
			errorReport.enabled = true;
		} else {
			errorMessage.enabled = false;
			errorTitle.enabled = false;
			errorReport.enabled = false;
		}
	}


	public void onE1 () {
		
		checkIsOn (gE1.isOn);

		enemyActive [0] = gE1.isOn;
	}
	public void onE2 () {
		checkIsOn (gE2.isOn);
		enemyActive [1] = gE2.isOn;
	}
	public void onE3 () {
		checkIsOn (gE3.isOn);
		enemyActive [2] = gE3.isOn;
	}
	public void onE4 () {
		checkIsOn (gE4.isOn);
		enemyActive [3] = gE4.isOn;
	}
	public void onE5 () {
		checkIsOn (gE5.isOn);
		enemyActive [4] = gE5.isOn;
	}

	public void setMax() {
		maxE = int.Parse (goldMaxEnemies.text);
	}
	public void setProScore() {
		proScr = int.Parse (goldProScore.text);
	}

	public void checkIsOn(bool b) {
		if (b) {
			onCount++;
		} else {
			onCount--;
		}
	}
}
