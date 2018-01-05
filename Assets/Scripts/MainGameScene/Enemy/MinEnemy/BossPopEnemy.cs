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
		transform.position = InitData.BasePosition.position;
		EnemyManager.Instance.SetEnemy(this);

		RotateSpeed = 1.8f;
	}


	IEnumerator PopedMotion() {

		yield return new WaitForSeconds(2f);
		EnableMove();
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

	protected override void AnimationDamaged() {
		
	}

	protected override void AttackPose() {

		var prefab = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHitEnemyMin, ConstActionHitData.ActionEnemyPop);
		MyAttackObj = Instantiate(prefab);
		MyAttackObj.Initialize(this.gameObject);

		SoundManager.Instance.PlaySE(SoundManager.SEType.Mob_Attack, transform.position);

		IsAttacking = true;
		AnimationAttackPose();

		if (ieAttackMode != null) {
			StopCoroutine(ieAttackMode);
		}
		ieAttackMode = AttackStart(1f + attackIntervalTime);
		StartCoroutine(ieAttackMode);

		StopMove(2f + NextAttackInterval);
	}

	protected override void AttackEnd() {

		// 次の攻撃待機
		AttackAction = AttackPose;
		// 攻撃中でなくなる
		IsAttacking = false;

		AnimationMove();

		// 次の目標
		//StartPlayerAttackMode(NowTarget.transform);

		// 移動開始
		EnableMove();
		attackIntervalTime = NextAttackInterval;

		BossAtk.SetAttackState(BossAttackObject.BodyType.RightHand, false);
	}


	override protected void OnTriggerEnter(Collider other) {


		// プレイヤの攻撃
		if (other.CompareTag(ConstTags.PlayerAttack)) {

			var hito = other.GetComponent<HitObject>();

			bool isHit = !HitLog.CheckLog(hito);
			if (isHit) {

				// 攻撃元の座標を受け取る
				var player = hito.ParentHit.myPlayer.GetPlayerObj();

				// 緊急措置
				if (player == null) {
					hito.DamageHp(MyHp);
					SwtichHitted(hito);
					CameraChance(hito);
					return;
				}

				hito.DamageHp(MyHp);
				SwtichHitted(hito);
				CreateHittedEffect(hito, player.transform.position);

				CameraChance(hito);
				return;
			}
		}
	}

	protected override void EscapeToCity(int playerNo) {

		IsEscape = true;

		StopPlayerAttackMode();
		StopMove(10f);
		ClushedPlusScore(playerNo);
		StartCoroutine(GameObjectExtensions.LoopMethod(1f, DestroyLoop, DestroyMe));

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


	//========================================================================================
	//                                    private
	//========================================================================================

	float BaseScale = 0.2f;

	void DestroyLoop(float rate) {

		transform.localScale = new Vector3(rate, rate, rate);
	}


	override protected Transform ChildModelTrans {
		get { return AnimCtrl.transform; }
	}
}
