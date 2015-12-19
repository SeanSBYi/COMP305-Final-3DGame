using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameRuleCtrl : MonoBehaviour {
	// Public Instance Values.
	public float timeRemaining = 5.0f * 60.0f;
	public bool gameOver = false;
    public bool gameClear = false;

	public float sceneChangeTime = 3.0f;
	public Text txtTimer;
	public Text txtScore;

	public Text playerHP;
	public Text playerScore;

	public RawImage rawClear;
	public RawImage rawGameOver;

	private AudioSource[] _audioSources;
	private AudioSource gameOverAudioSource;
	private AudioSource gameWinAudioSource;

	// Private Instance Values.
	private int dataScore;
	private int dataTimer;

	private string loadLevelName;


	// Use this for initialization
	void Start () {
		this._audioSources = gameObject.GetComponents<AudioSource> ();
		this.gameWinAudioSource = this._audioSources[0];
		this.gameOverAudioSource = this._audioSources [1];

		this.rawClear.enabled = false;
		this.rawGameOver.enabled = false;

		this.dataScore = 0;

		if (Application.loadedLevelName == "Stage1") {
			loadLevelName = "Stage2";
		} else if (Application.loadedLevelName == "Stage2") {
			loadLevelName = "Stage3";
		} else {
			loadLevelName = "TitleScene";
		}
	}

	// Update is called once per frame
	void Update(){
		if (this.gameOver == true) {
			this.sceneChangeTime -= Time.deltaTime;
			if (this.sceneChangeTime <= 0.0f) {
				Application.LoadLevel ("TitleScene");
			}
			return;
		}

		if (this.gameClear == true) {
			this.sceneChangeTime -= Time.deltaTime;
			if (this.sceneChangeTime <= 0.0f) {
				Application.LoadLevel (loadLevelName);
			}
			return;
		}

		timeRemaining -= Time.deltaTime;
		this.dataTimer = (int)timeRemaining;

		this.txtTimer.text = "TIMER <color=#ff0000>" + this.dataTimer.ToString () + "</color>";

		if(timeRemaining<= 0.0f ){
			this.GameOver();
		}
	}
	
	public void GameOver() {
		gameOver = true;
		if (gameOverAudioSource != null) {
			gameOverAudioSource.Play ();
		}
		this.rawGameOver.enabled = true;
        Debug.Log("GameOver");
	}

	public void GameClear() {
		gameClear = true;
		if (gameWinAudioSource != null) {
			gameWinAudioSource.Play ();
		}
		this.rawClear.enabled = true;
        Debug.Log("GameClear");
    }

	public void UpdatePlayerHP(int _HP) {
		if (this.playerHP != null) {
			this.playerHP.text = "HP <color=#ff0000>" + _HP.ToString () + "</color>";
		}
	}

	public void UpdatePlayerScore(int _Score) {
		if (this.playerScore != null) {
			this.dataScore += _Score;
			this.playerScore.text = "Score <color=#0000ff>" + this.dataScore.ToString () + "</color>";
		}
	}
}
