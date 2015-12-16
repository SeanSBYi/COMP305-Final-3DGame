// Files:			PlayerCtrl.cs
//
// Author:			Sangbeom Yi
// Description:		Controll the Player Character.
//
// Revision History 11/11/2015 file created
//
//
// Last Modified by	11/11/2015

using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour {
	// Public Instance Values.
	public float attackRange = 1.5f;

	// Private Instance Values.
	const float RayCastMaxDistance = 100.0f;
	private InputManager inputManager;

	private CharacterStatus status;
	private CharaAnimation charaAnimation;
	private Transform attackTarget;

	private GameRuleCtrl gameRuleCtrl;

	// 
	public float joystickSpeed = 2.0f;
	FollowCamera followcamera;

	// Player State.
	enum PlayerState {
		PlayerWalking,
		PlayerAttacking,
		PlayerDied,
	};

	// Manage the Player State
	private PlayerState PS = PlayerState.PlayerWalking;
	private PlayerState nextPS = PlayerState.PlayerWalking;

	// Use this for initialization
	void Start () {
		this.inputManager = FindObjectOfType<InputManager> ();
		this.charaAnimation = GetComponent<CharaAnimation> ();
		this.status = GetComponent<CharacterStatus> ();
		this.gameRuleCtrl = FindObjectOfType<GameRuleCtrl> ();

	}
	
	// Update is called once per frame
	void Update () {
		switch (this.PS) {
			case PlayerState.PlayerWalking:
				this.Walking();
				break;
			case PlayerState.PlayerAttacking:
				this.Attacking();
				break;
			default:
				break;
		}

		if (this.PS != this.nextPS) {
			this.PS = this.nextPS;
			switch (this.PS) {
				case PlayerState.PlayerWalking:
					this.WalkingStart();
					break;
				case PlayerState.PlayerAttacking:
					this.AttackStart();
					break;
				case PlayerState.PlayerDied:
					this.Died();
					break;
				default:
					break;
			}
		}
	}

	void ChangePlayerState(PlayerState nextState) {
		this.nextPS = nextState;
	}

	// Initilize a State.
	void StateStartCommon() {
		this.status.attacking = false;
		this.status.died = false;
	}

	void WalkingStart() {
		StateStartCommon ();
	}

	// If player is walking...
	void Walking(){
		if (followcamera == null) {
			followcamera = FindObjectOfType<FollowCamera>();
		}

		Vector3 joyStick = Vector3.zero;
		joyStick.x = Input.GetAxisRaw("Horizontal") * joystickSpeed;
		joyStick.z = Input.GetAxisRaw("Vertical") * joystickSpeed;
		if (joyStick.magnitude > 0) {
			Debug.Log ("joystick Hori=" + joyStick.x + " Ver=" + joyStick.z);
			// カメラの向きに合わせて移動方向を回転
			if (followcamera != null) {
				joyStick = Quaternion.Euler(0, followcamera.horizontalAngle, 0) * joyStick;
			}
			Vector3 position = transform.position;
			position.x += joyStick.x;
			position.z += joyStick.z;
			SendMessage("SetDestination", position);
			//targetCursor.SetPosition(position);
		}

		if (Input.GetKeyDown("space")) {
			ChangePlayerState(PlayerState.PlayerAttacking);
		}
		if (inputManager.Clicked()) {
			Vector2 clickPos = inputManager.GetCursorPosition();
			Ray ray = Camera.main.ScreenPointToRay(clickPos);
			RaycastHit hitInfo;

			// Using Raycast
			if( Physics.Raycast( ray,out hitInfo,RayCastMaxDistance,
			                   ( 1 << LayerMask.NameToLayer("Ground") ) |
			                   ( 1 << LayerMask.NameToLayer("EnemyHit") ))) {
				if( hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Ground") ) {
					SendMessage("SetDestination",hitInfo.point);
				}

				if( hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("EnemyHit") ) {
					Vector3 hitPoint = hitInfo.point;
					hitPoint.y = transform.position.y;
					float distance = Vector3.Distance( hitPoint, transform.position );

					// Check the distance to Enemy from Player.
					if( distance < attackRange ) {
						this.attackTarget = hitInfo.collider.transform;
						this.ChangePlayerState(PlayerState.PlayerAttacking);
					}
				}
			}
		}
	}

	void AttackStart() {
		this.StateStartCommon ();
		this.status.attacking = true;

		// Change the direction to Enemy. (Normalize)
		if (this.attackTarget != null) {
			Vector3 targetDirection = (this.attackTarget.position - this.transform.position).normalized;
			SendMessage ("SetDirection", targetDirection);
		}

		SendMessage ("StopMove");
	}

	// If player is attacking...
	void Attacking() {
		if (this.charaAnimation.IsAttacked ()) {
			this.ChangePlayerState(PlayerState.PlayerWalking);
		}
	}

	// If player is dying...
	void Died() {
		// Die flag on.
		this.status.died = true;
		gameRuleCtrl.GameOver ();
	}

	// Calculate a Damage.
	void Damage( AttackArea.AttackInfo attackInfo ) {
		this.status.HP -= attackInfo.attackPower;

		if( this.status.HP <= 0 ) {
			this.status.HP = 0;
			this.ChangePlayerState( PlayerState.PlayerDied );
		}
	}
}
