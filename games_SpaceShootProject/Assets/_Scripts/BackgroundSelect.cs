using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackgroundSelect : MonoBehaviour {

	public Sprite highway;
	public Sprite starfield;
	public Sprite earth;
	public Sprite milkyWay;

	public Texture highwayT;
	public Texture starfieldT;
	public Texture earthT;
	public Texture milkyWayT; 

	public Image previewer;

	public Dropdown selector;

	public Material back;

	public InputField wide;
	public InputField high;

	public void OnValueChanged() {
		if (selector.value == 0) {
			previewer.sprite = starfield;
		} else if (selector.value == 1) {
			previewer.sprite = milkyWay;
		} else if (selector.value == 2) {
			previewer.sprite = earth;
		} else if (selector.value == 3) {
			previewer.sprite = highway;
		}
	}

	public void OnSetClick() {
		//sets background to selected dropdown value
		if (selector.value == 0) {
			back.mainTexture = starfieldT;
		} else if (selector.value == 1) {
			back.mainTexture = milkyWayT;
		} else if (selector.value == 2) {
			back.mainTexture = earthT;
		} else if (selector.value == 3) {
			back.mainTexture = highwayT;
		}
	}

	public void GetInputFieldWidthX() {
		Main.screenWidth = float.Parse (wide.text);
		Main.wid = int.Parse (wide.text);
	}

	public void GetInputFieldHeightY() {
		Main.screenHeight = float.Parse (high.text);
		Main.hei = int.Parse (high.text);
	}
}