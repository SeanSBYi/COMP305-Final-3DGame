// Files:			EnemyCtrl.cs
//
// Author:			Sangbeom Yi
// Description:		Control the Enemy's Action.
//
// Revision History 11/25/2015 file created
//
//
// Last Modified by	12/02/2015

using UnityEngine;
using System.Collections;

public class EnemyCtrl : MonoBehaviour {
	// Public Instance Values.
	public float waitBaseTime = 2.0f;
	public float walkRange = 5.0f;
	public Vector3 basePosition;
	public GameObject[] dropItemPrefab;
	public GameObject hitEffect;

	// Private Instance Values.
	private float waitTime;
	CharacterStatus status;
	CharaAnimation charaAnimation;
    CharacterMove characterMove;
	Transform attackTarget;

	GameRuleCtrl gameRuleCtrl;


	// Enemy State.
	enum EnemyState {
        Walking,
        Chasing,
        Attacking,
        Died,
    };
	
	EnemyState ES = EnemyState.Walking;
	EnemyState nextES = EnemyState.Walking;
	
	// Use this for initialization
	void Start () {
		this.gameRuleCtrl = FindObjectOfType<GameRuleCtrl> ();
        this.status = GetComponent<CharacterStatus>();
        charaAnimation = GetComponent<CharaAnimation>();
    	characterMove = GetComponent<CharacterMove>(); 
   
        basePosition = transform.position;
        waitTime = waitBaseTime;
    }
	
	// Update is called once per frame
	void Update () {
		switch (this.ES) {
		case EnemyState.Walking:
			this.Walking();
			break;
		case EnemyState.Chasing:
            this.Chasing();
            break;
		case EnemyState.Attacking:
			this.Attacking();
			break;
		}
		
		if (this.ES != this.nextES)
		{
			this.ES = this.nextES;

			switch (this.ES) {
			case EnemyState.Walking:
				this.WalkStart();
				break;
			case EnemyState.Chasing:
                this.ChaseStart();
                break;
			case EnemyState.Attacking:
				this.AttackStart();
				break;
			case EnemyState.Died:
                this.Died();
                break;
            }
		}
	}

	void ChangeState(EnemyState nextState) {
		this.nextES = nextState;
	}
	
	void WalkStart() {
		StateStartCommon();
	}

    void Walking() {
        if (this.waitTime > 0.0f) {
            this.waitTime -= Time.deltaTime;

            if (this.waitTime <= 0.0f) {
				Vector2 randomValue = Random.insideUnitCircle * walkRange;
                Vector3 destinationPosition = basePosition + new Vector3(randomValue.x, 0.0f, randomValue.y);
                SendMessage("SetDestination", destinationPosition);
            }
        } else {
            if (characterMove.Arrived()) {
                waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
            }

			if (attackTarget){
                ChangeState(EnemyState.Chasing);
            }
        }
    }

    void ChaseStart(){
        StateStartCommon();
    }

    void Chasing() {
	    SendMessage("SetDestination", attackTarget.position);

	    if (Vector3.Distance( attackTarget.position, transform.position ) <= 2.0f) {
		    ChangeState(EnemyState.Attacking);
	    }
    }

	void AttackStart(){
		StateStartCommon();
		status.attacking = true;

		Vector3 targetDirection = (attackTarget.position-transform.position).normalized;
		SendMessage("SetDirection",targetDirection);

		SendMessage("StopMove");
	}

	void Attacking() {
		if (charaAnimation.IsAttacked ()) {
			ChangeState (EnemyState.Walking);
		}

        waitTime = Random.Range(waitBaseTime, waitBaseTime * 2.0f);
        attackTarget = null;
    }

    void dropItem(){
        if (dropItemPrefab.Length == 0) { return; }
        GameObject dropItem = dropItemPrefab[Random.Range(0, dropItemPrefab.Length)];
        Instantiate(dropItem, transform.position, Quaternion.identity);
    }

    void Died() {
		this.status.died = true;
        dropItem();
        Destroy(gameObject);

		if( this.gameObject.tag == "Boss" ) { 
			this.gameRuleCtrl.GameClear () ;
		}
    }
	
	void Damage(AttackArea.AttackInfo attackInfo) {
		// Effect.
		GameObject effect = Instantiate (this.hitEffect, transform.position, Quaternion.identity) as GameObject;
		effect.transform.localPosition = transform.position + new Vector3 (0.0f, 0.5f, 0.0f);
		Destroy (effect, 0.3f);

		// Calculate HP.
		status.HP -= attackInfo.attackPower;

		if (status.HP <= 0) {
			status.HP = 0;
            ChangeState(EnemyState.Died);
		}
	}

	void StateStartCommon(){
		status.attacking = false;
        status.died = false;
    }

    public void SetAttackTarget(Transform target) {
        attackTarget = target;
    }
}
