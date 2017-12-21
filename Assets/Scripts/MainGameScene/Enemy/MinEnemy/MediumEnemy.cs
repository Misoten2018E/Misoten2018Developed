﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GroupLeaderEnemyCommand))]
public class MediumEnemy : AimingPlayerEnemy {



	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private float GroupCheckTime = 0.5f;

	//========================================================================================
	//                                     public
	//========================================================================================

	//========================================================================================
	//									   override
	//========================================================================================

	protected override void Start() {
		base.Start();
	}


	override protected void Update() {
		base.Update();

		if ((!IsGroupLeader) && CheckGroupFormation()) {
			IsGroupLeader = true;
			GroupCommand.NoticeEnemiesGather();
		}

		Debug();
	}

	public override void InitEnemy(UsedInitData InitData) {

		transform.position = InitData.BasePosition.position;
		MyType = EnemyType.Middle;
		EnemyManager.Instance.SetEnemy(this);

		ChildModelMedium.SetParentTransform(transform);

		IsGroupLeader = false;
		ElapsedCheckTime = GroupCheckTime;
		GroupCommand.GroupInitialize();

		RotateSpeed = 1.8f;

	}

	/// <summary>
	/// 攻撃ポーズ
	/// </summary>
	protected override void AnimationAttackPose() {
		ChildModelMedium.Animation(EnemyMediumAnimation.AnimationType.AttackPose);
	}

	/// <summary>
	/// 攻撃アニメーション
	/// </summary>
	protected override void AnimationAttack() {

		switch (AttackId) {

			case 0:
				ChildModelMedium.Animation(EnemyMediumAnimation.AnimationType.Attack1);
				myTrail.StartTrail(TrailSupport.BodyType.RightArm);
				SoundManager.Instance.PlaySE(SoundManager.SEType.Hero_Light1, transform.position);
				break;

			case 1:
				ChildModelMedium.Animation(EnemyMediumAnimation.AnimationType.Attack2);
				myTrail.StartTrail(TrailSupport.BodyType.LeftArm);
				SoundManager.Instance.PlaySE(SoundManager.SEType.Hero_Light2, transform.position);
				break;

			case 2:
				ChildModelMedium.Animation(EnemyMediumAnimation.AnimationType.Attack3);
				myTrail.StartTrail(TrailSupport.BodyType.RightLeg);
				SoundManager.Instance.PlaySE(SoundManager.SEType.Hero_Light3, transform.position);
				break;

			default:
				break;
		}
	}

	/// <summary>
	/// 移動アニメーション
	/// </summary>
	protected override void AnimationMove() {
		ChildModelMedium.Animation(EnemyMediumAnimation.AnimationType.Move);
		myTrail.EndTrail();
	}

	/// <summary>
	/// ダメージアニメーション
	/// </summary>
	protected override void AnimationDamaged() {
		ChildModelMedium.Animation(EnemyMediumAnimation.AnimationType.Damage);
		myTrail.EndTrail();
	}

	/// <summary>
	/// 避難アニメーション
	/// </summary>
	protected override void AnimationRunAway() {
		ChildModelMedium.Animation(EnemyMediumAnimation.AnimationType.RunAway);
		myTrail.EndTrail();
	}

	protected override void EscapeToCity() {

		base.EscapeToCity();
		StopPlayerAttackMode();

		GroupCommand.NoticeEnemiesGroupEnd();
		IsGroupLeader = false;
	}

	/// <summary>
	/// 攻撃体勢に入る
	/// </summary>
	override protected void AttackPose() {

		SelectAttackId();

		var prefab = GetResource();
		MyAttackObj = Instantiate(prefab);
		MyAttackObj.Initialize(this.gameObject);


		IsAttacking = true;
		AnimationAttackPose();
		GroupCommand.NoticeEnemiesAttack();

		if (ieAttackMode != null) {
			StopCoroutine(ieAttackMode);
		}

		ieAttackMode = AttackStart(0.5f + attackIntervalTime);
		StartCoroutine(ieAttackMode);

		StopMove(2f + NextAttackInterval);
	}


	protected override void AttackEnd() {

		base.AttackEnd();
		GroupCommand.NoticeEnemiesGroupEnd();
	}

	/// <summary>
	/// 撃破時スコア追加
	/// </summary>
	override protected void ClushedPlusScore() {
		Score.instance.AddScore(Score.ScoreType.E_Medium);
	}

	/// <summary>
	/// 停止時
	/// </summary>
	/// <param name="hitObj"></param>
	/// <param name="impact"></param>
	protected override void HittedFreezeAction(HitObjectFreeze hitObj, Vector3 impact) {

		float freezeTime = hitObj.FreezeTime / 3;

		hitObj.Freeze(Damaged);
		Damaged.HittedStoppedAction(freezeTime, ChildModelTrans, impact);

		if (ieFreezeCort != null) {
			StopCoroutine(ieFreezeCort);
		}

		IsStop = true;
		ieFreezeCort = GameObjectExtensions.DelayMethod(freezeTime, () => {

			ChildModelAnim.AnimationRestart();
			IsStop = false;
		});

		StartCoroutine(ieFreezeCort);

		print("hitFreeze");
	}


	protected override Transform ChildModelTrans {
		get {
			return ChildModelMedium.transform;
		}
	}


	//========================================================================================
	//                                     private
	//========================================================================================

	float ElapsedCheckTime;


	bool _IsGroupLeader;
	public bool IsGroupLeader {
		private set { _IsGroupLeader = value; }
		get { return _IsGroupLeader; }
	}
      

	/// <summary>
	/// グループを組むかどうか
	/// </summary>
	/// <returns></returns>
	private bool CheckGroupFormation() {

		ElapsedCheckTime -= Time.deltaTime;
		if (ElapsedCheckTime <= 0f) {

			ElapsedCheckTime = GroupCheckTime;
			int rand = Random.Range(0, 100);
			return (rand <= GroupFormRate);
		}

		return false;
	}

	

	/// <summary>
	/// 攻撃当たり判定プレハブゲット
	/// </summary>
	/// <returns></returns>
	private HitSeriesofAction GetResource() {

		HitSeriesofAction prefab = null;

		switch (AttackId) {

			case 0:
				prefab = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHitMediumEnemy, ConstActionHitData.ActionEnemyHeroWeak1);
				break;

			case 1:
				prefab = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHitMediumEnemy, ConstActionHitData.ActionEnemyHeroWeak2);
				break;

			case 2:
				prefab = ResourceManager.Instance.Get<HitSeriesofAction>(ConstDirectry.DirPrefabsHitMediumEnemy, ConstActionHitData.ActionEnemyHeroWeak3);
				break;

			default:
				break;
		}
		return prefab;
	}

	/// <summary>
	/// 攻撃タイプの選択
	/// </summary>
	private void SelectAttackId() {
		AttackId = Random.Range(0, 2);
	}

	int AttackId;

	int _GroupFormRate;
	public int GroupFormRate {
		set {
			_GroupFormRate = value;
			_GroupFormRate = Mathf.Clamp(_GroupFormRate, 0, 100);
		}
		get { return _GroupFormRate; }
	}
      
	/// <summary>
	/// 中型用のキャッシュ
	/// </summary>
	EnemyMediumAnimation _ChildModelMedium;
	public EnemyMediumAnimation ChildModelMedium {
		get {
			if (_ChildModelMedium == null) {
				_ChildModelMedium = GetComponentInChildren<EnemyMediumAnimation>();
			}
			return _ChildModelMedium;
		}
	}

	int id = -1;
	private void Debug() {

		string s = "MediumEnemy : " + NowTarget.name;
		id = DebugLog.ChaseLog(s, id);
	}


	GroupLeaderEnemyCommand _GroupCommand;
	public GroupLeaderEnemyCommand GroupCommand {
		get {
			if (_GroupCommand == null) {
				_GroupCommand = GetComponent<GroupLeaderEnemyCommand>();
			}
			return _GroupCommand;
		}
	} 
}
