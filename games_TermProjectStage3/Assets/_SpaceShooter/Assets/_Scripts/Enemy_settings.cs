using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy_settings : MonoBehaviour {

	public Material e0;
	public Material e1;
	public Material e2;
	public Material e3;
	public Material e4;

	public Dropdown e0Color;
	public Dropdown e1Color;
	public Dropdown e2Color;
	public Dropdown e3Color;
	public Dropdown e4Color;

	public InputField e0Input;
	public InputField e1Input;
	public InputField e2Input;
	public InputField e3Input;
	public InputField e4Input;

	void Start() {
		e0.color = Color.red;
		e1.color = Color.red;
		e2.color = Color.red;
		e3.color = Color.red;
		e4.color = Color.red;
	}

	public void OnValueChanged() {
		if (e0Color.value == 0) {
			e0.color = Color.red;
		} else if (e0Color.value == 1) {
			e0.color = Color.magenta;
		} else if (e0Color.value == 2) {
			e0.color = Color.yellow;
		}

		if (e1Color.value == 0) {
			e1.color = Color.red;
		} else if (e1Color.value == 1) {
			e1.color = Color.magenta;
		} else if (e1Color.value == 2) {
			e1.color = Color.yellow;	
		}

		if (e2Color.value == 0) {
			e2.color = Color.red;
		} else if (e2Color.value == 1) {
			e2.color = Color.magenta;
		} else if (e2Color.value == 2) {
			e2.color = Color.yellow;
		}

		if (e3Color.value == 0) {
			e3.color = Color.red;
		} else if (e3Color.value == 1) {
			e3.color = Color.magenta;
		} else if (e3Color.value == 2) {
			e3.color = Color.yellow;
		}

		if (e4Color.value == 0) {
			e4.color = Color.red;
		} else if (e4Color.value == 1) {
			e4.color = Color.magenta;
		} else if (e4Color.value == 2) {
			e4.color = Color.yellow;
		}
	}

	public void EditEnemy0() {
		Main.eScore0 = int.Parse (e0Input.text);
	}
	public void EditEnemy1() {
		Main.eScore1 = int.Parse (e1Input.text);
	}
	public void EditEnemy2() {
		Main.eScore2 = int.Parse (e2Input.text);
	}
	public void EditEnemy3() {
		Main.eScore3 = int.Parse (e3Input.text);
	}
	public void EditEnemy4() {
		Main.eScore4 = int.Parse (e4Input.text);
	}
}
