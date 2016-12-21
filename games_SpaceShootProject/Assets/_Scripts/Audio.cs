using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio : MonoBehaviour {

	public AudioSource spaceJam;
	public AudioSource ohChild;
	public AudioSource ambient;

	public AudioSource horn;
	public AudioSource league;

	public AudioSource explode;
	public AudioSource glass;

	public AudioSource laser;
	public AudioSource gun;

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
			Menu.stopBackSound ();
			ambient.Play ();
			PlayerPrefs.SetString ("SpaceShootBackMusic", "Ambient");
		} else if (back.value == 1) {
			Menu.stopBackSound ();
			ohChild.Play ();
			PlayerPrefs.SetString ("SpaceShootBackMusic", "OhChild");
		} else if (back.value == 2) {
			Menu.stopBackSound ();
			spaceJam.Play ();
			PlayerPrefs.SetString ("SpaceShootBackMusic", "SpaceJam");
		}
	}

	public void OnChangeWin () {
		Menu.pauseBackSound ();
		if (win.value == 0) {
			horn.Play ();
			PlayerPrefs.SetString ("SpaceShootWinMusic", "Horn");
			Invoke("delayedPlay",6.8f);
		} else if (win.value == 1) {
			league.Play ();
			PlayerPrefs.SetString ("SpaceShootWinMusic", "League");
			Invoke("delayedPlay",5.8f);
		}

	}

	public void OnChangeDestroy () {
		Menu.pauseBackSound ();
		if (destroy.value == 0) {
			explode.Play ();
			PlayerPrefs.SetString ("SpaceShootDestroyMusic", "Explode");
			Invoke("delayedPlay", 2.4f);
		} else if (destroy.value == 1) {
			glass.Play ();
			PlayerPrefs.SetString ("SpaceShootDestroyMusic", "Glass");
			Invoke("delayedPlay", 1f);
		}
	}

	public void OnChangeShoot () {
		Menu.pauseBackSound ();
		if (shoot.value == 0) {
			laser.Play ();
			PlayerPrefs.SetString ("SpaceShootShootMusic", "Laser");
			Invoke("delayedPlay",1f);
		} else if (shoot.value == 1) {
			gun.Play ();
			PlayerPrefs.SetString ("SpaceShootShootMusic", "Gun");
			Invoke("delayedPlay",1f);
		}
	}

	public void OnSliderBack () {
		ambient.volume = backS.value;
		ohChild.volume = backS.value;
		spaceJam.volume = backS.value;
		PlayerPrefs.SetFloat ("SpaceShootBackVolume", backS.value);
	}

	public void OnSliderWin () {
		league.volume = winS.value;
		horn.volume = winS.value;
		PlayerPrefs.SetFloat ("SpaceShootWinVolume", winS.value);
	}

	public void OnSliderDestroy () {
		explode.volume = destoryS.value;
		glass.volume = destoryS.value;
		PlayerPrefs.SetFloat ("SpaceShootDestroyVolume", destoryS.value);
	}

	public void OnSliderShoot () {
		gun.volume = shootS.value;
		laser.volume = shootS.value;
		PlayerPrefs.SetFloat ("SpaceShootShootVolume", shootS.value);
	}

	public void delayedPlay() {
		Menu.playBackSound ();
	}
}
