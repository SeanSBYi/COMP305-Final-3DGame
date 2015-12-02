using UnityEngine;
using System.Collections;

public class GameRuleCtrl : MonoBehaviour {
	// Public Instance Values.
	public float timeRemaining = 5.0f * 60.0f;
	public bool gameOver = false;
    public bool gameClear = false;

	void Update(){
		timeRemaining -= Time.deltaTime;

		if(timeRemaining<= 0.0f ){
			GameOver();
		}
	}
	
	public void GameOver() {
		gameOver = true;
        Debug.Log("GameOver");
	}

	public void GameClear() {
		gameClear = true;
        Debug.Log("GameClear");
    }
}
