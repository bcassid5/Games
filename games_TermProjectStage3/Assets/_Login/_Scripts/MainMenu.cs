using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class MainMenu : MonoBehaviour {

	public Canvas adminLogin;
	public Canvas regUserLogin;
	public Canvas history;

	private List<UserAccount> items = new List<UserAccount>();
	public static List<UserAccount> blocked = new List <UserAccount> ();

	//text array for list of all usernames
	public Text listOfUsers;

	void Awake() {
		//determine which canvas to load wehther for admin or for regular user
		if (Login.current.retrieveUsername () == "admin") { //if it is admin, open admin screen, close user screen
			adminLogin.enabled = true;
			regUserLogin.enabled = false;
			history.enabled = true;
		} else { //otherwise open the user screen and close the admin screen
			adminLogin.enabled = false;
			regUserLogin.enabled = true;
			history.enabled = true;
		}//end if

		//set the user login history
		setUserLoginHistory();

		//set current users background
		setBackForCurrent();

		//create the list of all usernames
		listOfAllUsers();
	}

	public Text userLogins;

	public void setUserLoginHistory() {
		userLogins.text = Login.current.printLoginsArray ();
	}

	public void listOfAllUsers () {
		//add all the items in the users dictionary to a list
		items.AddRange(Login.users.Values);
		listOfUsers.text = "";
		//for all of the accounts in the new list, make text objects from them
		foreach (UserAccount ur in items) {
			listOfUsers.text += ur.retrieveUsername () + "\n";
		}
		Save ();
	}

	//============USER LIST MODIFIERS (ADMIN ONLY)============\\
	//variables for removing a user
	public InputField removeMe;
	public Text errorOnRemove;

	public void removeUserAt() {
		//initialize a couple variables for use in the function...
		errorOnRemove.text = "";
		string removeKey = removeMe.text;

		//make sure that the user to be removed actually exists first
		if (Login.users.ContainsKey (removeKey)) { //the user exists...
			//temp variable to hold the to remove user
			UserAccount temp = Login.users [removeKey];
			//check to make sure its good to remove
			if (removeKey == "admin") {//if its the admin, you cant do this
				errorOnRemove.color = Color.red;
				errorOnRemove.text = "Cannot remove admin!";
			} else {//otherwise it is okay to remove the user
				Login.users.Remove (removeKey);
				errorOnRemove.color = Color.green;
				errorOnRemove.text = removeKey + " removed successfully!";
				//also remove from blocked if it is in there
				if (blocked.Contains (temp)) {
					blocked.Remove (temp);
					listOfBlockedUsers ();
					Save ();
				}
				//reset the list of users
				items.Clear ();
				listOfAllUsers ();
				//remove all of that players data
				removeLoginData (temp);
				removeMUData (temp);
				removeSSData (temp);
				removeRPSData (temp);
				removeAPData (temp);
			} //end if
		} else {//otherwise the player does not exist
			//send error message to the admin
			errorOnRemove.color = Color.red;
			errorOnRemove.text = "User does not exist!";
		}//end if
		//save contents
		Save();
		Login.Save ();
	}

	/*
		ALL OF THE FOLLOWING FUNCTIONS ARE FOR REMOVING DATA

	*/

	public void removeLoginData(UserAccount ua) {
		List<string> temp = ua.getLoginRemove ();
		foreach (string st in temp) {
			History_Admin.admin_login_HISTORY.Remove (st);
		}
		History_Admin.Save ();
	}
	public void removeAPData(UserAccount ua) {
		List<string> temp = ua.getAPRemove ();
		foreach (string st in temp) {
			History_AP.historyAP.Remove (st);
		}
		History_AP.Save ();
	}
	public void removeSSData(UserAccount ua) {
		List<string> temp = ua.getSSRemove ();
		foreach (string st in temp) {
			History_SS.historySS.Remove (st);
		}
		History_SS.Save ();
	}
	public void removeMUData(UserAccount ua) {
		List<string> temp = ua.getMURemove ();
		foreach (string st in temp) {
			History_MU.historyMU.Remove (st);
		}
		History_MU.Save ();
	}
	public void removeRPSData(UserAccount ua) {
		List<string> temp = ua.getRPSRemove ();
		foreach (string st in temp) {
			History_RPS.historyRPS.Remove (st);
		}
		History_RPS.Save ();
	}


	//============USER CREATION============\\
	//variables for adding a user
	public InputField addMe;
	public Text errorOnAdd;

	public void addUserWith() {
		//create some temp variables...
		string addKey = addMe.text;
		string defaultPassword = addMe.text;
		errorOnAdd.text = "";

		//detemrine if the key already exists in the users dictionary...
		if (Login.users.ContainsKey(addKey)) {//if it does exist already, dont allow anotehr to be made
			//this user already exists, invalid
			errorOnAdd.color = Color.red;
			errorOnAdd.text = "This username already exists!";

		} else {//otherwise it is okay to add the user
			UserAccount newUser = new UserAccount (addKey, defaultPassword);
			errorOnAdd.color = Color.green;
			errorOnAdd.text = addKey+" added successfully!";
			Login.users.Add (addKey, newUser);

			//update the users list
			items.Clear ();
			listOfAllUsers ();
		}//end if
		//save contents
		Save();
		Login.Save ();

	}

	//============PASSWORD STUFF============\\
	//create some temp password variables
	public InputField newPassAdmin;
	public InputField newPassUser;
	public Text[] errorInPass;

	public void changePassword() {
		//initialzie the temp variables
		string newPassKey = "";

		//determine if the admin is changing its password or if its another user
		if (Login.current.retrieveUsername () == "admin") {//get the admin text
			newPassKey = newPassAdmin.text;
		} else {//otherwise get the user text
			newPassKey = newPassUser.text;
		}//end if

		//verify that the password is both different from the current one and different than the username
		if (Login.current.retrievePassword () == newPassKey || newPassKey == Login.current.retrieveUsername()) {//it is both bad
			//display error warnings and dont allow the change
			foreach (Text t in errorInPass) {
				t.color = Color.red;
				t.text = "Cannot have same password!"; 	
			}

		} else {//otherwise its all good to change the password
			//display that the password was changed
			foreach (Text t in errorInPass) {
				t.color = Color.green;
				t.text = "Password changed successfully!";	
			}
			//set the password in the UserAccount
			Login.current.setPassword (newPassKey);
			//save contents
			Login.Save ();
		}//end if
		Save();
	}

	//unblock a user temp variables
	public Text listOfBlocked;
	public Text errorReport;
	public InputField unblockMe;

	//unblock users
	public void unblockUser() {
		//initialize variables
		string unblockKey = unblockMe.text;
		errorReport.text = "";

		//make sure the user to unblock exists
		if (Login.users.ContainsKey (unblockKey)) {
			
			//create a temp useraccount variable for if statements and changing stuff
			UserAccount tempToCheck = Login.users [unblockKey];

			//determine if the desired user is actually blocked
			if (!tempToCheck.retrieveBlockStatus()) {
				//then the user is not blocked, display warnings
				errorReport.color = Color.red;
				errorReport.text = "This user is not blocked";
			} else {//otherwise its good, unblock the person
				errorReport.color = Color.green;
				errorReport.text = tempToCheck.retrieveUsername() + " unblocked successfully!";

				//set block status
				tempToCheck.setBlockStatus (false);
				//return to new status
				tempToCheck.setNewStatus (true);
				//display the status for the admin
				tempToCheck.setStatusString ();
				//reset their password attempts
				tempToCheck.resetPassAttempts ();
				//reset the users password
				tempToCheck.setPassword(tempToCheck.retrieveUsername());
				//remove from the blocked list
				blocked.Remove (tempToCheck);
				Save ();
				//update the blocked list
				listOfBlockedUsers ();

				//save contents
				Login.Save ();
			}//end if
		} else {//otherwise the user doesnt even exist, so do nothing and tell that they dont
			errorReport.color = Color.red;
			errorReport.text = "User does not exist!";
		}//end if
		Save();
	}

	public void listOfBlockedUsers () {
		listOfBlocked.text = "";
		foreach (UserAccount ua in blocked) {
			listOfBlocked.text += ua.retrieveUsername() + "\n";
		}
	}

	//============BACKGROUND============\\

	public Sprite[] images;
	public Image[] backs;

	public void defaultImage() {
		foreach (Image im in backs) {
			im.color = new Color (255, 255, 255, 255);
			im.sprite = images [3];
		}
		Login.current.setBackground ("null");
		Login.Save ();
	}
	public void beachImage() {
		foreach (Image im in backs) {
			im.color = new Color (255, 255, 255, 255);
			im.sprite = images [0];
		}
		Login.current.setBackground ("beach");
		Login.Save ();
	}
	public void windowsImage() {
		foreach (Image im in backs) {
			im.color = new Color (255, 255, 255, 255);
			im.sprite = images [1];
		}
		Login.current.setBackground ("windows");
		Login.Save ();
	}
	public void osXImage() {
		foreach (Image im in backs) {
			im.color = new Color (255, 255, 255, 255);
			im.sprite = images [2];
		}
		Login.current.setBackground ("osX");
		Login.Save ();
	}

	public void setBackForCurrent() {
		if (Login.current.retrieveUserBackground () == "null") {
			defaultImage ();
		} else if (Login.current.retrieveUserBackground () == "beach") {
			beachImage ();
		} else if (Login.current.retrieveUserBackground () == "windows") {
			windowsImage ();
		} else if (Login.current.retrieveUserBackground () == "osX") {
			osXImage ();
		}	
	}

	//============SAVING USERS============\\
	//save function to save the data
	public static void Save() {
		//first create a binary formatter and a file to save the items in
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/blocked.dat");

		//next we need to determine the data to save, which in our case is the user account settings and data
		//already created and such in a dictionary format (Dictionary<string, UserAccount> users)
		//serialize this data and add it to the save folder

		bf.Serialize (file, blocked);
		//close the file
		file.Close();
	}

	//load function to get the data that was saved
	public static void Load() {
		//first determine if the file exists yet
		if (File.Exists (Application.persistentDataPath + "/blocked.dat")) {
			//if it does, make a binary formatter to read the data, and open the file
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/blocked.dat", FileMode.Open);

			//create a new storage container and assign it to the data in the file (with a cast****)
			List<UserAccount> container = (List<UserAccount>)bf.Deserialize(file);
			//and close the file, we're done with it
			file.Close ();

			//finally, assign the container to the list we have here to allow access
			blocked = container;
		}
	}


	//=============== CLOSING ALL OF THE PANELS AT ONCE! ===============\\

	public GameObject[] panels;

	public void closeAllPanels() {
		for (int i = 0; i < panels.Length; i++) {
			panels [i].SetActive (false);
		}
	}
}
