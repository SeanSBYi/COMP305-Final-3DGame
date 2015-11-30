// Files:			HitArea.cs
//
// Author:			Sangbeom Yi
// Description:		GameObject's Hit Area
//
// Revision History 11/11/2015 file created
//
//
// Last Modified by	11/11/2015

using UnityEngine;
using System.Collections;

public class HitArea : MonoBehaviour {

	void Damage(AttackArea.AttackInfo attackInfo) {
		transform.root.SendMessage ("Damage",attackInfo);
	}
}
