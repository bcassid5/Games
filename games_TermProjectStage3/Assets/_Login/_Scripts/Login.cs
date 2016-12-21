using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Login : MonoBehaviour {

	//Canvas variables to set enable on clicks
	public Canvas main;
	public Canvas info;

	//Inputfields to get the username and password
	public InputField username;
	public InputField password;

	//text for error messages
	public Text errorMessage;

	//a List to hold all the possible users in the sytem
	public static Dictionary<string, UserAccount> users = new Dictionary<string, UserAccount>();

	//know which user is signed in currently
	public static UserAccount current;

	//============INITITAL STATES============\\
	//set awake for canvas enables
	void Awake() {
		main.enabled = true;
		info.enabled = false;

		//load any saved data
		Load ();
		if (users.ContainsKey ("admin")) {
			//contintue normally
		} else {
			UserAccount admin = new UserAccount ("admin", "admin");
			users.Add ("admin", admin);
			Save ();
		}
		//LOAD ALL FILES
		Load ();
		MainMenu.Load ();
		History_AP.Load ();
		History_MU.Load ();
		History_RPS.Load ();
		History_SS.Load ();
		History_Admin.Load ();


	}

	//============BUTTONS FUNCTIONALITY============\\
	//gameobject for new user password panel
	public GameObject newUserPanel;

	public void _login () {
		string error = "";
		//check to make sure the username exists
		if (users.ContainsKey (username.text)) {
			//determine if admin or not...
			//if admin then open special admin windows
			UserAccount tempForLogin = users [username.text];
			current = users [username.text];

			if (tempForLogin.retrieveBlockStatus ()) {
				error = "The user is currently BLOCKED";
				tempForLogin.setStatusString ();
			} else if (username.text == "admin") {
				if (tempForLogin.retrievePassword () == password.text) {//the password is correct continue with login info
					error = "Loading menu for you "+tempForLogin.retrieveUsername()+"...";
					tempForLogin.setNewStatus (false);
					tempForLogin.setStatusString ();
					tempForLogin.setTimeIn ();
					tempForLogin.setDateTime ();
					SceneManager.LoadScene ("_Scene_MainMenu");
				} else {
					//display an error message that tells the user that the password was entered incorrectly
					error = "Incorrect Password!";
				}//end if

			} else { //if not then load regular user windows with their settings
				if (tempForLogin.retrievePassword () == password.text) {//the password is correct
					if (tempForLogin.retrieveNewStatus ()) {//determine if the account is new
						newUserPanel.SetActive (true);
						tempForLogin.setStatusString ();
					} else {//otherwise login, reset attempts, set all necessary login info
						tempForLogin.resetPassAttempts ();
						error = "Loading menu for you "+tempForLogin.retrieveUsername()+"...";
						tempForLogin.setStatusString ();
						tempForLogin.setTimeIn ();
						tempForLogin.setDateTime ();
						SceneManager.LoadScene ("_Scene_MainMenu");
					}//end if
				} else {//then the password is wrong
					//display error message for wrong password
					error = "Incorrect Password!";
					tempForLogin.incrementAttempts ();
					if (tempForLogin.retrievePassAttempts () == 3) {//set a new block on the
						tempForLogin.setBlockStatus (true);
						tempForLogin.setNewStatus (false);
						tempForLogin.setStatusString ();
						error = "User has now been BLOCKED";
						MainMenu.blocked.Add(tempForLogin);
						MainMenu.Save ();
					}//end if
				}//end if	
			}//end if
		} else {
			//display error message for unexisting user
			error = "User Does Not Exist!";
		}//end if
		//display the message
		errorMessage.color = Color.black;
		errorMessage.text = error;
		//save the login
		Save ();
	}

	//=== LOGIN BASICS ===\\
	public void _exit () {
		Save ();
		Application.Quit ();
	}
	public void _info () {
		main.enabled = false;
		info.enabled = true;
	}
	public void _back () {
		main.enabled = true;
		info.enabled = false;
	}

	//============FIRST TIME PASSWORD CHANGE============\\
	//variable for input field and error text
	public InputField newPassInput;
	public Text errorOnNewPass;

	public void newUserPassword() {//first time login user resets the password
		string newPassKey = newPassInput.text;
		//make sure the password is valid
		if (newPassKey == "") {
			errorOnNewPass.text = "Must enter a password.";
		} else if (newPassKey == current.retrieveUsername ()) {
			errorOnNewPass.text = "Password cannot be username!";
		} else {//if no errors are present, we can finally assign the password inputted
			current.setPassword (newPassKey);
			current.setNewStatus (false);
			newUserPanel.SetActive (false);
		}//end if
		//save the new password
		Save ();
	}

	//============SAVING USERS============\\
	//save function to save the data
	public static void Save() {
		//first create a binary formatter and a file to save the items in
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/userAccounts.dat");

		//next we need to determine the data to save, which in our case is the user account settings and data
		//already created and such in a dictionary format (Dictionary<string, UserAccount> users)
		//serialize this data and add it to the save folder

		bf.Serialize (file, users);
		//close the file
		file.Close();
	}

	//load function to get the data that was saved
	public static void Load() {
		//first determine if the file exists yet
		if (File.Exists (Application.persistentDataPath + "/userAccounts.dat")) {
			//if it does, make a binary formatter to read the data, and open the file
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/userAccounts.dat", FileMode.Open);

			//create a new storage container and assign it to the data in the file (with a cast****)
			Dictionary<string, UserAccount> container = (Dictionary<string,UserAccount>)bf.Deserialize(file);
			//and close the file, we're done with it
			file.Close ();

			//finally, assign the container to the list we have here to allow access
			users = container;
		}
	}

}



//============MAKING A NEW PRIVATE CLASS FOR ALL OF THE PLAYERS============\\
[System.Serializable]
public class UserAccount
{
	//User information variables
	private string username;
	private string password;
	private bool newStatus;
	private bool blockStatus;
	private int passAttempts;

	//user prefs variables for login screen
	private string mainMusic;
	private string backgroundImage;

	//User values for history
	private string status;
	private float timeLoggedIn;
	private float timeLoggedOut;
	private string date_time;
	private string logins;

	//for history variables
	public void addToLogins(string st) {
		logins += st;
	}
	public string printLoginsArray() {
		return logins;
	}
	public void resetLogins() {
		logins = "";
	}
	public void setTimeIn() {
		timeLoggedIn = Time.time;
	}
	public void setTimeOut() {
		timeLoggedOut = Time.time;
	}
	public int getTotalTime() {
		return (int)(timeLoggedOut - timeLoggedIn);
	}
	public void setDateTime() {
		date_time = System.DateTime.Now.ToString ();
	}
	public string getDateTime() {
		return date_time;
	}

	//constructing the username and password upon creation of a new user
	public UserAccount (string user, string pass) {
		username = user;
		password = pass;
		newStatus = true;
		blockStatus = false;
		passAttempts = 0;
		setStatusString ();
	}

	//=== GETTER FUNCTIONS ===\\
	public string retrieveMainMusic(){
		return mainMusic;
	}
	public string retrieveUsername () {
		return username;
	}

	public string retrievePassword () {
		return password;
	}

	public bool retrieveNewStatus() {
		return newStatus;
	}

	public bool retrieveBlockStatus() {
		return blockStatus;
	}

	public int retrievePassAttempts() {
		return passAttempts;
	}

	public string retrieveUserBackground() {
		return backgroundImage;
	}
	public string retrieveStatusString() {
		return status;
	}

	//=== SETTER FUNCTIONS ===\\
	public void setStatusString() {
		if (blockStatus) {
			status = "BLOCKED";
		} else if (newStatus) {
			status = "NEW";
		} else {
			status = "NORMAL";
		}
	}

	public void setPassword(string pass){
		password = pass;	
	}

	public void setNewStatus(bool stat) {
		newStatus = stat;
	}

	public void setBlockStatus(bool stat) {
		blockStatus = stat;
	}

	public void setBackground(string back) {
		backgroundImage = back;
	}

	public void setMainMusic(string main) {
		mainMusic = main;
	}

	//=== INCREMENTAL FUNCTIONS ===\\
	public void incrementAttempts() {
		passAttempts++;
	}
	public void resetPassAttempts() {
		passAttempts = 0;
	}

	private List<string> loginRemove = new List<string>();

	public void addToLoginRemove(string st) {
		loginRemove.Add (st);
	}
	public List<string> getLoginRemove() {
		return loginRemove;
	}


	private List<string> apRemove = new List<string>();

	public void addToAPRemove(string st) {
		apRemove.Add (st);
	}
	public List<string> getAPRemove() {
		return apRemove;
	}


	private List<string> ssRemove = new List<string>();

	public void addToSSRemove(string st) {
		ssRemove.Add (st);
	}
	public List<string> getSSRemove() {
		return ssRemove;
	}


	private List<string> muRemove = new List<string>();

	public void addToMURemove(string st) {
		muRemove.Add (st);
	}
	public List<string> getMURemove() {
		return muRemove;
	}

	private List<string> rpsRemove = new List<string>();

	public void addToRPSRemove(string st) {
		rpsRemove.Add (st);
	}
	public List<string> getRPSRemove() {
		return rpsRemove;
	}
}