using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MoveTargetEnemy {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[SerializeField] private List<BossActionData> BossActData;

	[SerializeField] private AnimationCurve JumpCurve;

	//========================================================================================
	//                                     public
	//========================================================================================

	public void InitBossEnemy(BossCheckPointManager mng) {

		CheckMng = mng;

		CameraManager.Instance.FocusCamera.AddTarget(transform);

		NextPosition = 0;
		var NextTarget = CheckMng.GetNextRelayPoint(NextPosition);
		NextPosition++;

		transform.position = NextTarget.transform.position;

		ActedId = 0;
		ElapsedTime = 0f;

		IsEnabled = true;
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
		var actData = BossActData[ActedId];
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

	bool _IsEnabled;
	/// <summary>
	/// 行動状態ならtrue
	/// </summary>
	public bool IsEnabled {
		private set { _IsEnabled = value; }
		get { return _IsEnabled; }
	}

	public enum BossAct {

		TalePushOff,
		FireBreath,
		SoOA,   // 最適な行動の選択　(Selection of optimal action)
		PopedNoob,
		JumpMove,

	}


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
				StartFireBreath();
				break;

			case BossAct.SoOA:
				StartSelectionOfOptimalAction();
				break;

			case BossAct.PopedNoob:
				StartPopedEnemy();
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

		// 右か左かチェック
		if (IsLeftCheckTalePushOff()) {
			BossControll.SetState(BossAnimationController.EnemyState.LeftTale);
		}
		else {
			BossControll.SetState(BossAnimationController.EnemyState.RightTale);
		}
	}

	/// <summary>
	/// 火球攻撃開始
	/// </summary>
	void StartFireBreath() {



	}

	/// <summary>
	/// 雑魚発射
	/// </summary>
	void StartPopedEnemy() {

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

		float Height = 5f;

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

		BossControll.SetState(BossAnimationController.EnemyState.Wait);
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
}


[System.Serializable]
public struct BossActionData {

	public BossEnemy.BossAct ActType;

	public float StartTime;

	public int GeneralNum;
}
