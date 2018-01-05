using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveFixedEnemy : PlayerAttackEnemy ,IFGroupEnemyCommand {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[Range(3f, 30f)] [Tooltip("攻撃的な時間(攻撃された時にやり返す時間)")]
	[SerializeField] private float AggressiveTime = 10f;

	[Tooltip("前回の攻撃から次の攻撃を出すまでの猶予")]
	[SerializeField] protected float NextAttackInterval = 3f;

	[Tooltip("基本的にデバッグ用なので触る必要無し")]
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

		if (IsGroupMode) {
			GroupModeUpdate();
			return;
		}

		if (NowTarget == null) {
			NextTargetSearch();
			return;
		}

		if (!Damaged.isHitted) {
			MoveAdvanceToTarget(NowTarget, MoveSpeed, RotateSpeed);
		}

		if (IsAttackMode && (!IsAttacking)) {
			ChangeAttackToRange();
		}

		IntervalTimeUpdate();
		HitLog.CheckEnd();
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
				var player = hito.ParentHit.myPlayer.GetPlayerObj();

				// 緊急措置
				if (player == null) {
					hito.DamageHp(MyHp);
					SwtichHitted(hito);
					CameraChance(hito);
					return;
				}

				HittedPlayerAttack(player);

				hito.DamageHp(MyHp);
				SwtichHitted(hito);
				CreateHittedEffect(hito, player.transform.position);

				CameraChance(hito);
				return;
			}
		}

		// ターゲット通過
		if (other.CompareTag(ConstTags.EnemyCheckPoint)) {

			TargetIndex++;
			NextTargetSearch();

		}
		else if (other.CompareTag(ConstTags.City)) {
			NextTargetSearch();
		}
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
		AnimationMove();
		EnableMove();
		MyType = EnemyType.MoveFixed;
		EnemyManager.Instance.SetEnemy(this);

		RotateSpeed = 1.8f;

		base.InitEnemy(InitData);
	}


	//========================================================================================
	//                                    protected - virtual
	//========================================================================================

	/// <summary>
	/// 当たったものに応じた処理
	/// </summary>
	/// <param name="obj"></param>
	virtual protected void SwtichHitted(HitObject obj) {

		// (衝撃の方向)
		var impact = (transform.position - obj.transform.position).normalized;
		
		AnimationDamaged();

		if (MyHp.isDeath && ieDeath == null) {
			EscapeToCity(obj.ParentHit.PlayerNo);
		}

		switch (obj.hitType) {
			case HitObject.HitType.Impact:

				var HitImpact = obj as HitObjectImpact;
				HitImpact.Impact(Damaged, impact);
				Damaged.HittedTremble(ChildModelTrans, impact);
				//		print("hitImpact");

				break;
			case HitObject.HitType.BlowOff:

				var HitBlow = obj as HitObjectBlowOff;

				break;
			case HitObject.HitType.Suction:

				var HitSuction = obj as HitObjectSuction;
				HitSuction.Sucion(Damaged);
				Damaged.HittedTremble(ChildModelTrans, impact);
				//		print("hitSuction");
				break;

			case HitObject.HitType.Freeze:

				HittedFreezeAction((obj as HitObjectFreeze), impact);
				break;

			default:
				break;
		}
	}

	/// <summary>
	/// 攻撃体勢に入る
	/// </summary>
	virtual protected void AttackPose() {

		var prefab = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHitEnemyMin, ConstActionHitData.ActionEnemyMin1);
		MyAttackObj = Instantiate(prefab);
		MyAttackObj.Initialize(this.gameObject);

		SoundManager.Instance.PlaySE(SoundManager.SEType.Mob_Attack, transform.position);

		IsAttacking = true;
		AnimationAttackPose();

		if (ieAttackModeLimit != null) {
			StopCoroutine(ieAttackModeLimit);
		}
		ieAttackModeLimit = AttackStart(1f + attackIntervalTime);
		StartCoroutine(ieAttackModeLimit);

		StopMove(5f + NextAttackInterval);
	}


	/// <summary>
	/// 攻撃終了時関数
	/// </summary>
	virtual protected void AttackEnd() {

		// 次の攻撃待機
		AttackAction = AttackPose;
		// 攻撃中でなくなる
		IsAttacking = false;
		AnimationMove();

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
	/// 外周へ逃げていく
	/// </summary>
	virtual protected void EscapeToOutside() {

		// 逃走モードへ
		AnimationRunAway();
		IsEscape = true;
	//	print("逃げた");

		NowTarget = RunAwayRelayPointManager.Instance.GetNearPoint(transform.position);

		RotateToTarget(NowTarget, 180f);
		MoveSpeed = MoveSpeed * 7;
		RotateSpeed = 30f;
		StartCoroutine(GameObjectExtensions.LoopMethod(1f, LoopScaleMin));

		CreateParticle(ConstEffects.Disappointed);
	}

	/// <summary>
	/// 街へ逃げていく
	/// </summary>
	virtual protected void EscapeToCity(int playerNo) {

		// 逃走モードへ
		AnimationRunAway();
		IsEscape = true;
	//	print("街に行った");

		NowTarget = City.Instance.transform;

		RotateToTarget(NowTarget, 180f);
		MoveSpeed = MoveSpeed * 7;
		RotateSpeed = 30f;
		StartCoroutine(GameObjectExtensions.LoopMethod(1f, LoopScaleMin));
		GetComponent<CapsuleCollider>().isTrigger = true;

		CreateParticle(ConstEffects.Happiness);

		ClushedPlusScore(playerNo);
	}

	/// <summary>
	/// 撃破時スコア追加
	/// </summary>
	virtual protected void ClushedPlusScore(int playerNo) {
		Score.instance.AddScore(Score.ScoreType.E_Fixed, playerNo);
	}

	virtual protected Transform ChildModelTrans {
		get { return ChildModelAnim.transform; }
	}

	/// <summary>
	/// 動きを止める攻撃を受けた
	/// </summary>
	/// <param name="hitObj"></param>
	virtual protected void HittedFreezeAction(HitObjectFreeze hitObj,Vector3 impact) {

		hitObj.Freeze(Damaged);
		Damaged.HittedStoppedAction(hitObj.FreezeTime, ChildModelTrans, impact);

		ChildModelAnim.AnimationStop();

		if (ieFreezeCort != null) {
			StopCoroutine(ieFreezeCort);
		}

		IsStop = true;
		ieFreezeCort = GameObjectExtensions.DelayMethod(hitObj.FreezeTime, () => {

			ChildModelAnim.AnimationRestart();
			IsStop = false;
		});

		StartCoroutine(ieFreezeCort);

		print("hitFreeze");
	}

	protected IEnumerator ieFreezeCort;

	//========================================================================================
	//                                    Animation - protected
	//========================================================================================

	protected virtual void AnimationMove() {

		ChildModelAnim.Animation(EnemyMiniAnimation.AnimationType.Move);
	//	myTrail.EndTrail(TrailSupport.BodyType.LeftArm);
		myTrail.EndTrail(TrailSupport.BodyType.RightArm);
	}

	protected virtual void AnimationAttackPose() {

		ChildModelAnim.Animation(EnemyMiniAnimation.AnimationType.AttackPose);
	}

	protected virtual void AnimationAttack() {

		ChildModelAnim.Animation(EnemyMiniAnimation.AnimationType.Attack);
	//	myTrail.StartTrail(TrailSupport.BodyType.LeftArm);
		myTrail.StartTrail(TrailSupport.BodyType.RightArm);
	}

	/// <summary>
	/// ダメージ時アニメーションなど
	/// </summary>
	protected virtual void AnimationDamaged() {

		ChildModelAnim.Animation(EnemyMiniAnimation.AnimationType.Damage);
	//	myTrail.EndTrail(TrailSupport.BodyType.LeftArm);
		myTrail.EndTrail(TrailSupport.BodyType.RightArm);
	}

	/// <summary>
	/// 逃げる時アニメーション
	/// </summary>
	protected virtual void AnimationRunAway() {
		ChildModelAnim.Animation(EnemyMiniAnimation.AnimationType.RunAway);
	//	myTrail.EndTrail(TrailSupport.BodyType.LeftArm);
		myTrail.EndTrail(TrailSupport.BodyType.RightArm);
	}

	protected virtual void AnimationCityPose() {
		ChildModelAnim.Animation(EnemyMiniAnimation.AnimationType.CityPose);
	}

	//========================================================================================
	//                                    protected
	//========================================================================================

	bool _cityTargeted; // 街をターゲットしているかどうか
	protected bool CityTargeted {
		set { _cityTargeted = value; }
		get { return _cityTargeted; }
	}

	Transform _nowTarget;
	protected Transform NowTarget {
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
			AnimationCityPose();

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
	/// ダメージ時エフェクト作成
	/// </summary>
	/// <param name="obj"></param>
	protected void CreateHittedEffect(HitObject obj ,Vector3 playerPos) {

		obj.HitEffect(transform.position + Option, playerPos);

	}
	readonly Vector3 Option = new Vector3(0, 1, 0);

	/// <summary>
	/// 死亡
	/// </summary>
	protected void DestroyMe() {

		Destroy(this.gameObject);
	}

	protected IEnumerator ieAttackModeLimit;
	protected IEnumerator ieDeath;

	protected HitSeriesofAction MyAttackObj;

	//========================================================================================
	//                                    private
	//========================================================================================


	int TargetIndex;

	/// <summary>
	/// プレイヤーに攻撃された時
	/// </summary>
	void HittedPlayerAttack(GameObject player) {

		// 目標初期化
		TargetTransform.Clear();
		TargetIndex = 0;

		TargetTransform.Add(player.transform);
		NowTarget = player.transform;
		CityTargeted = false;

		AttackAction = AttackPose;
		StartPlayerAttackMode(player.transform);
		attackIntervalTime = NextAttackInterval;

	//	myTrail.EndTrail(TrailSupport.BodyType.LeftArm);
		myTrail.EndTrail(TrailSupport.BodyType.RightArm);

		EnableMove();

		ieAttackModeLimit = GameObjectExtensions.DelayMethod(AggressiveTime, StopAttackMode);
		StartCoroutine(ieAttackModeLimit);
	}

	/// <summary>
	/// 攻撃開始までの猶予処理 + 起動
	/// </summary>
	/// <param name="wait"></param>
	/// <returns></returns>
	IEnumerator AttackStart(float wait) {

		yield return new WaitForSeconds(wait);

		AnimationAttack();

		SoundManager.Instance.PlaySE(SoundManager.SEType.Mob_Attack, transform.position);
	//	print("攻撃" + gameObject.name);

		MyAttackObj.SetEndCallback(AttackEnd);

		MyAttackObj.Activate();

		if (ieAttackModeLimit != null) {
			StopCoroutine(ieAttackModeLimit);
		}
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
	/// 攻撃終了
	/// </summary>
	private void StopAttackMode() {

		print("攻撃行動終了");
		StopPlayerAttackMode();
		CityTargeted = true;
		AnimationMove();
		NowTarget = City.Instance.transform;
	}

	

	readonly Vector3 NormalScale = new Vector3(1f, 1f, 1f);
	readonly Vector3 RunAwayScale = new Vector3(0.7f, 0.7f, 0.7f);

	/// <summary>
	/// スケール変更
	/// </summary>
	/// <param name="rate"></param>
	private void LoopScaleMin(float rate) {

		ChildModelTrans.localScale = (NormalScale - RunAwayScale * rate);
	}

	/// <summary>
	/// カメラチャンスを知らせる
	/// </summary>
	/// <param name="hito"></param>
	protected void CameraChance(HitObject hito) {

		var ph = hito.ParentHit;

		if (ph == null) {
			return;
		}

		CheckProducePhotoCamera.PhotoType type = CheckProducePhotoCamera.PhotoType.Strong;
		switch (ph.actionType) {
			case HitSeriesofAction.ActionType.LightEnd:
				type = CheckProducePhotoCamera.PhotoType.LightEnd;
				break;
			case HitSeriesofAction.ActionType.Strong:
				break;
			default:
				return;
		}

		CheckProducePhotoCamera.Instance.PhotoChance(hito.transform, transform, type, 0/*ph.myPlayer.GetPlayerObj()*/);
	}

	protected void CreateParticle(string particleName) {

		var ef = ResourceManager.Instance.Get<ParticleSupport>(ConstDirectry.DirParticleEdo, particleName);
		var e = Instantiate(ef);
		Vector3 p = transform.position;
		p.y += 2f;
		e.transform.SetParent(transform);
		e.transform.position = p;
		e.Play();
	}

	//========================================================================================
	//                                    Cache
	//========================================================================================

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
	public EnemyMiniAnimation ChildModelAnim {
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


	//========================================================================================
	//                                    GroupMode
	//========================================================================================

	/// <summary>
	/// グループ行動開始
	/// </summary>
	/// <param name="target"></param>
	virtual public void GroupStart(Transform target) {

		NowTarget = target;
		IsGroupMode = true;
	}

	protected void GroupModeUpdate() {

		if (!Damaged.isHitted) {
			MoveAdvanceToTarget(NowTarget, MoveSpeed);
		}

		IntervalTimeUpdate();
		HitLog.CheckEnd();
	}

	/// <summary>
	/// グループでの攻撃開始
	/// </summary>
	public void GroupAttack() {

		attackIntervalTime = 0f;
		AttackPose();
	}

	/// <summary>
	/// グループ行動終了
	/// </summary>
	virtual public void GroupEnd() {

		NextTargetSearch();
		EnableMove();
		IsAttackMode = false;
		IsGroupMode = false;
	}

	bool _IsGroupMode;
	/// <summary>
	/// グループ行動状態かどうか
	/// </summary>
	public bool IsGroupMode {
		protected set { _IsGroupMode = value; }
		get { return _IsGroupMode; }
	}
	
}
