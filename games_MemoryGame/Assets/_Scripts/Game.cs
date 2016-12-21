using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	//Holds two copies of each matching image, size = 18
	public Sprite[] teamImages;
	//the default back of the card image
	public Sprite nhlLogo;

	//the array of all buttons, size = 18
	public Button[] cardButtons;

	//text if click on same button twice
	public Text warning;

	/*
	 * REQUIRED GAME SOUNDS
		[0] = cards are the same
		[1] = cards are different
		[2] = card flipped
		[3] = game won
		[4] = game lost
	*/
	public AudioSource cardsSame;
	public AudioSource cardsDifferent;
	public AudioSource cardsFlip;
	public AudioSource gameWon;
	public AudioSource gameLost;

	//determines the sprite to be set
	public static int index;
	//counts amount of cards turned over currently
	private int count;
	//private int save index
	private int save;
	private int gameWinCounter = 0;
	public static int overCount = 0;
	private bool muted = false;


	void Start() {
		randomizeArray ();
		randomizeArray ();
		setAllButtonsActive ();
		warning.text = "";
		setAllToBack ();
	}

	void Update() {
		if (GameUI.scr == 0 && overCount == 0) {
			overCount++;
			delayRestart ();
		}
		if (GameUI.scr == 0) {
			setAllButtonsInactive ();
		}
	}

	public void OnButton () {
		
		warning.text = "";
		cardButtons [index].image.sprite = teamImages [index];
		if (count == 0) {
			cardsFlip.Play ();
			count++;
			save = index;
		} else if (count == 1) {
			if (save == index) {
				//call function to send error, allow another button selection
				warning.text = "Cannot Press Same Button Twice!";
				return;
			}
			cardsFlip.Play ();
			count = 0;
			setAllButtonsInactive ();
			Invoke ("setAllButtonsActive", 1.25f);
			determineIfWin ();
		}
	}

	public void delayRestart() {

		//set score
		GameOver.winScore = GameUI.scr;
		//set winning time
		GameOver.winTime = (int)(Time.time-GameUI.startTime);

		if (GameUI.scr <= 0) {
			gameLost.Play ();
			GameOver.winLoseText = "You Lost!";
			Invoke ("restart",4.7f);
		} else {
			gameWon.Play ();
			GameOver.winLoseText = "You Won!";
			Invoke ("restart",6.4f);
		}


	}

	public void restart() {
		setAllButtonsActive ();
		SceneManager.LoadScene ("_Scene_GameOver");
	}

	public void determineIfWin () {
		if (cardButtons [index].image.sprite == cardButtons [save].image.sprite) {
			//then they won
			//play win sound
			cardsSame.Play ();
			gameWinCounter++;
			Invoke ("setWonToNull", 1);
				
		} else {
			cardsDifferent.Play ();
			GameUI.scr -= 40;
			Invoke ("setPickedBack", 1);
		}

		if (gameWinCounter == 9) {
			Invoke ("delayRestart", 1.5f);
		}
	}

	public void setPickedBack () {
		cardButtons [index].image.sprite = nhlLogo;
		cardButtons [save].image.sprite = nhlLogo;
	}

	public void setWonToNull() {
		cardButtons [index].enabled = false;
		cardButtons [save].enabled = false;
		cardButtons [index].image.color = new Color(0,0,0,0);
		cardButtons [save].image.color = new Color(0,0,0,0);
	}

	public void setAllToBack () {
		for (int i = 0; i < cardButtons.Length; i++) {
			cardButtons [i].image.sprite = nhlLogo;
		}
	}

	public void setButtonSprites () {
		for (int i = 0; i < cardButtons.Length; i++) {
			cardButtons [i].image.sprite = teamImages [i];
		}
	}

	public void randomizeArray() {
		for (int t = 0; t < teamImages.Length; t++ )
		{
			Sprite tmp = teamImages[t];
			int r = Random.Range(t, teamImages.Length);
			teamImages[t] = teamImages[r];
			teamImages[r] = tmp;
		}
	}

	public void setAllButtonsInactive() {
		for (int i = 0; i < 18; i++) {
			cardButtons [i].interactable = false;
		}
	}

	public void setAllButtonsActive() {
		for (int i = 0; i < 18; i++) {
			cardButtons [i].interactable = true;
		}
	}

	public void muteSound () {
		if (muted) {
			cardsSame.volume = 1;
			cardsDifferent.volume = 1;
			cardsFlip.volume = 1;
			gameWon.volume = 1;
			gameLost.volume = 1;
			muted = false;
		} else {
			cardsSame.volume = 0;
			cardsDifferent.volume = 0;
			cardsFlip.volume = 0;
			gameWon.volume = 0;
			gameLost.volume = 0;
			muted = true;
		}
	}
}
