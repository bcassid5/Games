using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_RPS : MonoBehaviour {

	//button variables
	public Button rock;
	public Button paper;
	public Button scissors;
	public Canvas playAgain;

	//Counting integer variables
	public static int gamesPlayed = 0;

	//computer variables
	private int compChoiceInt;
	public string compChoiceString;
	public static int compScore = 0;

	public GameObject compBlock;

	//Player variables
	private string playerChoiceString;
	public static int playerScore = 0;
	public GameObject playerBlock;

	//materials variables
	public Material rockMat;
	public Material paperMat;
	public Material scissorsMat;

	//text variables
	public Text player;
	public Text comp;
	public Text winnerLine;

	void Awake() {
		History_RPS.date_time = System.DateTime.Now.ToString ();
		History_RPS.username = Login.current.retrieveUsername ();
	}

	// Use this for initialization
	void Start () {

		//make the buttons good for clicking
		rock.enabled = true;
		paper.enabled = true;
		scissors.enabled = true;

		//set the gameObjects
		compBlock = GameObject.Find ("CompBlock");
		playerBlock = GameObject.Find ("PlayerBlock");

		//hide the play again panel while game is running
		playAgain.enabled = false;

		//set all variables to default
		winnerLine.text = "";
		playerScore = 0;
		compScore = 0;
		gamesPlayed = 0;
	}

	// Update is called once per frame
	void Update () {
		//determine if the game is over
		if (gamesPlayed == 10) {
			//reset the gamesPlayed value to 0
			gamesPlayed = 0;
			//disable buttons, show play again screen
			rock.enabled = false;
			paper.enabled = false;
			scissors.enabled = false;
			playAgain.enabled = true;
			//determine the winner of the Game
			DetermineGameWinner ();
			History_RPS.setNewAP ();
		}
		//always display the current score
		player.text = "" + playerScore;
		comp.text = "" + compScore;
	}
	//rock clicked
	public void OnRockClick(){
		//set the players choice
		playerChoiceString = "Ro";
		//change the material to show a rock
		playerBlock.GetComponent<Renderer> ().material = rockMat;
		//create the computer choice
		CreateRandomComputer ();
		//increment the games played
		gamesPlayed++;
		//determine who won the hand
		DetermineRoundWinner ();
	}
	//paper clicked
	public void OnPaperClick(){
		//set the players choice
		playerChoiceString = "Pa";
		//change the material to show paper
		playerBlock.GetComponent<Renderer> ().material = paperMat;
		//create the computer choice
		CreateRandomComputer ();
		//increment the games played
		gamesPlayed++;
		//determine who won the hand
		DetermineRoundWinner ();
	}
	//scissors clicked
	public void OnScissorsClick(){
		//set the players choice
		playerChoiceString = "Sc";
		//change the material to show scissors
		playerBlock.GetComponent<Renderer> ().material = scissorsMat;
		//create the computer choice
		CreateRandomComputer ();
		//increment the games played
		gamesPlayed++;
		//determine who won the hand
		DetermineRoundWinner ();
	}

	public void CreateRandomComputer (){
		//create random computer player value and object
		compChoiceInt = Random.Range (1,4);
		if (compChoiceInt == 1) {
			compBlock.GetComponent<Renderer> ().material = rockMat;
			compChoiceString = "Ro";
		} else if (compChoiceInt == 2) {
			compBlock.GetComponent<Renderer> ().material = paperMat;
			compChoiceString = "Pa";
		} else if (compChoiceInt == 3) {
			compBlock.GetComponent<Renderer> ().material = scissorsMat;
			compChoiceString = "Sc";
		}
	}

	public void DetermineRoundWinner(){
		//determines which player has won the round being played
		string tempText = "";

		if (playerChoiceString == compChoiceString) {
			//tie round!
			tempText = "It's a Tie!";
		} else if (playerChoiceString == "Ro" && compChoiceString == "Sc") {
			//player wins
			playerScore++;

			tempText = "Player Wins!";
		} else if (playerChoiceString == "Sc" && compChoiceString == "Pa") {
			//player wins
			playerScore++;
			tempText = "Player Wins!";
		} else if (playerChoiceString == "Pa" && compChoiceString == "Ro") {
			//player wins
			playerScore++;
			tempText = "Player Wins!";
		} else {
			//computer wins
			compScore++;
			tempText = "Computer Wins!";
		}
		winnerLine.text = tempText;
	}

	public void DetermineGameWinner(){

		string tempText = "";

		//determines which player has the most points
		if (playerScore == compScore) {
			//its a tie game
			tempText = "It's a Tie Game!";
			History_RPS.result = "Tie";
		}  else if (playerScore > compScore) {
			//the player wins
			tempText = "The Player Has Won the Game!";
			History_RPS.result = "Player Win";
		}  else {
			//the computer wins
			tempText = "The Computer Has Won the Game!";
			History_RPS.result = "Computer Win";
		}
		winnerLine.text = tempText;
	}

}
