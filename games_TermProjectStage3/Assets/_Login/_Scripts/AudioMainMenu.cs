using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioMainMenu : MonoBehaviour {

	//create variables to hold all of the audiosources and clips
	public AudioSource mainMenuMusic;

	public AudioClip fix;
	public AudioClip stardust;
	public AudioClip years;
	public AudioClip house;

	//to control the volume
	public Slider volume;

	//at the start
	void Awake() {
		//get the users music preference
		string toPlay = Login.current.retrieveMainMusic ();
		//determine which clip to play based on the user preference
		if (toPlay == "fix") {
			mainMenuMusic.clip = fix;
		} else if (toPlay == "stardust") {
			mainMenuMusic.clip = stardust;
		} else if (toPlay == "years") {
			mainMenuMusic.clip = years;
		} else if (toPlay == "house") {
			mainMenuMusic.clip = house;
		} else {
			mainMenuMusic.clip = stardust;
		}
		//set the volume and play the desired clip
		mainMenuMusic.volume = volume.value;
		mainMenuMusic.Play ();
	}

	//update the volume based on the slider value in the UI
	void Update() {
		mainMenuMusic.volume = volume.value;
	}

	/*
		FUCNTIONS FOR CHOOSING NEW MUSIC CLIPS
		- get the click of each button and assign the desired audio clip to the source and play it

	*/
	public void fixCLick() {
		mainMenuMusic.clip = fix;
		Login.current.setMainMusic ("fix");
		mainMenuMusic.Play ();
		Login.Save ();
	}
	public void stardustCLick() {
		mainMenuMusic.clip = stardust;
		Login.current.setMainMusic ("stardust");
		mainMenuMusic.Play ();
		Login.Save ();
	}
	public void yearsCLick() {
		mainMenuMusic.clip = years;
		Login.current.setMainMusic ("years");
		mainMenuMusic.Play ();
		Login.Save ();
	}
	public void houseCLick() {
		mainMenuMusic.clip = house;
		Login.current.setMainMusic ("house");
		mainMenuMusic.Play ();
		Login.Save ();
	}
}
