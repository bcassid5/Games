﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class History_AP : MonoBehaviour {

	public Text ap_display;

	public static List<string> historyAP = new List<string>();

	public static string username;
	public static int scoreAchieved;
	public static string date_time;

	void Start() {
		setText ();

	}

	public static void setNewAP() {
		string st = username + "  -  " + date_time + "  -  Score: " + scoreAchieved;
		historyAP.Add (st);
		Login.current.addToAPRemove (st);
		Save ();
	}
	public static string printValues() {
		string textToReturn = "";

		foreach (string st in historyAP) {
			textToReturn += st + "\n";
		}

		return textToReturn;
	}

	public void setText() {
		ap_display.text = "";
		ap_display.text = printValues();
	}




	//============SAVING USERS============\\
	//save function to save the data
	public static void Save() {
		//first create a binary formatter and a file to save the items in
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/apHistory.dat");

		//next we need to determine the data to save, which in our case is the user account settings and data
		//already created and such in a dictionary format (Dictionary<string, UserAccount> users)
		//serialize this data and add it to the save folder

		bf.Serialize (file, historyAP);
		//close the file
		file.Close();
	}

	//load function to get the data that was saved
	public static void Load() {
		//first determine if the file exists yet
		if (File.Exists (Application.persistentDataPath + "/apHistory.dat")) {
			//if it does, make a binary formatter to read the data, and open the file
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/apHistory.dat", FileMode.Open);

			//create a new storage container and assign it to the data in the file (with a cast****)
			List<string> container = (List<string>)bf.Deserialize(file);
			//and close the file, we're done with it
			file.Close ();

			//finally, assign the container to the list we have here to allow access
			historyAP = container;
		}
	}
}