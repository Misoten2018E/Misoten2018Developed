using EDO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : PlayerAttackEnemy {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private List<BossActionData> BossActData;

	[SerializeField] private AnimationCurve JumpCurve;

	[SerializeField] private Transform FacePoint;

	//========================================================================================
	//                                     public
	//========================================================================================

	public void InitBossEnemy(BossCheckPointManager mng) {

		CheckMng = mng;

		CameraManager.Instance.FocusCamera.AddTarget(FacePoint);

		NextPosition = 0;
		var NextTarget = CheckMng.GetNextRelayPoint(NextPosition);
		NextPosition++;

		transform.position = NextTarget.transform.position;

		ActedId = 0;
		ElapsedTime = 0f;
		MyType = EnemyType.Boss;
		EnemyManager.Instance.SetEnemy(this);

		IsEnabled = true;

		BossControll.SetState(BossAnimationController.EnemyState.Howling);
		SoundManager.Instance.PlaySE(SoundManager.SEType.Boss_Howling, FacePoint.position);

		CameraManager.Instance.EventCamera.EventCameraStart(ProduceEventCamera.CameraEvent.BossPop, FacePoint, new Vector3(0, 3f, 0));
	}

	public enum BossAct {

		TalePushOff,
		FireBreath,
		SoOA,   // 最適な行動の選択　(Selection of optimal action)
		PopedNoob,
		JumpMove,

	}

	bool _IsEnabled;
	/// <summary>
	/// 行動状態ならtrue
	/// </summary>
	public bool IsEnabled {
		private set { _IsEnabled = value; }
		get { return _IsEnabled; }
	}

	//========================================================================================
	//                                 public - override
	//========================================================================================

	// Update is called once per frame
	void Update() {

		if (!IsEnabled) {
			return;
		}

		ElapsedTime += Time.deltaTime;

		if (ActedId >= BossActData.Count) {
			return;
		}

		var actData = NowAction;
		if (actData.StartTime <= ElapsedTime) {

			SwitchAction(actData.ActType);
			ActedId++;
		}

	}

	public override void InitEnemy(UsedInitData InitData) {
		
	}

	public override bool IsDeath {
		get { return false; }
		protected set { throw new System.NotImplementedException(); }
	}

	//========================================================================================
	//                                     private
	//========================================================================================

	BossCheckPointManager CheckMng;

	IEnumerator ProduceCort;

	float ElapsedTime;

	int NextPosition;

	// 終了済み行動ID
	int ActedId = -1;

	BossAnimationController _BossControll;
	/// <summary>
	/// ボスのアニメーション管理
	/// </summary>
	public BossAnimationController BossControll {
		get {
			if (_BossControll == null) {
				_BossControll = GetComponentInChildren<BossAnimationController>();
			}
			return _BossControll;
		}
	}

	public BossActionData NowAction {
		get { return BossActData[ActedId]; }
	}


	/// <summary>
	/// 行動変化
	/// </summary>
	/// <param name="act"></param>
	void SwitchAction(BossAct act) {


		switch (act) {
			case BossAct.TalePushOff:
				StartTalePushOff();
				break;

			case BossAct.FireBreath:
				StartFireBreath(NowAction.GeneralNum);
				break;

			case BossAct.SoOA:
				StartSelectionOfOptimalAction();
				break;

			case BossAct.PopedNoob:
				StartPopedEnemy(NowAction.GeneralNum);
				break;

			case BossAct.JumpMove:
				var NextTarget = CheckMng.GetNextRelayPoint(NextPosition);
				NextPosition++;
				StartMoveNext(NextTarget.transform);
				break;

			default:
				break;
		}
	}
      

	//========================================================================================
	//                                    行動開始系
	//========================================================================================


	/// <summary>
	/// 次のエリアへ移動し始める
	/// </summary>
	void StartMoveNext(Transform trs) {

		if (ProduceCort != null) {
			StopCoroutine(ProduceCort);
		}
		ProduceCort = IEUpdateMoveNextArea(trs);
		StartCoroutine(ProduceCort);

		BossControll.SetState(BossAnimationController.EnemyState.Jump);
	}

	/// <summary>
	/// しっぽ攻撃開始
	/// </summary>
	void StartTalePushOff() {

		if (ProduceCort != null) {
			StopCoroutine(ProduceCort);
		}
		ProduceCort = IEUpdateTaleAttack();
		StartCoroutine(ProduceCort);

		SoundManager.Instance.PlaySE(SoundManager.SEType.Boss_Attack1, FacePoint.position);

		// 右か左かチェック
		if (IsLeftCheckTalePushOff()) {
			BossControll.SetState(BossAnimationController.EnemyState.LeftTale);
			BossAtkMng.SetAttackState(BossAttackObject.BodyType.RightHand, true);
			BossAtkMng.SetAttackState(BossAttackObject.BodyType.RightLeg, true);
		}
		else {
			BossControll.SetState(BossAnimationController.EnemyState.RightTale);
			BossAtkMng.SetAttackState(BossAttackObject.BodyType.Tale, true);
		}
	}

	/// <summary>
	/// 火球攻撃開始
	/// </summary>
	void StartFireBreath(int num) {

		if (ProduceCort != null) {
			StopCoroutine(ProduceCort);
		}
		ProduceCort = IEUpdateFireBreath(num);
		StartCoroutine(ProduceCort);
	}

	/// <summary>
	/// 雑魚発射
	/// </summary>
	void StartPopedEnemy(int num) {

	}

	/// <summary>
	/// 最適な行動の選択
	/// 火球orしっぽ攻撃
	/// </summary>
	void StartSelectionOfOptimalAction() {

	}


	//========================================================================================
	//                                    IEnumerater系
	//========================================================================================



	/// <summary>
	/// 次のエリアへ移動する
	/// </summary>
	IEnumerator IEUpdateMoveNextArea(Transform trans) {

		// 位置計算系
		Vector3Complession compPos = new Vector3Complession();
		compPos.Initialize(transform.position, trans.position);

		float Height = 8f;

		// 角度計算系
		QuaternionComplession compQt = new QuaternionComplession();
		{
			var startQt = Quaternion.LookRotation(transform.forward);
			var endQt = Quaternion.LookRotation((City.Instance.transform.position - trans.position).normalized);
			compQt.Initialize(startQt, endQt);
		}

		const float MaxTime = 1.5f;
		TimeComplession time = new TimeComplession(MaxTime);

		while (true) {

			time.TimeUpdate();

			if (time.IsEnd) {
				break;
			}

			float rate = (time.Rate);
			Vector3 pos = compPos.CalcPosition(rate);
			pos.y = Height * JumpCurve.Evaluate(rate);
			transform.position = pos;
			transform.rotation = compQt.Slerp(rate);

			yield return null;
		}

		transform.position = compPos.CalcPosition(1f);
		transform.rotation = compQt.Slerp(1f);

		BossControll.SetState(BossAnimationController.EnemyState.Wait);
	}


	/// <summary>
	/// しっぽ攻撃
	/// </summary>
	IEnumerator IEUpdateTaleAttack() {

		const float MaxTime = 3f;
		yield return new WaitForSeconds(MaxTime);

		BossAtkMng.AttackStateOff();
		BossControll.SetState(BossAnimationController.EnemyState.Wait);
	}


	/// <summary>
	/// 火球攻撃
	/// </summary>
	IEnumerator IEUpdateFireBreath(int Count) {

		const float MaxTime = 0.7f;
		var fire = ResourceManager.Instance.Get<BossFire>(ConstDirectry.DirPrefabsEnemy, ConstActionHitData.ActionBossFire);

		var plObjs = PlayerManager.instance.PlayersObject;
		int AlreadyAttacked = 0;

		Count = Count >= plObjs.Count ? plObjs.Count : Count;

		for (int i = 0; i < Count; i++) {

			BossControll.SetState(BossAnimationController.EnemyState.Fire);
			int plNum = GetRandomPlayer(AlreadyAttacked);
			yield return new WaitForSeconds(MaxTime);

			var f = Instantiate(fire);
			f.StartFire(FacePoint.position, plObjs[plNum].transform.position);

			SoundManager.Instance.PlaySE(SoundManager.SEType.Boss_Attack2, FacePoint.position);

			AlreadyAttacked |= 1 << plNum;

			yield return new WaitForSeconds(MaxTime / 2);
		}

		BossControll.SetState(BossAnimationController.EnemyState.Wait);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="NoSelectBit"></param>
	/// <returns></returns>
	int GetRandomPlayer(int NoSelectBit) {

		int maxPlayer = PlayerManager.instance.PlayersObject.Count;
		int rand = Random.Range(0, maxPlayer);

		while (true) {

			int bitCheck = (1 << rand);

			// 選択可能なNoなら
			if ((NoSelectBit & bitCheck) == 0) {
				break;
			}

			rand = Random.Range(0, maxPlayer);
		}

		return rand;
	}

	/// <summary>
	/// 右にしっぽを振るか左に振るか
	/// (左ならtrue)
	/// </summary>
	/// <returns></returns>
	bool IsLeftCheckTalePushOff() {

		// 最も近いプレイヤーを探す
		var players = PlayerManager.instance.PlayersObject;
		float len = 99999f;
		int index = -1;

		for (int i = 0; i < players.Count; i++) {

			float mag = (players[i].transform.position - transform.position).sqrMagnitude;
			if (mag <= len) {
				len = mag;
				index = i;
			}
		}

		// 最も近いプレイヤーが右にいるか左にいるか判別
		Vector3 dir = (players[index].transform.position - transform.position).normalized;
		float dotFR = Vector3.Dot(dir, transform.right);

		return (dotFR < 0f);
	}


	


	//========================================================================================
	//                                    cache
	//========================================================================================


	BossAttackObjectManager _BossAtkMng;
	public BossAttackObjectManager BossAtkMng {
		get {
			if (_BossAtkMng == null) {
				_BossAtkMng = GetComponent<BossAttackObjectManager>();
			}
			return _BossAtkMng;
		}
	}

	
	BossHittedObjectManager _BossHitMng;
	public BossHittedObjectManager BossHitMng {
		get {
			if (_BossHitMng == null) {
				_BossHitMng = GetComponent<BossHittedObjectManager>();
			}
			return _BossHitMng;
		}
	}
}


[System.Serializable]
public struct BossActionData {

	public BossEnemy.BossAct ActType;

	public float StartTime;

	public int GeneralNum;
}
