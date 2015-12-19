using UnityEngine;
using System.Collections;

public class AttackAreaActivator : MonoBehaviour {
	// PUBLIC INSTANCE VALUES.
	public AudioClip attackSeClip;
	AudioSource attackSeAudio;

	Collider[] attackAreaColliders;

	// Use this for initialization
	void Start () {
		// Audio Setting.
		this.attackSeAudio = gameObject.AddComponent<AudioSource> ();
		this.attackSeAudio.clip = attackSeClip;
		this.attackSeAudio.loop = false;

		// Collider Setting.
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

		// Play Sound.
		this.attackSeAudio.Play ();
	}

	void EndAttackHit() {
		foreach (Collider attackAreaCollider in attackAreaColliders)
			attackAreaCollider.enabled = false;
	}
}
