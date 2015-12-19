using UnityEngine;
using System.Collections;

public class SceneCtrlTown : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Enter!");
		Application.LoadLevel ("Stage1");
	}
}
