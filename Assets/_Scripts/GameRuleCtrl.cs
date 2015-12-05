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

	public RawImage rawClear;
	public RawImage rawGameOver;

	// Private Instance Values.
	private int dataScore;
	private int dataTimer;

	// Use this for initialization
	void Start () {
		this.rawClear.enabled = false;
		this.rawGameOver.enabled = false;
	}

	// Update is called once per frame
	void Update(){
		if (this.gameOver == true || this.gameClear == true) {
			this.sceneChangeTime -= Time.deltaTime;
			if (this.sceneChangeTime <= 0.0f) {
				Application.LoadLevel ("TitleScene");
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
		this.rawGameOver.enabled = true;
        Debug.Log("GameOver");
	}

	public void GameClear() {
		gameClear = true;
		this.rawClear.enabled = true;
        Debug.Log("GameClear");
    }
}
