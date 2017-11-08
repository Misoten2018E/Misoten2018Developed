using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFixedEnemy : PlayerAttackEnemy {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[Range(3f, 30f)]
	[SerializeField] private float AggressiveTime = 10f;

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
	void Start () {
		TargetIndex = 0;
		CityTargeted = false;
		IsEscape = false;
		NextTargetSearch();	
	}
	
	// Update is called once per frame
	void Update () {

		if ((NowTarget == null) || IsStop) {
			return;
		}

		if (!Damaged.isHitted) {
			MoveAdvanceToTarget(NowTarget, MoveSpeed);
		}

		if (IsAttackMode && (!IsAttacking)) {
			ChangeAttackToRange();
		}
		

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

		base.InitEnemy(InitData);
	}

	/// <summary>
	/// 当たり始めの判定
	/// </summary>
	/// <param name="other"></param>
	private void OnTriggerEnter(Collider other) {

		//既に逃げ始めている
		if (IsEscape) {
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

		// ターゲット通過
		if (other.CompareTag(ConstTags.EnemyCheckPoint)) {

			TargetIndex++;
			NextTargetSearch();
			
		}else if (other.CompareTag(ConstTags.City)) {
			NextTargetSearch();
		}
	}

	//========================================================================================
	//                                    private
	//========================================================================================

	bool CityTargeted; // 街をターゲットしているかどうか
	int TargetIndex;
	Transform NowTarget;

	/// <summary>
	/// 次の獲物へ向かう
	/// </summary>
	void NextTargetSearch() {

		if (CityTargeted && (!IsStop)) {

			// 街に着いたということなので
			// 一定時間待機状態へ
			StopMove(5f,()=> { EscapeToOutside(); });
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
	void SwtichHitted(HitObject obj) {

		// (衝撃の方向)
		var impact = (transform.position - obj.transform.position).normalized;
		Damaged.HittedTremble(ChildModel.transform, impact);

		ChildModel.DamageAnimation();


		if (MyHp.isDeath && ieDeath == null) {
			ieDeath = GameObjectExtensions.DelayMethod(0.5f, Destroy);
			StartCoroutine(ieDeath);
		}

		// 攻撃元の座標を受け取る
		HittedPlayerAttack(obj.ParentHit.myPlayer);

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

		ieAttackModeLimit = GameObjectExtensions.DelayMethod(AggressiveTime, StopAttackMode);
		StartCoroutine(ieAttackModeLimit);
	}

	IEnumerator ieAttackModeLimit;
	IEnumerator ieDeath;

	HitSeriesofAction MyAttackObj;

	/// <summary>
	/// 攻撃
	/// </summary>
	private void AttackPose() {

		var prefab = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHitEnemyMin, ConstActionHitData.ActionEnemyMin1);
		MyAttackObj = Instantiate(prefab);
		MyAttackObj.Initialize(this);

		IsAttacking = true;
		ChildModel.Animation(EnemyMiniAnimation.AnimationType.AttackPose);

		if (ieAttackModeLimit != null) {
			StopCoroutine(ieAttackModeLimit);
		}
		ieAttackModeLimit = AttackStart(1f);
		StartCoroutine(ieAttackModeLimit);

		StopMove(5f);
	}

	IEnumerator AttackStart(float wait) {

		yield return new WaitForSeconds(wait);

		ChildModel.Animation(EnemyMiniAnimation.AnimationType.Attack);
		print("攻撃" + gameObject.name);

		MyAttackObj.SetEndCallback(AttackEnd);

		MyAttackObj.Activate();

		if (ieAttackModeLimit != null) {
			StopCoroutine(ieAttackModeLimit);
		}

		// 利用したので削除
		Destroy(MyAttackObj.gameObject);
	}


	private void AttackEnd() {

		// 次の攻撃待機
		AttackAction = AttackPose;
		// 攻撃中でなくなる
		IsAttacking = false;
		ChildModel.Animation(EnemyMiniAnimation.AnimationType.Move);

		// 次の目標
		StartPlayerAttackMode(NowTarget.transform);

		// 移動開始
		EnableMove();

		// 制限時間開始
		StopCoroutine(ieAttackModeLimit);
		StartCoroutine(ieAttackModeLimit);
	}

	/// <summary>
	/// 死亡
	/// </summary>
	private void Destroy() {

		Destroy(this.gameObject);
	}

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
		print("逃げた");
		Destroy();
	}

	bool _IsEscape;
	/// <summary>
	/// 逃走flag
	/// </summary>
	public bool IsEscape {
		private set { _IsEscape = value; }
		get { return _IsEscape; }
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
      
}
