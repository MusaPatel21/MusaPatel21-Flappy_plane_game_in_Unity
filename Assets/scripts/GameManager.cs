using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {
	public GameObject playui;
	public InputField username;
	public GameObject LeaderBoard;
	public delegate void GameDelegate ();
	public static event GameDelegate OnGameStarted;//geri sayım bittiğinde plane'de yapılacak işlemler
	public static event GameDelegate OnGameOverConfirmed;//replay butonuna tıklandığında plane'de yapılacak işlemler ve Parallax objelerin durumu

	public static GameManager Instance;//GameManagera dışardan başka bir scriptin erişmesi için referans(Singleton)

	public GameObject startPage;
	public GameObject gameOverPage;
	public GameObject countdownPage;
	public Text scoreText;

	enum PageState{
		None,
		Start,
		GameOver,
		Countdown
	}

	int score = 0;
	bool gameOver = true;

	public bool GameOver { get { return gameOver; } }

	void Awake(){
	
		Instance = this;
	}

	void OnEnable(){
		CountdownText.OnCountdownFinished += OnCountdownFinished;
		TapController.OnPlayerDied += OnPlayerDied;
		TapController.OnPlayerScored += OnPlayerScored;
	
	}

	void OnDisable(){
		CountdownText.OnCountdownFinished -= OnCountdownFinished;
		TapController.OnPlayerDied -= OnPlayerDied;
		TapController.OnPlayerScored -= OnPlayerScored;
	
	}

	void OnCountdownFinished(){
		SetPageState (PageState.None);
		OnGameStarted ();//event TapControllera yollar
		score = 0;
		gameOver = false;
	
	}

	void OnPlayerDied(){
		gameOver = true;
		int savedScore = PlayerPrefs.GetInt ("highscore");
		if (score > savedScore) {
			PlayerPrefs.SetInt ("highscore", score);
		
		}
		SetPageState (PageState.GameOver);
	}

	void OnPlayerScored(){
	
		score++;
		scoreText.text = score.ToString();
	}

	void SetPageState(PageState state){

		switch (state) {

		case PageState.None:
			startPage.SetActive (false);
			gameOverPage.SetActive (false);
			countdownPage.SetActive (false);
			break;
		case PageState.Start:
			startPage.SetActive (true);
			gameOverPage.SetActive (false);
			countdownPage.SetActive (false);
			break;
		case PageState.GameOver:
			startPage.SetActive (false);
			gameOverPage.SetActive (true);
			countdownPage.SetActive (false);
			break;
		case PageState.Countdown:
			startPage.SetActive (false);
			gameOverPage.SetActive (false);
			countdownPage.SetActive (true);
			break;

		}
	}

	public void ConfirmedGameOver(){
		//replay butonuna tıklandığında aktive olur.
		OnGameOverConfirmed();//event TapControllera yollar
		scoreText.text="0";
		SetPageState (PageState.Start);
	}
	string usernam;
	public void StartGame(){
		if (username.text.Length >= 3)
        {
			Invoke("playclose",0);
			Invoke("SG", 0);
			PlayerPrefs.SetString(usernam, username.text);
		}
	}
	public void lbclose()
    {
		LeaderBoard.SetActive(false);
    }
	public void lbopen()
    {
		LeaderBoard.SetActive(true);
    }
	public void playclose()
	{
		playui.SetActive(false);
	}
	public void playopen()
	{
		playui.SetActive(true);
	}
	public void SG()
    {
		SetPageState(PageState.Countdown);
	}
}
