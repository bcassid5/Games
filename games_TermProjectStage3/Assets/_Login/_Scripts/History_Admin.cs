using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class History_Admin : MonoBehaviour {

	/*
		NOTES FOR ALL OF THE HISTORY FILES
		The history files are to maintain all of the game play and also keep track of which user has played
		the games. these files allow for both posting the saved history information to Text files in the UI system.
		They also allow for the saving of each users history in order to allow for the removal of this data easily.


	*/
	public Text display;

	public static List<string> admin_login_HISTORY = new List<string>();

	void Start() {
		
		setText ();

	}

	public static void setNewAP() {
		string st = Login.current.retrieveUsername () + "\n" + Login.current.getDateTime () + "\nTime Spent: " + Login.current.getTotalTime () + " seconds";
		admin_login_HISTORY.Add (st);
		Login.current.addToLoginRemove (st);
		Save ();
	}
	public static string printValues() {
		string textToReturn = "";

		foreach (string st in admin_login_HISTORY) {
			textToReturn += st + "\n\n";
		}

		return textToReturn;
	}

	public void setText() {
		display.text = "";
		display.text = printValues();
	}




	//============SAVING USERS============\\
	//save function to save the data
	public static void Save() {
		//first create a binary formatter and a file to save the items in
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/adminHISTORY.dat");

		//next we need to determine the data to save, which in our case is the user account settings and data
		//already created and such in a dictionary format (Dictionary<string, UserAccount> users)
		//serialize this data and add it to the save folder

		bf.Serialize (file, admin_login_HISTORY);
		//close the file
		file.Close();
	}

	//load function to get the data that was saved
	public static void Load() {
		//first determine if the file exists yet
		if (File.Exists (Application.persistentDataPath + "/adminHISTORY.dat")) {
			//if it does, make a binary formatter to read the data, and open the file
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/adminHISTORY.dat", FileMode.Open);

			//create a new storage container and assign it to the data in the file (with a cast****)
			List<string> container = (List<string>)bf.Deserialize(file);
			//and close the file, we're done with it
			file.Close ();

			//finally, assign the container to the list we have here to allow access
			admin_login_HISTORY = container;
		}
	}
}
