// Files:			SearchArea.cs
//
// Author:			Sangbeom Yi
// Description:		For Search Collider
//
// Revision History 12/01/2015 file created
//
//
// Last Modified by	12/01/2015

using UnityEngine;
using System.Collections;

public class SearchArea : MonoBehaviour {
	EnemyCtrl enemyCtrl;

	// Use this for initialization
	void Start() {
		enemyCtrl = transform.root.GetComponent<EnemyCtrl>();
	}
	
	void OnTriggerStay( Collider other ) {
        // Check the Player.
		if( other.tag == "Player" )
			enemyCtrl.SetAttackTarget( other.transform );
	}
}
