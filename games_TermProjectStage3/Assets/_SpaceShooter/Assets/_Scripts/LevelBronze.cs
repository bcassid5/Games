using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelBronze : MonoBehaviour {

	public Toggle bE1;
	public Toggle bE2;
	public Toggle bE3;
	public Toggle bE4;
	public Toggle bE5;

	public InputField bronzeMaxEnemies;
	public InputField bronzeProScore;

	public static bool[] enemyActive = {true, true, true, false, false };

	public static string level = "Bronze";

	public static int maxE = 2;
	public static int proScr = 2500;

	public static int onCount = 3;

	public void onE1 () {
		checkIsOn (bE1.isOn);
		enemyActive [0] = bE1.isOn;
	}
	public void onE2 () {
		checkIsOn (bE2.isOn);
		enemyActive [1] = bE2.isOn;
	}
	public void onE3 () {
		checkIsOn (bE3.isOn);
		enemyActive [2] = bE3.isOn;
	}
	public void onE4 () {
		checkIsOn (bE4.isOn);
		enemyActive [3] = bE4.isOn;
	}
	public void onE5 () {
		checkIsOn (bE5.isOn);
		enemyActive [4] = bE5.isOn;
	}

	public void setMax() {
		maxE = int.Parse (bronzeMaxEnemies.text);
	}
	public void setProScore() {
		proScr = int.Parse (bronzeProScore.text);
	}

	public void checkIsOn(bool b) {
		if (b) {
			onCount++;
		} else {
			onCount--;
		}
	}
}
