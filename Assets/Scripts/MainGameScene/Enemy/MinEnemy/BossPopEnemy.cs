using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPopEnemy : AimingPlayerEnemy {

	protected override void Start() {

		StopMove(5f);
		ieAttackMode = PopedMotion();
		StartCoroutine(ieAttackMode);
	}

	public override void InitEnemy(UsedInitData InitData) {

		MyType = EnemyType.BossPop;
		EnemyManager.Instance.SetEnemy(this);
	}


	IEnumerator PopedMotion() {

		yield return new WaitForSeconds(2f);
		base.Start();
	}


	//========================================================================================
	//                                    override
	//========================================================================================

	protected override void AnimationAttack() {

		AnimCtrl.SetState(BossAnimationController.EnemyState.LeftTale);
		BossAtk.SetAttackState(BossAttackObject.BodyType.RightHand, true);
	}

	protected override void AnimationAttackPose() {

		AnimCtrl.SetState(BossAnimationController.EnemyState.Howling);
	}

	protected override void AnimationMove() {

		AnimCtrl.SetState(BossAnimationController.EnemyState.Move);
	}

	protected override void AttackPose() {

		IsAttacking = true;
		AnimationAttackPose();

		if (ieAttackMode != null) {
			StopCoroutine(ieAttackMode);
		}
		ieAttackMode = AttackStart(1f + attackIntervalTime);
		StartCoroutine(ieAttackMode);

		StopMove(2f + NextAttackInterval);
	}

	BossAnimationController _AnimCtrl;
	public BossAnimationController AnimCtrl {
		get {
			if (_AnimCtrl == null) {
				_AnimCtrl = GetComponentInChildren<BossAnimationController>();
			}
			return _AnimCtrl;
		}
	}


	BossAttackObjectManager _BossAtk;
	public BossAttackObjectManager BossAtk {
		get {
			if (_BossAtk == null) {
				_BossAtk = GetComponent<BossAttackObjectManager>();
			}
			return _BossAtk;
		}
	}
      
}
