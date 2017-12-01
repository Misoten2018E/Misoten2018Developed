using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingPlayerEnemy : MoveFixedEnemy {

	// Use this for initialization
	protected override void Start () {

		CityTargeted = false;
		IsEscape = false;
		IsAttackMode = true;
		AttackAction = AttackPose;
		StartPlayerAttackMode();
		NowTarget = TargetPlayer;
	}

	// Update is called once per frame
	protected override void Update () {

		if (IsEscape) {
			MoveAdvanceToTarget(NowTarget, MoveSpeed);
			HitLog.CheckEnd();
			return;
		}

		NowTarget = TargetPlayer;
		if (NowTarget == null) {
			return;
		}

		// 攻撃を喰らっていなくて、かつ移動出来る状態なら
		if (!Damaged.isHitted && (!IsStop)) {
			MoveAdvanceToTarget(NowTarget, MoveSpeed);
		}

		if (IsAttackMode && (!IsAttacking)) {			
			ChangeAttackToRange();
		}

		HitLog.CheckEnd();
	}

	public override void InitEnemy(UsedInitData InitData) {

		transform.position = InitData.BasePosition.position;
		MyType = EnemyType.PlayerAttack;
		EnemyManager.Instance.SetEnemy(this);
	}

	protected override void EscapeToCity() {

		base.EscapeToCity();
		StopPlayerAttackMode();
	}

	/// <summary>
	/// 当たった時
	/// </summary>
	/// <param name="other"></param>
	protected override void OnTriggerEnter(Collider other) {

		//既に逃げ始めている
		if (IsEscape) {

			if (other.CompareTag(ConstTags.RunAwayPoint)) {
				DestroyMe();
			}

			return;
		}

		// プレイヤの攻撃
		if (other.CompareTag(ConstTags.PlayerAttack)) {

			var hito = other.GetComponent<HitObject>();

			bool isHit = !HitLog.CheckLog(hito);
			if (isHit) {

				SwtichHitted(hito);
				hito.DamageHp(MyHp);
				return;
			}
		}
	}


	//========================================================================================
	//                                    private
	//========================================================================================

	/// <summary>
	/// 攻撃体勢に入る
	/// </summary>
	override protected void AttackPose() {

		var prefab = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHitEnemyMin, ConstActionHitData.ActionEnemyMin1);
		MyAttackObj = Instantiate(prefab);
		MyAttackObj.Initialize(this.gameObject);

		IsAttacking = true;
		AnimationAttackPose();

		if (ieAttackMode != null) {
			StopCoroutine(ieAttackMode);
		}
		ieAttackMode = AttackStart(1f + attackIntervalTime);
		StartCoroutine(ieAttackMode);

		StopMove(2f + NextAttackInterval);
	}

	/// <summary>
	/// 攻撃開始までの猶予処理 + 起動
	/// </summary>
	/// <param name="wait"></param>
	/// <returns></returns>
	protected IEnumerator AttackStart(float wait) {

		yield return new WaitForSeconds(wait);

		AnimationAttack();
		print("攻撃" + gameObject.name);

		MyAttackObj.SetEndCallback(AttackEnd);

		MyAttackObj.Activate();

		if (ieAttackMode != null) {
			StopCoroutine(ieAttackMode);
		}
	}

	/// <summary>
	/// 攻撃終了時関数
	/// </summary>
	private void AttackEnd() {

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

		// 利用したので削除
		Destroy(MyAttackObj.gameObject);
	}


	public IEnumerator ieAttackMode {
		protected set { ieAttackModeLimit = value; }
		get { return ieAttackModeLimit; }
	}
      
}
