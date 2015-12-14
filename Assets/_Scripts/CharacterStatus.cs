// Files:			CharacterStatus.cs
//
// Author:			Sangbeom Yi
// Description:		Common Character Status
//
// Revision History 11/11/2015 file created
//
//
// Last Modified by	11/11/2015

using UnityEngine;
using System.Collections;

public class CharacterStatus : MonoBehaviour {
	// Player Data.
	public int HP = 100;
	public int MaxHP = 100;
	public int Pow = 10;
	public int Dex = 10;

	public GameObject lastAttackTarget = null;

	// Name of Character.
	public string characterName = "Player";

	// Player Status.
	public bool attacking = false;
	public bool died = false;
	
	public bool powerBoost = false;
	float powerBoostTime = 0.0f;

	public void GetItem(DropItem.ItemKind itemKind) {
		switch (itemKind)
		{
		case DropItem.ItemKind.Attack:
			powerBoostTime = 5.0f;
			break;
		case DropItem.ItemKind.Heal:
			HP = Mathf.Min(HP + MaxHP / 2, MaxHP);
			break;
		}
	}
	
	void Update() {
		powerBoost = false;
		if (powerBoostTime > 0.0f)
		{
			powerBoost = true;
			powerBoostTime = Mathf.Max(powerBoostTime - Time.deltaTime, 0.0f);
		}
	}
	
}
