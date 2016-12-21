using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class History_Status : MonoBehaviour {

	public Text display;

	private static List<UserAccount> temp = new List<UserAccount>();

	void Start() {
		populateList ();
	}

	public void populateList() {
		display.text = " ";
		temp.Clear ();
		foreach (KeyValuePair<string, UserAccount> us in Login.users) {
			temp.Add(us.Value);
		}
		foreach (UserAccount us in temp) {
			display.text += us.retrieveUsername() + "  -  Status: " + us.retrieveStatusString() + "\n"; 		
		}

	}

}
