using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSilver : MonoBehaviour {

	public Image errorMessage;
	public Text errorTitle;
	public Text errorReport;

	public Toggle sE1;
	public Toggle sE2;
	public Toggle sE3;
	public Toggle sE4;
	public Toggle sE5;

	public InputField silverMaxEnemies;
	public InputField silverProScore;

	public static bool[] enemyActive = {true, true, true, true, false };

	public static string level = "Silver";

	public static int maxE = 4;
	public static int proScr = 5000;

	public static int onCount = 4;


	void Update() {
		if (onCount < LevelBronze.onCount) {
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
		Debug.Log (onCount);
		checkIsOn (sE1.isOn);
		Debug.Log (onCount);
		enemyActive [0] = sE1.isOn;
	}
	public void onE2 () {
		checkIsOn (sE2.isOn);
		enemyActive [1] = sE2.isOn;
	}
	public void onE3 () {
		checkIsOn (sE3.isOn);
		enemyActive [2] = sE3.isOn;
	}
	public void onE4 () {
		checkIsOn (sE4.isOn);
		enemyActive [3] = sE4.isOn;
	}
	public void onE5 () {
		checkIsOn (sE5.isOn);
		enemyActive [4] = sE5.isOn;
	}

	public void setMax() {
		maxE = int.Parse (silverMaxEnemies.text);
	}
	public void setProScore() {
		proScr = int.Parse (silverProScore.text);
	}

	public void checkIsOn(bool b) {
		if (b) {
			onCount++;
		} else {
			onCount--;
		}
	}
}
