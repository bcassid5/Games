using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LogOut : MonoBehaviour {

	/*
	 * LOGGING OUT PROCEEDINGS 
	 * 1. first make sure the users sign out time is saved for the time logged in
	 * 2. create a new history tag
	 * 3. make a new login for the specific user login history
	 * 4. save the login system
	 * 5. load the login scene (essentially logging out)
	 * 
	 * */

	public void restart() {
		
		Login.current.setTimeOut ();
		History_Admin.setNewAP ();
		makeLogin ();
		Login.Save ();
		SceneManager.LoadScene ("_Scene_Login");

	}
	//making a login string for the current user login array
	public string makeLogin() {
		string st = "";
		st = Login.current.printLoginsArray ();

		st = "Date/Time: " + System.DateTime.Now.ToString () + "  -  Time Spent: " + Login.current.getTotalTime () + " seconds\n\n";
		Login.current.addToLogins (st);

		return st;
	}

}
