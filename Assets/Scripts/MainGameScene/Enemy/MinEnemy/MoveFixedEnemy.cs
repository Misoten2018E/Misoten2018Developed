using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveFixedEnemy : PlayerAttackEnemy {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[Range(3f, 30f)]
	[SerializeField] private float AggressiveTime = 10f;

	[SerializeField] protected float NextAttackInterval = 3f;

	[SerializeField] private List<Transform> TargetTransform;

	//========================================================================================
	//                                    public
	//========================================================================================

	public void AddTarget(Transform target, bool isClear = false) {

		if (isClear) {
			TargetTransform.Clear();
		}

		TargetTransform.Add(target);

	}

	//========================================================================================
	//                                    public - override
	//========================================================================================

	// Use this for initialization
	protected virtual void Start() {
		TargetIndex = 0;
		CityTargeted = false;
		IsEscape = false;
		NextTargetSearch();
	}

	// Update is called once per frame
	protected virtual void Update() {

		if (IsStop) {
			return;
		}

		if (NowTarget == null) {
			NextTargetSearch();
			return;
		}

		if (!Damaged.isHitted) {
			MoveAdvanceToTarget(NowTarget, MoveSpeed);
		}

		if (IsAttackMode && (!IsAttacking)) {
			ChangeAttackToRange();
		}

		IntervalTimeUpdate();
		HitLog.CheckEnd();
	}

	/// <summary>
	/// 敵初期化
	/// </summary>
	/// <param name="InitData"></param>
	public override void InitEnemy(UsedInitData InitData) {

		transform.position = InitData.BasePosition.position;
		transform.rotation = InitData.BasePosition.rotation;
		TargetIndex = 0;
		CityTargeted = false;
		ChildModel.Animation(EnemyMiniAnimation.AnimationType.Move);
		EnableMove();
		MyType = EnemyType.MoveFixed;
		EnemyManager.Instance.SetEnemy(this);

		base.InitEnemy(InitData);
	}

	/// <summary>
	/// 当たり始めの判定
	/// </summary>
	/// <param name="other"></param>
	protected virtual void OnTriggerEnter(Collider other) {

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

				// 攻撃元の座標を受け取る
				HittedPlayerAttack(hito.ParentHit.myPlayer);

				SwtichHitted(hito);
				hito.DamageHp(MyHp);
				return;
			}
		}

		// ターゲット通過
		if (other.CompareTag(ConstTags.EnemyCheckPoint)) {

			TargetIndex++;
			NextTargetSearch();

		} else if (other.CompareTag(ConstTags.City)) {
			NextTargetSearch();
		}
	}

	//========================================================================================
	//                                    private
	//========================================================================================

	bool _cityTargeted; // 街をターゲットしているかどうか
	protected bool CityTargeted {
		set { _cityTargeted = value; }
		get { return _cityTargeted; }
	}
	int TargetIndex;

	Transform _nowTarget;
	protected Transform NowTarget{
		set { _nowTarget = value; }
		get { return _nowTarget; }
	}

	protected float attackIntervalTime;

	/// <summary>
	/// 次の獲物へ向かう
	/// </summary>
	protected void NextTargetSearch() {

		if (CityTargeted && (!IsStop)) {

			// 街に着いたということなので
			// 一定時間待機状態へ
			StopMove(5f, EscapeToOutside);
			ChildModel.Animation(EnemyMiniAnimation.AnimationType.CityPose);

			return;
		}


		// 次のターゲットが存在しない場合
		if (((TargetIndex) >= TargetTransform.Count)) {

			CityTargeted = true;
			NowTarget = City.Instance.transform;
		}
		else {
			NowTarget = TargetTransform[TargetIndex];
		}

	}

	/// <summary>
	/// 当たったものに応じた処理
	/// </summary>
	/// <param name="obj"></param>
	protected void SwtichHitted(HitObject obj) {

		// (衝撃の方向)
		var impact = (transform.position - obj.transform.position).normalized;
		Damaged.HittedTremble(ChildModel.transform, impact);

		ChildModel.Animation(EnemyMiniAnimation.AnimationType.Damage);
		ChildModel.Animation(EnemyMiniAnimation.AnimationType.Move);

		if (MyHp.isDeath && ieDeath == null) {
			ieDeath = GameObjectExtensions.DelayMethod(0.5f, DestroyMe);
			StartCoroutine(ieDeath);
		}

		switch (obj.hitType) {
			case HitObject.HitType.Impact:

				var HitImpact = obj as HitObjectImpact;
				HitImpact.Impact(Damaged, impact);
				print("hitImpact");

				break;
			case HitObject.HitType.BlowOff:

				var HitBlow = obj as HitObjectBlowOff;

				break;
			case HitObject.HitType.Suction:

				var HitSuction = obj as HitObjectSuction;
				HitSuction.Sucion(Damaged);
				print("hitSuction");
				break;

			default:
				break;
		}
	}

	/// <summary>
	/// プレイヤーに攻撃された時
	/// </summary>
	void HittedPlayerAttack(PlayerBase player) {

		// 目標初期化
		TargetTransform.Clear();
		TargetIndex = 0;

		TargetTransform.Add(player.transform);
		NowTarget = player.transform;
		CityTargeted = false;

		AttackAction = AttackPose;
		StartPlayerAttackMode(player.transform);

		myTrail.EndTrail(TrailSupport.BodyType.LeftArm);
		myTrail.EndTrail(TrailSupport.BodyType.RightArm);

		ieAttackModeLimit = GameObjectExtensions.DelayMethod(AggressiveTime, StopAttackMode);
		StartCoroutine(ieAttackModeLimit);
	}

	protected IEnumerator ieAttackModeLimit;
	IEnumerator ieDeath;

	protected HitSeriesofAction MyAttackObj;

	/// <summary>
	/// 攻撃体勢に入る
	/// </summary>
	private void AttackPose() {

		var prefab = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHitEnemyMin, ConstActionHitData.ActionEnemyMin1);
		MyAttackObj = Instantiate(prefab);
		MyAttackObj.Initialize(this.gameObject);

		IsAttacking = true;
		ChildModel.Animation(EnemyMiniAnimation.AnimationType.AttackPose);

		if (ieAttackModeLimit != null) {
			StopCoroutine(ieAttackModeLimit);
		}
		ieAttackModeLimit = AttackStart(1f + attackIntervalTime);
		StartCoroutine(ieAttackModeLimit);

		StopMove(5f+NextAttackInterval);
	}

	/// <summary>
	/// 攻撃開始までの猶予処理 + 起動
	/// </summary>
	/// <param name="wait"></param>
	/// <returns></returns>
	IEnumerator AttackStart(float wait) {

		yield return new WaitForSeconds(wait);

		ChildModel.Animation(EnemyMiniAnimation.AnimationType.Attack);
		myTrail.StartTrail(TrailSupport.BodyType.LeftArm);
		myTrail.StartTrail(TrailSupport.BodyType.RightArm);
		print("攻撃" + gameObject.name);

		MyAttackObj.SetEndCallback(AttackEnd);

		MyAttackObj.Activate();

		if (ieAttackModeLimit != null) {
			StopCoroutine(ieAttackModeLimit);
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
		ChildModel.Animation(EnemyMiniAnimation.AnimationType.Move);
		myTrail.EndTrail(TrailSupport.BodyType.LeftArm);
		myTrail.EndTrail(TrailSupport.BodyType.RightArm);

		// 次の目標
		StartPlayerAttackMode(NowTarget.transform);

		// 移動開始
		EnableMove();

		// 制限時間開始
		StopCoroutine(ieAttackModeLimit);
		ieAttackModeLimit = GameObjectExtensions.DelayMethod(AggressiveTime, StopAttackMode);
		StartCoroutine(ieAttackModeLimit);
		attackIntervalTime = NextAttackInterval;


		// 利用したので削除
		Destroy(MyAttackObj.gameObject);
	}

	/// <summary>
	/// 時間経過
	/// </summary>
	private void IntervalTimeUpdate() {

		if (attackIntervalTime > 0f) {
			attackIntervalTime -= Time.deltaTime;

			if (attackIntervalTime < 0f) {
				attackIntervalTime = 0f;
			}
		}
	}

	/// <summary>
	/// 死亡
	/// </summary>
	protected void DestroyMe() {

		Destroy(this.gameObject);
	}

	/// <summary>
	/// 攻撃終了
	/// </summary>
	private void StopAttackMode() {

		print("攻撃行動終了");
		StopPlayerAttackMode();
		CityTargeted = true;
		ChildModel.Animation(EnemyMiniAnimation.AnimationType.Move);
		NowTarget = City.Instance.transform;
	}

	/// <summary>
	/// 外周へ逃げていく
	/// </summary>
	private void EscapeToOutside() {

		// 逃走モードへ
		// 今は仮で死ぬ
		ChildModel.Animation(EnemyMiniAnimation.AnimationType.RunAway);
		IsEscape = true;
		print("逃げた");

		NowTarget = RunAwayRelayPointManager.Instance.GetNearPoint(transform.position);

		RotateToTarget(NowTarget, 90f);
		MoveSpeed = MoveSpeed * 7;
		StartCoroutine(GameObjectExtensions.LoopMethod(1f, LoopScaleMin));
	}

	readonly Vector3 NormalScale = new Vector3(1f, 1f, 1f);
	readonly Vector3 RunAwayScale = new Vector3(0.7f, 0.7f, 0.7f);

	private void LoopScaleMin(float rate) {

		ChildModel.transform.localScale = (NormalScale - RunAwayScale * rate);
	}

	bool _IsEscape;
	/// <summary>
	/// 逃走flag
	/// </summary>
	public bool IsEscape {
		protected set { _IsEscape = value; }
		get { return _IsEscape; }
	}


	TrailBodyManager _myTrail;
	public TrailBodyManager myTrail {
		get {
			if (_myTrail == null) {
				_myTrail = GetComponentInChildren<TrailBodyManager>();
			}
			return _myTrail;
		}
	}
      

	EnemyMiniAnimation _ChildModel;
	public EnemyMiniAnimation ChildModel {
		get {
			if (_ChildModel == null) {
				_ChildModel = GetComponentInChildren<EnemyMiniAnimation>();
			}
			return _ChildModel;
		}
	}

	public override bool IsDeath {
		get { return IsEscape; }
		protected set { IsEscape = value; }
	}
}
