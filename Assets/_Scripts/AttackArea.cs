// Files:			AttackArea.cs
//
// Author:			Sangbeom Yi
// Description:		GameObject's AttackArea
//
// Revision History 11/11/2015 file created
//
//
// Last Modified by	11/11/2015

using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {
	CharacterStatus status;

	// Update is called once per frame
	void Start() {
		status = transform.root.GetComponent<CharacterStatus>();
	}

	public class AttackInfo {
		public int attackPower; 
		public Transform attacker;
	}

	AttackInfo GetAttackInfo() {			
		AttackInfo attackInfo = new AttackInfo();
		// Calcuate attack point.
		attackInfo.attackPower = status.Pow;

		if (status.powerBoost) {
			attackInfo.attackPower += attackInfo.attackPower;
		}

		attackInfo.attacker = transform.root;
		
		return attackInfo;
	}

	void OnTriggerEnter(Collider other) {
		other.SendMessage("Damage",GetAttackInfo());
		status.lastAttackTarget = other.transform.root.gameObject;
	}

	void OnAttack(){
		GetComponent<Collider>().enabled = true;
	}

	void OnAttackTermination() {
		GetComponent<Collider>().enabled = false;
	}
}
