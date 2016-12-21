using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio : MonoBehaviour {

	public AudioSource backgroundMusic;
	public AudioSource winningMusic;
	public AudioSource explosionMusic;
	public AudioSource shootingMusic;

	public AudioClip spaceJam;
	public AudioClip ohChild;
	public AudioClip ambient;
	public AudioClip horn;
	public AudioClip league;
	public AudioClip explode;
	public AudioClip glass;
	public AudioClip laser;
	public AudioClip gun;

	public Dropdown back;
	public Dropdown win;
	public Dropdown destroy;
	public Dropdown shoot;

	public Slider backS;
	public Slider winS;
	public Slider destoryS;
	public Slider shootS;

	public void OnChangeBack () {
		if (back.value == 0) {
			backgroundMusic.clip = ambient;
			PlayerPrefs.SetString ("SpaceShootBackMusic", "Ambient");
		} else if (back.value == 1) {
			
			backgroundMusic.clip = ohChild;
			PlayerPrefs.SetString ("SpaceShootBackMusic", "OhChild");
		} else if (back.value == 2) {
			
			backgroundMusic.clip = spaceJam;
			PlayerPrefs.SetString ("SpaceShootBackMusic", "SpaceJam");
		}
		backgroundMusic.Play ();
		Invoke ("stopMusic", 5f);
	}

	public void stopMusic() {
		backgroundMusic.Stop ();
	}

	public void OnChangeWin () {
		
		if (win.value == 0) {
			winningMusic.clip = horn;
			PlayerPrefs.SetString ("SpaceShootWinMusic", "Horn");
			Invoke("delayedPlay",6.8f);
		} else if (win.value == 1) {
			winningMusic.clip = league;
			PlayerPrefs.SetString ("SpaceShootWinMusic", "League");
			Invoke("delayedPlay",5.8f);
		}
		winningMusic.Play ();
	}

	public void OnChangeDestroy () {
		if (destroy.value == 0) {
			explosionMusic.clip = explode;
			PlayerPrefs.SetString ("SpaceShootDestroyMusic", "Explode");
			Invoke("delayedPlay", 2.4f);
		} else if (destroy.value == 1) {
			explosionMusic.clip = glass;
			PlayerPrefs.SetString ("SpaceShootDestroyMusic", "Glass");
			Invoke("delayedPlay", 1f);
		}
		explosionMusic.Play ();
	}

	public void OnChangeShoot () {
		
		if (shoot.value == 0) {
			shootingMusic.clip = laser;
			PlayerPrefs.SetString ("SpaceShootShootMusic", "Laser");
			Invoke("delayedPlay",1f);
		} else if (shoot.value == 1) {
			shootingMusic.clip = gun;
			PlayerPrefs.SetString ("SpaceShootShootMusic", "Gun");
			Invoke("delayedPlay",1f);
		}
		shootingMusic.Play ();
	}

	public void OnSliderBack () {
		backgroundMusic.volume = backS.value;
		PlayerPrefs.SetFloat ("SpaceShootBackVolume", backS.value);
	}

	public void OnSliderWin () {
		winningMusic.volume = winS.value;

		PlayerPrefs.SetFloat ("SpaceShootWinVolume", winS.value);
	}

	public void OnSliderDestroy () {
		explosionMusic.volume = destoryS.value;

		PlayerPrefs.SetFloat ("SpaceShootDestroyVolume", destoryS.value);
	}

	public void OnSliderShoot () {
		shootingMusic.volume = shootS.value;

		PlayerPrefs.SetFloat ("SpaceShootShootVolume", shootS.value);
	}
}
