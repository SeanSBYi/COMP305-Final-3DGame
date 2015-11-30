using UnityEngine;
using System.Collections;

public class AttackAreaActivator : MonoBehaviour {
	Collider[] attackAreaColliders;

	// Use this for initialization
	void Start () {
		AttackArea[] attackAreas = GetComponentsInChildren<AttackArea>();
		attackAreaColliders = new Collider[attackAreas.Length];
		
		for (int attackAreaCnt = 0; attackAreaCnt < attackAreas.Length; attackAreaCnt++) {
			attackAreaColliders[attackAreaCnt] = attackAreas[attackAreaCnt].GetComponent<Collider>();
			attackAreaColliders[attackAreaCnt].enabled = false;
		}
	}

	void StartAttackHit() {
		foreach (Collider attackAreaCollider in attackAreaColliders)
			attackAreaCollider.enabled = true;
	}

	void EndAttackHit() {
		foreach (Collider attackAreaCollider in attackAreaColliders)
			attackAreaCollider.enabled = false;
	}
}
