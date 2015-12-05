using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClickStartGame() {
		Debug.Log ("Click! Click!");
		Application.LoadLevel ("MainScene");
	}
}
