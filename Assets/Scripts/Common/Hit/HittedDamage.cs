using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittedDamage : DamagedAction {


	//========================================================================================
	//                                    inspector
	//========================================================================================

	[Tooltip("衝撃による揺れ動き方")]
	[SerializeField]
	private AnimationCurve ImpactTremble;

	[Range(0f, 1f)]
	[SerializeField]
	private float TremblePower = 0.2f;

	//========================================================================================
	//                                    public
	//========================================================================================

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

		
	}

	/// <summary>
	/// モデルのトランスフォームを渡すこと
	/// 親を渡すと実際の座標などまで揺れるため
	/// </summary>
	/// <param name="trs"></param>
	/// <param name="impact"></param>
	public void HittedTremble(Transform trs, Vector3 impact) {

		if (ieTrembled != null) {

			// 止める
			StopCoroutine(ieTrembled);

			// 初期座標に戻す
			TrembledTrs.localPosition = TrembleStartPosition;
		}

		// 今回の初期化
		TrembledTrs = trs;
		TrembleStartPosition = trs.localPosition;

		// 新しいものに差し替え
		ieTrembled = IETremble(impact);
		StartCoroutine(ieTrembled);
	}

	public bool isHitted {
		get { return (ieTrembled != null); }
	}


	//========================================================================================
	//                                    private
	//========================================================================================

	/// <summary>
	/// 揺れ動き
	/// </summary>
	/// <param name="trs"></param>
	/// <param name="impact"></param>
	/// <returns></returns>
	IEnumerator IETremble(Vector3 impact) {

		const float MaxTime = 0.2f;
		float time = 0f;

		impact = impact * TremblePower;

		while (true) {

			time += Time.deltaTime;

			if (time >= MaxTime) {
				break;
			}

			float rate = ImpactTremble.Evaluate(time / MaxTime);
			TrembledTrs.localPosition = TrembleStartPosition + impact * rate;

			yield return null;
		}

		TrembledTrs.localPosition = TrembleStartPosition;
		ieTrembled = null;
	}

	/// <summary>
	/// 初期座標保持
	/// </summary>
	private Vector3 TrembleStartPosition;
	private Transform TrembledTrs;

	IEnumerator ieTrembled;
}


public class HitLogList {

	List<HitLog> LogList = new List<HitLog>();

	/// <summary>
	/// ログのチェック(存在するならtrue)
	/// </summary>
	/// <param name="obj"></param>
	/// <returns></returns>
	public bool CheckLog(HitObject obj) {

		HitLog log = new HitLog(obj);

		for (int i = 0; i < LogList.Count; i++) {

			if (LogList[i].checkEqual(log)) {
				return true;
			}
		}

		// 喰らったことの無いものとして格納
		LogList.Add(log);
		return false;
	}

	public void CheckEnd() {

		for (int i = 0; i < LogList.Count; i++) {

			if (LogList[i].isEnd) {
				LogList.Remove(LogList[i]);
				i--;
			}
		}
	}


	public class HitLog {

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="obj"></param>
		public HitLog(HitObject obj) {
			Hit = obj;
		}

		HitObject _Hit;
		public HitObject Hit {
			set { _Hit = value; }
			get { return _Hit; }
		}

		public bool isEnd {
			get { return !(Hit.ParentHit.isActive); }
		}

		public bool checkEqual(HitLog log) {

			// 同じプレイヤーの攻撃ならtrue
			bool eqPl = (Hit.ParentHit.PlayerNo == log.Hit.ParentHit.PlayerNo);
			// 他者の攻撃であるので同じではない
			if (!eqPl) {
				return false;
			}

			// 同じ攻撃動作ならtrue
			bool eqAct = (Hit.ParentHit == log.Hit.ParentHit);

			// 同じIDならtrue
			bool eqId = (Hit.Id == log.Hit.Id);

			// 同じ攻撃動作ならtrue
			// かつ同じIDのものならtrue、違うIDなら別として扱う
			return eqAct ? (eqId) : (false);
		}
	}
}


